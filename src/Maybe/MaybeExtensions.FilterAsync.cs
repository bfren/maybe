// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="MaybeF.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public static Task<Maybe<T>> FilterAsync<T>(this Task<Maybe<T>> @this, Func<T, bool> predicate) =>
		MaybeF.FilterAsync(@this, x => Task.FromResult(predicate(x)));

	/// <inheritdoc cref="MaybeF.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public static Task<Maybe<T>> FilterAsync<T>(this Task<Maybe<T>> @this, Func<T, Task<bool>> predicate) =>
		MaybeF.FilterAsync(@this, predicate);
}
