// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.Iterate{T}(IEnumerable{Maybe{T}}, Action{T})"/>
	public static Task IterateAsync<T>(this IEnumerable<Maybe<T>> @this, Func<T, Task> f) =>
		F.EnumerableF.IterateAsync(@this, f);
}
