// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.UnwrapAsync{T, TSingle}(Task{Maybe{T}}, Func{F.FluentUnwrapAsync{T}, TSingle})"/>
	public static Task<TReturn> UnwrapAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<F.FluentUnwrapAsync<T>, TReturn> unwrap) =>
		F.UnwrapAsync(@this, unwrap);
}
