// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MaybeF.Caching;

/// <inheritdoc cref="MaybeCache{TKey}"/>
public abstract class MaybeCache
{
	/// <summary>
	/// Internal creation only
	/// </summary>
	internal MaybeCache() { }

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Cache entry does not exist</summary>
		public sealed record class CacheEntryDoesNotExistMsg : IMsg;

		/// <summary>Cache entry exists but is not of the type requested</summary>
		public sealed record class CacheEntryIsIncorrectTypeMsg : IMsg;

		/// <summary>Cache entry exists but is null</summary>
		public sealed record class CacheEntryIsNullMsg : IMsg;

		/// <summary>Error creating cache value using factory function</summary>
		/// <param name="Value">Exception</param>
		public sealed record class ErrorCreatingCacheValueMsg(Exception Value) : IExceptionMsg;

		/// <summary>Cache key is null</summary>
		public sealed record class KeyIsNullMsg : IMsg;
	}
}

/// <inheritdoc cref="IMaybeCache{TKey}"/>
public sealed class MaybeCache<TKey> : MaybeCache, IMaybeCache<TKey>
	where TKey : notnull
{
	internal IMemoryCache Cache { get; private init; }

	internal SemaphoreSlim CacheLock { get; } = new(1, 1);

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="cache"></param>
	public MaybeCache(IMemoryCache cache) =>
		Cache = cache;

	/// <inheritdoc/>
	public Maybe<TValue> GetValue<TValue>(TKey key)
	{
		// Key cannot be null
		if (key is null)
		{
			return F.None<TValue, M.KeyIsNullMsg>();
		}

		// Attempt to get the value
		if (Cache.TryGetValue(key, out var value))
		{
			return value switch
			{
				TValue correctType =>
					correctType,

				{ } =>
					F.None<TValue, M.CacheEntryIsIncorrectTypeMsg>(),

				_ =>
					F.None<TValue, M.CacheEntryIsNullMsg>()
			};
		}

		return F.None<TValue, M.CacheEntryDoesNotExistMsg>();
	}

	/// <inheritdoc/>
	public void SetValue<TValue>(TKey key, TValue value) =>
		SetValue(key, value, new());

	/// <inheritdoc/>
	public void SetValue<TValue>(TKey key, TValue value, MemoryCacheEntryOptions opt)
	{
		ArgumentNullException.ThrowIfNull(key);
		ArgumentNullException.ThrowIfNull(value);

		_ = Cache.Set(key, value, opt);
	}

	/// <inheritdoc/>
	public Task SetValueAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory) =>
		SetValueAsync(key, valueFactory, new());

	/// <inheritdoc/>
	public async Task SetValueAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory, MemoryCacheEntryOptions opt)
	{
		ArgumentNullException.ThrowIfNull(key);
		ArgumentNullException.ThrowIfNull(valueFactory);

		_ = Cache.Set(key, await valueFactory(), opt);
	}

	/// <inheritdoc/>
	public Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<TValue> valueFactory) =>
		GetOrCreate(key, valueFactory, new());

	/// <inheritdoc/>
	public Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<TValue> valueFactory, MemoryCacheEntryOptions opt) =>
		GetOrCreate(key, () => F.Some(valueFactory()), opt);

	/// <inheritdoc/>
	public Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<Maybe<TValue>> valueFactory) =>
		GetOrCreate(key, valueFactory, new());

	/// <inheritdoc/>
	public Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<Maybe<TValue>> valueFactory, MemoryCacheEntryOptions opt) =>
		GetOrCreateAsync(key, () => Task.FromResult(valueFactory()), opt).Result;

	/// <inheritdoc/>
	public Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory) =>
		GetOrCreateAsync(key, valueFactory, new());

	/// <inheritdoc/>
	public Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory, MemoryCacheEntryOptions opt) =>
		GetOrCreateAsync(key, async () => F.Some(await valueFactory()), opt);

	/// <inheritdoc/>
	public Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<Maybe<TValue>>> valueFactory) =>
		GetOrCreateAsync(key, valueFactory, new());

	/// <inheritdoc/>
	public async Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<Maybe<TValue>>> valueFactory, MemoryCacheEntryOptions opt)
	{
		// Key cannot be null
		if (key is null)
		{
			return F.None<TValue, M.KeyIsNullMsg>();
		}

		// Check the entry already exists
		if (Cache.TryGetValue(key, out var value))
		{
			return value switch
			{
				TValue correctType =>
					correctType,

				{ } =>
					F.None<TValue, M.CacheEntryIsIncorrectTypeMsg>(),

				_ =>
					F.None<TValue, M.CacheEntryIsNullMsg>()
			};
		}

		// Lock all threads
		await CacheLock.WaitAsync();
		try
		{
			return await valueFactory()
				.MapAsync(
					x =>
					{
						_ = Cache.GetOrCreate(key, e => e.SetOptions(opt).SetValue(x));
						return x;
					},
					e => new M.ErrorCreatingCacheValueMsg(e)
				);
		}
		catch (Exception e)
		{
			// Return none on failure
			return F.None<TValue, M.ErrorCreatingCacheValueMsg>(e);
		}
		finally
		{
			// Release other threads
			_ = CacheLock.Release();
		}
	}

	/// <inheritdoc/>
	public void RemoveValue(TKey key)
	{
		ArgumentNullException.ThrowIfNull(key);

		Cache.Remove(key);
	}
}
