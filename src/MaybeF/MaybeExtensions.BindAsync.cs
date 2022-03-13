// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.BindAsync{T, TReturn}(Maybe{T}, Func{T, Task{Maybe{TReturn}}})"/>
	public static Task<Maybe<TReturn>> BindAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Maybe<TReturn>> bind) =>
		F.BindAsync(@this, x => Task.FromResult(bind(x)));

	/// <inheritdoc cref="F.BindAsync{T, TReturn}(Maybe{T}, Func{T, Task{Maybe{TReturn}}})"/>
	public static Task<Maybe<TReturn>> BindAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<Maybe<TReturn>>> bind) =>
		F.BindAsync(@this, bind);
}
