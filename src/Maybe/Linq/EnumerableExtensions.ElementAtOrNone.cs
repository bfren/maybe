// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Collections.Generic;
using Maybe.Functions;

namespace Maybe.Linq;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="MaybeF.EnumerableF.ElementAtOrNone{T}(IEnumerable{T}, int)"/>
	public static Maybe<T> ElementAtOrNone<T>(this IEnumerable<T> @this, int index) =>
		MaybeF.EnumerableF.ElementAtOrNone(@this, index);
}
