// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class EnumerableExtensions
{
	/// <inheritdoc cref="F.EnumerableF.BindAsync{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, Task{Maybe{TReturn}}})"/>
	public static IAsyncEnumerable<Maybe<TReturn>> BindAsync<T, TReturn>(IEnumerable<Maybe<T>> @this, Func<T, Task<Maybe<TReturn>>> bind) =>
		F.EnumerableF.BindAsync(@this, bind);
}
