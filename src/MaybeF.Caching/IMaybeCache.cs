// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF.Caching;

public interface IMaybeCache<TKey>
{
	Maybe<TValue> GetValue<TValue>(TKey key);

	void SetValue<TValue>(TKey key, TValue value);

	Task SetValueAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory);

	Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<TValue> valueFactory);

	Maybe<TValue> GetOrCreate<TValue>(TKey key, Func<Maybe<TValue>> valueFactory);

	Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<TValue>> valueFactory);

	Task<Maybe<TValue>> GetOrCreateAsync<TValue>(TKey key, Func<Task<Maybe<TValue>>> valueFactory);

	void RemoveValue(TKey key);
}
