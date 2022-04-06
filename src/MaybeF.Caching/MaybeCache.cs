// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MaybeF.Caching;

public abstract class MaybeCache
{
	internal MaybeCache() { }

	public static class M
	{
		public sealed record class CacheEntryDoesNotExistMsg : IMsg;

		public sealed record class ErrorCreatingCacheValueMsg(Exception Value) : IExceptionMsg;
	}
}

public sealed class MaybeCache<TKey> : MaybeCache, IMaybeCache<TKey>
{
	internal IMemoryCache Cache { get; private init; }

	internal SemaphoreSlim CacheLock { get; } = new(1, 1);

	public MaybeCache(IMemoryCache cache) =>
		Cache = cache;

	public Maybe<TValue> GetValue<TValue>(TKey key)
	{
		if (Cache.TryGetValue(key, out TValue value))
		{
			return value;
		}

		return F.None<TValue, M.CacheEntryDoesNotExistMsg>();
	}

	public void SetValue<TValue>(TKey key, TValue value) =>
		Cache.Set(key, value);

	public async Task SetValueAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory) =>
		Cache.Set(key, await valueFactory());

	public Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<TValue> valueFactory) =>
		GetOrCreate(key, () => F.Some(valueFactory()));

	public Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<Maybe<TValue>> valueFactory)
	{
		if (Cache.TryGetValue(key, out TValue cachedValue))
		{
			return cachedValue;
		}

		CacheLock.Wait();
		try
		{
			var createdValue = valueFactory();
			return Cache.GetOrCreate(key, _ => createdValue);
		}
		catch (Exception e)
		{
			return F.None<TValue, M.ErrorCreatingCacheValueMsg>(e);
		}
		finally
		{
			_ = CacheLock.Release();
		}
	}

	public Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory) =>
		GetOrCreateAsync(key, async () => F.Some(await valueFactory()));

	public async Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<Maybe<TValue>>> valueFactory)
	{
		if (Cache.TryGetValue(key, out TValue cachedValue))
		{
			return cachedValue;
		}

		await CacheLock.WaitAsync();
		try
		{
			var createdValue = await valueFactory();
			return Cache.GetOrCreate(key, _ => createdValue);
		}
		catch (Exception e)
		{
			return F.None<TValue, M.ErrorCreatingCacheValueMsg>(e);
		}
		finally
		{
			_ = CacheLock.Release();
		}
	}

	public void RemoveValue(TKey key) =>
		Cache.Remove(key);
}
