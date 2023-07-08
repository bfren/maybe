// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.MapAsync{T, TReturn}(IEnumerable{T}, Func{T, Task{Maybe{TReturn}}})"/>
	public static IAsyncEnumerable<Maybe<TReturn>> MapAsync<T, TReturn>(IEnumerable<T> @this, Func<T, Task<Maybe<TReturn>>> map) =>
		F.EnumerableF.MapAsync(@this, map);
}
