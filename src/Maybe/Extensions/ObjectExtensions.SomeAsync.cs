// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe.Extensions;

public static partial class ObjectExtensions
{
	/// <inheritdoc cref="MaybeF.SomeAsync{T}(Func{Task{T}}, MaybeF.Handler)"/>
	public static Task<Maybe<T>> SomeAsync<T>(this Func<Task<T>> @this, MaybeF.Handler handler) =>
		MaybeF.SomeAsync(@this, handler);

	/// <inheritdoc cref="MaybeF.SomeAsync{T}(Func{Task{T}}, bool, MaybeF.Handler)"/>
	public static Task<Maybe<T?>> SomeAsync<T>(this Func<Task<T?>> @this, bool allowNull, MaybeF.Handler handler) =>
		MaybeF.SomeAsync(@this, allowNull, handler);
}
