// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using Maybe.Functions;

namespace Maybe.Extensions;

public static partial class ObjectExtensions
{
	/// <inheritdoc cref="MaybeF.SomeIf{T}(Func{bool}, Func{T}, MaybeF.Handler)"/>
	public static Maybe<T> SomeIf<T>(this T @this, Func<bool> predicate, MaybeF.Handler handler) =>
		MaybeF.SomeIf(predicate, @this, handler);

	/// <inheritdoc cref="MaybeF.SomeIf{T}(Func{bool}, Func{T}, MaybeF.Handler)"/>
	public static Maybe<T> SomeIf<T>(this T @this, Func<T, bool> predicate, MaybeF.Handler handler) =>
		MaybeF.SomeIf(() => predicate(@this), @this, handler);
}
