// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using Maybe.Functions;

namespace Maybe.Linq;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="MaybeF.EnumerableF.Filter{T}(IEnumerable{Maybe{T}}, Func{T, bool}?)"/>
	public static IEnumerable<T> Filter<T>(this IEnumerable<Maybe<T>> @this) =>
		MaybeF.EnumerableF.Filter(@this, null);

	/// <inheritdoc cref="MaybeF.EnumerableF.Filter{T}(IEnumerable{Maybe{T}}, Func{T, bool}?)"/>
	public static IEnumerable<T> Filter<T>(this IEnumerable<Maybe<T>> @this, Func<T, bool> predicate) =>
		MaybeF.EnumerableF.Filter(@this, predicate);
}
