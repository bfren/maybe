// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using Maybe.Functions;

namespace Maybe.Linq;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="MaybeF.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<TReturn> FilterMap<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, TReturn> map) =>
		MaybeF.EnumerableF.FilterMap(@this, map, null);

	/// <inheritdoc cref="MaybeF.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<TReturn> FilterMap<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, TReturn> map, Func<T, bool> predicate) =>
		MaybeF.EnumerableF.FilterMap(@this, map, predicate);

	/// <inheritdoc cref="MaybeF.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<Maybe<TReturn>> FilterMap<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, Maybe<TReturn>> map) =>
		MaybeF.EnumerableF.FilterMap(@this, map, null);

	/// <inheritdoc cref="MaybeF.EnumerableF.FilterMap{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, TReturn}, Func{T, bool}?)"/>
	public static IEnumerable<Maybe<TReturn>> FilterMap<T, TReturn>(this IEnumerable<Maybe<T>> @this, Func<T, Maybe<TReturn>> map, Func<T, bool> predicate) =>
		MaybeF.EnumerableF.FilterMap(@this, map, predicate);
}
