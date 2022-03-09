// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="MaybeF.BindAsync{T, TReturn}(Maybe{T}, Func{T, Task{Maybe{TReturn}}})"/>
	public static Task<Maybe<TReturn>> BindAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Maybe<TReturn>> bind) =>
		MaybeF.BindAsync(@this, x => Task.FromResult(bind(x)));

	/// <inheritdoc cref="MaybeF.BindAsync{T, TReturn}(Maybe{T}, Func{T, Task{Maybe{TReturn}}})"/>
	public static Task<Maybe<TReturn>> BindAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<Maybe<TReturn>>> bind) =>
		MaybeF.BindAsync(@this, bind);
}
