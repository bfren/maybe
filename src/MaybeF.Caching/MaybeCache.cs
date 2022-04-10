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

		/// <summary>Error creating cache value using factory function</summary>
		/// <param name="Value">Exception</param>
		public sealed record class ErrorCreatingCacheValueMsg(Exception Value) : IExceptionMsg;
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
		if (Cache.TryGetValue(key, out TValue value))
		{
			return value;
		}

		return F.None<TValue, M.CacheEntryDoesNotExistMsg>();
	}

	/// <inheritdoc/>
	public void SetValue<TValue>(TKey key, TValue value) =>
		Cache.Set(key, value);

	/// <inheritdoc/>
	public async Task SetValueAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory) =>
		Cache.Set(key, await valueFactory());

	/// <inheritdoc/>
	public Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<TValue> valueFactory) =>
		GetOrCreate(key, () => F.Some(valueFactory()));

	/// <inheritdoc/>
	public Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<Maybe<TValue>> valueFactory)
	{
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
			var createdValue = valueFactory();
			_ = Cache.CreateEntry(key).Value = createdValue;

			// Return value
			return createdValue;
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
	public Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory) =>
		GetOrCreateAsync(key, async () => F.Some(await valueFactory()));

	/// <inheritdoc/>
	public async Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<Maybe<TValue>>> valueFactory)
	{
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
			var createdValue = await valueFactory();
			_ = Cache.CreateEntry(key).Value = createdValue;

			// Return value
			return createdValue;
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
	public void RemoveValue(TKey key) =>
		Cache.Remove(key);
}
