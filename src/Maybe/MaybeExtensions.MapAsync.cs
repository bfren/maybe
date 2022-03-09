// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="MaybeF.MapAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, MaybeF.Handler)"/>
	public static Task<Maybe<TReturn>> MapAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> map, MaybeF.Handler handler) =>
		MaybeF.MapAsync(@this, x => Task.FromResult(map(x)), handler);

	/// <inheritdoc cref="MaybeF.MapAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, MaybeF.Handler)"/>
	public static Task<Maybe<TReturn>> MapAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> map, MaybeF.Handler handler) =>
		MaybeF.MapAsync(@this, map, handler);
}
