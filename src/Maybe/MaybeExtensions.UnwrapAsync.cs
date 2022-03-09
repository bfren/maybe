// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="MaybeF.UnwrapAsync{T, TSingle}(Task{Maybe{T}}, Func{MaybeF.FluentUnwrapAsync{T}, TSingle})"/>
	public static Task<TReturn> UnwrapAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<MaybeF.FluentUnwrapAsync<T>, TReturn> unwrap) =>
		MaybeF.UnwrapAsync(@this, unwrap);
}
