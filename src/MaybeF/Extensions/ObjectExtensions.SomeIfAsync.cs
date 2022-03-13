// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF.Extensions;

public static partial class ObjectExtensions
{
	/// <inheritdoc cref="F.SomeIfAsync{T}(Func{bool}, Func{Task{T}}, F.Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<bool> predicate, F.Handler handler) =>
		F.SomeIfAsync(predicate, @this, handler);

	/// <inheritdoc cref="F.SomeIfAsync{T}(Func{bool}, Task{T}, F.Handler)"/>
	public static Task<Maybe<T>> SomeIfAsync<T>(this Func<Task<T>> @this, Func<T, bool> predicate, F.Handler handler) =>
		F.SomeIfAsync(predicate, @this, handler);
}
