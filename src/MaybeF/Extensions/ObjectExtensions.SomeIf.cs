// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF.Extensions;

public static partial class ObjectExtensions
{
	/// <inheritdoc cref="F.SomeIf{T}(Func{bool}, Func{T}, F.Handler)"/>
	public static Maybe<T> SomeIf<T>(this T @this, Func<bool> predicate, F.Handler handler) =>
		F.SomeIf(predicate, @this, handler);

	/// <inheritdoc cref="F.SomeIf{T}(Func{bool}, Func{T}, F.Handler)"/>
	public static Maybe<T> SomeIf<T>(this T @this, Func<T, bool> predicate, F.Handler handler) =>
		F.SomeIf(() => predicate(@this), @this, handler);
}
