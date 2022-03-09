// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe.Extensions;

public static partial class ObjectExtensions
{
	/// <inheritdoc cref="MaybeF.SomeIfAsync{T}(Func{bool}, Func{Task{T}}, MaybeF.Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<bool> predicate, MaybeF.Handler handler) =>
		MaybeF.SomeIfAsync(predicate, @this, handler);

	/// <inheritdoc cref="MaybeF.SomeIfAsync{T}(Func{bool}, Task{T}, MaybeF.Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<T, bool> predicate, MaybeF.Handler handler) =>
		MaybeF.SomeIfAsync(predicate, @this, handler);
}
