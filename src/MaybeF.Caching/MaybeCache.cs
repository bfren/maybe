// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MaybeF.Caching;

/// <inheritdoc cref="MaybeCache{TKey, TValue}"/>
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

		/// <summary>Cache entry exists but is null</summary>
		public sealed record class CacheEntryIsNullMsg : IMsg;

		/// <summary>Error creating cache value using factory function</summary>
		/// <param name="Value">Exception</param>
		public sealed record class ErrorCreatingCacheValueMsg(Exception Value) : IExceptionMsg;

		/// <summary>Cache key is null</summary>
		public sealed record class KeyIsNullMsg : IMsg;
	}
}

/// <inheritdoc cref="IMaybeCache{TKey, TValue}"/>
public sealed class MaybeCache<TKey, TValue> : MaybeCache, IMaybeCache<TKey, TValue>
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
	public Maybe<TValue> GetValue(TKey key)
	{
		// Key cannot be null
		if (key is null)
		{
			return F.None<TValue, M.KeyIsNullMsg>();
		}

		// Attempt to get the value
		if (Cache.TryGetValue(key, out var value))
		{
			if (value is TValue cachedValue)
			{
				return cachedValue;
			}

			return F.None<TValue, M.CacheEntryIsNullMsg>();
		}

		return F.None<TValue, M.CacheEntryDoesNotExistMsg>();
	}

	/// <inheritdoc/>
	public void SetValue(TKey key, TValue value)
	{
		ArgumentNullException.ThrowIfNull(key);
		ArgumentNullException.ThrowIfNull(value);

		_ = Cache.Set(key, value);
	}

	/// <inheritdoc/>
	public async Task SetValueAsync(TKey key, Func<Task<TValue>> valueFactory)
	{
		ArgumentNullException.ThrowIfNull(key);
		ArgumentNullException.ThrowIfNull(valueFactory);

		_ = Cache.Set(key, await valueFactory());
	}

	/// <inheritdoc/>
	public Maybe<TValue> GetOrCreate(TKey key, Func<TValue> valueFactory) =>
		GetOrCreate(key, () => F.Some(valueFactory()));

	/// <inheritdoc/>
	public Maybe<TValue> GetOrCreate(TKey key, Func<Maybe<TValue>> valueFactory)
	{
		// Key cannot be null
		if (key is null)
		{
			return F.None<TValue, M.KeyIsNullMsg>();
		}

		// Check the entry already exists
		if (Cache.TryGetValue(key, out var value) && value is TValue cachedValue)
		{
			return cachedValue;
		}

		// Lock all threads
		CacheLock.Wait();
		try
		{
			// Create value and cache entry
			return valueFactory()
				.Map(
					x =>
					{
						_ = Cache.CreateEntry(key).Value = x;
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
	public Task<Maybe<TValue>> GetOrCreateAsync(TKey key, Func<Task<TValue>> valueFactory) =>
		GetOrCreateAsync(key, async () => F.Some(await valueFactory()));

	/// <inheritdoc/>
	public async Task<Maybe<TValue>> GetOrCreateAsync(TKey key, Func<Task<Maybe<TValue>>> valueFactory)
	{
		// Key cannot be null
		if (key is null)
		{
			return F.None<TValue, M.KeyIsNullMsg>();
		}

		// Check the entry already exists
		if (Cache.TryGetValue(key, out var value) && value is TValue cachedValue)
		{
			return cachedValue;
		}

		// Lock all threads
		await CacheLock.WaitAsync();
		try
		{
			// Create value and cache entry
			return await valueFactory()
				.MapAsync(
					x =>
					{
						_ = Cache.CreateEntry(key).Value = x;
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
