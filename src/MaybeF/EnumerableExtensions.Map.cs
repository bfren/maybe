// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;

namespace MaybeF;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.Map{T}(IEnumerable{T})"/>
	public static IEnumerable<Maybe<T>> Map<T>(this IEnumerable<T> @this) =>
		F.EnumerableF.Map(@this);

	/// <inheritdoc cref="F.EnumerableF.Map{T, TReturn}(IEnumerable{T}, Func{T, Maybe{TReturn}})"/>
	public static IEnumerable<Maybe<TReturn>> Map<T, TReturn>(this IEnumerable<T> @this, Func<T, Maybe<TReturn>> map) =>
		F.EnumerableF.Map(@this, map);
}
