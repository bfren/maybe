// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Maybe.Functions;

namespace Maybe.Extensions;

public static partial class ObjectExtensions
{
	/// <inheritdoc cref="MaybeF.Some{T}(T, bool)"/>
	public static Maybe<T> Some<T>(this T @this) =>
		MaybeF.Some(@this);

	/// <inheritdoc cref="MaybeF.Some{T}(T, bool)"/>
	public static Maybe<T?> Some<T>(this T @this, bool allowNull) =>
		MaybeF.Some(@this, allowNull);
}
