// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF.Caching;

/// <summary>
/// Asynchronous and null-safe cache implementation
/// </summary>
/// <typeparam name="TKey">Key type</typeparam>
public interface IMaybeCache<TKey>
	where TKey : notnull
{
	/// <summary>
	/// Retrieve a cache entry if it exists
	/// </summary>
	/// <typeparam name="TValue">Value Type</typeparam>
	/// <param name="key">Entry key</param>
	Maybe<TValue> GetValue<TValue>(TKey key);

	/// <summary>
	/// Create a cache entry with specified values
	/// </summary>
	/// <typeparam name="TValue">Value Type</typeparam>
	/// <param name="key">Entry key</param>
	/// <param name="value">Entry value</param>
	void SetValue<TValue>(TKey key, TValue value);

	/// <inheritdoc cref="SetValue{TValue}(TKey, TValue)"/>
	Task SetValueAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory);

	/// <summary>
	/// Get a value from the cache or create it if it doesn't exist
	/// </summary>
	/// <typeparam name="TValue">Value Type</typeparam>
	/// <param name="key">Entry key</param>
	/// <param name="valueFactory">Entry value factory</param>
	Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<TValue> valueFactory);

	/// <inheritdoc cref="GetOrCreate{TValue}(TKey, Func{TValue})"/>
	Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<Maybe<TValue>> valueFactory);

	/// <inheritdoc cref="GetOrCreate{TValue}(TKey, Func{TValue})"/>
	Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory);

	/// <inheritdoc cref="GetOrCreate{TValue}(TKey, Func{TValue})"/>
	Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<Maybe<TValue>>> valueFactory);

	/// <summary>
	/// Remove a value from the cache
	/// </summary>
	/// <param name="key">Entry key</param>
	void RemoveValue(TKey key);
}
