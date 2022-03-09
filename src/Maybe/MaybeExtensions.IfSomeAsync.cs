// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="MaybeF.IfSomeAsync{T}(Task{Maybe{T}}, Func{T, Task})"/>
	public static Task<Maybe<T>> IfSomeAsync<T>(this Task<Maybe<T>> @this, Action<T> ifSome) =>
		MaybeF.IfSomeAsync(@this, x => { ifSome(x); return Task.CompletedTask; });

	/// <inheritdoc cref="MaybeF.IfSomeAsync{T}(Task{Maybe{T}}, Func{T, Task})"/>
	public static Task<Maybe<T>> IfSomeAsync<T>(this Task<Maybe<T>> @this, Func<T, Task> ifSome) =>
		MaybeF.IfSomeAsync(@this, ifSome);
}
