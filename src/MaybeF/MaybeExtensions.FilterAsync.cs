// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public static Task<Maybe<T>> FilterAsync<T>(this Task<Maybe<T>> @this, Func<T, bool> predicate) =>
		F.FilterAsync(@this, x => Task.FromResult(predicate(x)));

	/// <inheritdoc cref="F.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public static Task<Maybe<T>> FilterAsync<T>(this Task<Maybe<T>> @this, Func<T, Task<bool>> predicate) =>
		F.FilterAsync(@this, predicate);
}
