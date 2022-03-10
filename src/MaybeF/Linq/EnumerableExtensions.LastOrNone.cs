// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;

namespace MaybeF.Linq;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.LastOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Maybe<T> LastOrNone<T>(this IEnumerable<T> @this) =>
		F.EnumerableF.LastOrNone(@this, null);

	/// <inheritdoc cref="F.EnumerableF.LastOrNone{T}(IEnumerable{T}, Func{T, bool}?)"/>
	public static Maybe<T> LastOrNone<T>(this IEnumerable<T> @this, Func<T, bool> predicate) =>
		F.EnumerableF.LastOrNone(@this, predicate);
}
