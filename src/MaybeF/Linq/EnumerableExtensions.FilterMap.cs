// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;

namespace MaybeF.Linq;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<TReturn> FilterMap<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, TReturn> map) =>
		F.EnumerableF.FilterMap(@this, map, null);

	/// <inheritdoc cref="F.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<TReturn> FilterMap<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, TReturn> map, Func<T, bool> predicate) =>
		F.EnumerableF.FilterMap(@this, map, predicate);

	/// <inheritdoc cref="F.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<Maybe<TReturn>> FilterMap<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, Maybe<TReturn>> map) =>
		F.EnumerableF.FilterMap(@this, map, null);

	/// <inheritdoc cref="F.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<Maybe<TReturn>> FilterMap<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, Maybe<TReturn>> map, Func<T, bool> predicate) =>
		F.EnumerableF.FilterMap(@this, map, predicate);
}
