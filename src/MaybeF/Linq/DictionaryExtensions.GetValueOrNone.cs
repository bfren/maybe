// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Collections.Generic;

namespace MaybeF.Linq;

public static partial class DictionaryExtensions
{
	/// <inheritdoc cref="F.DictionaryF.GetValueOrNone{TKey, TValue}(IDictionary{TKey, TValue}, TKey)"/>
	public static Maybe<TValue> GetValueOrNone<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key) =>
		F.DictionaryF.GetValueOrNone(@this, key);
}
