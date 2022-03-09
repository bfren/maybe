// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using Maybe.Functions;

namespace Maybe.Linq;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="MaybeF.EnumerableF.SingleOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> @this) =>
		MaybeF.EnumerableF.SingleOrNone(@this, null);

	/// <inheritdoc cref="MaybeF.EnumerableF.SingleOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
		MaybeF.EnumerableF.SingleOrNone(@this, predicate);
}
