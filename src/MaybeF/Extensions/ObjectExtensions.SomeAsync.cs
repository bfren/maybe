// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF.Extensions;

public static partial class ObjectExtensions
{
	/// <inheritdoc cref="F.SomeAsync{T}(Func{Task{T}}, F.Handler)"/>
	public static Task<Maybe<T>> SomeAsync<T>(this Func<Task<T>> @this, F.Handler handler) =>
		F.SomeAsync(@this, handler);

	/// <inheritdoc cref="F.SomeAsync{T}(Func{Task{T}}, bool, F.Handler)"/>
	public static Task<Maybe<T?>> SomeAsync<T>(this Func<Task<T?>> @this, bool allowNull, F.Handler handler) =>
		F.SomeAsync(@this, allowNull, handler);
}
