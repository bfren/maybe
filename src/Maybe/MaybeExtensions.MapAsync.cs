// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using MaybeF;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.MapAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, F.Handler)"/>
	public static Task<Maybe<TReturn>> MapAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> map, F.Handler handler) =>
		F.MapAsync(@this, x => Task.FromResult<TReturn>(map(x)), handler);

	/// <inheritdoc cref="F.MapAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, F.Handler)"/>
	public static Task<Maybe<TReturn>> MapAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> map, F.Handler handler) =>
		F.MapAsync(@this, map, handler);
}
