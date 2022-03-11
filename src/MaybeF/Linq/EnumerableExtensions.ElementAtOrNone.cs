// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Collections.Generic;

namespace MaybeF.Linq;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.ElementAtOrNone{T}(IEnumerable{T}, int)"/>
	public static Maybe<T> ElementAtOrNone<T>(this IEnumerable<T> @this, int index) =>
		F.EnumerableF.ElementAtOrNone(@this, index);
}
