// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Collections.Generic;
using Maybe.Functions;

namespace Maybe.Linq;

public static partial class DictionaryExtensions
{
	/// <inheritdoc cref="MaybeF.DictionaryF.GetValueOrNone{TKey, TValue}(IDictionary{TKey, TValue}, TKey)"/>
	public static Maybe<TValue> GetValueOrNone<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key) =>
		MaybeF.DictionaryF.GetValueOrNone(@this, key);
}
