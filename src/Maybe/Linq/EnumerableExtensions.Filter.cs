// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using MaybeF;

namespace MaybeF.Linq;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.Filter{T}(IEnumerable{Maybe{T}}, Func{T, bool}?)"/>
	public static IEnumerable<T> Filter<T>(this IEnumerable<Maybe<T>> @this) =>
		F.EnumerableF.Filter(@this, null);

	/// <inheritdoc cref="F.EnumerableF.Filter{T}(IEnumerable{Maybe{T}}, Func{T, bool}?)"/>
	public static IEnumerable<T> Filter<T>(this IEnumerable<Maybe<T>> @this, Func<T, bool> predicate) =>
		F.EnumerableF.Filter(@this, predicate);
}
