// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;

namespace MaybeF;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<Maybe<TReturn>> FilterBind<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, Maybe<TReturn>> map) =>
		F.EnumerableF.FilterBind(@this, map, null);

	/// <inheritdoc cref="F.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<Maybe<TReturn>> FilterBind<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, Maybe<TReturn>> map, Func<T, bool> predicate) =>
		F.EnumerableF.FilterBind(@this, map, predicate);
}
