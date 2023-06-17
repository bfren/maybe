// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;

namespace MaybeF;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.Iterate{T}(IEnumerable{Maybe{T}}, Action{T})"/>
	public static void Iterate<T>(this IEnumerable<Maybe<T>> @this, Action<T> f) =>
		F.EnumerableF.Iterate(@this, f);
}
