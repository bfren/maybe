// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Extensions;

public static partial class ObjectExtensions
{
	/// <inheritdoc cref="F.Some{T}(T, bool)"/>
	public static Maybe<T> Some<T>(this T @this) =>
		F.Some<T>(@this);

	/// <inheritdoc cref="F.Some{T}(T, bool)"/>
	public static Maybe<T?> Some<T>(this T @this, bool allowNull) =>
		F.Some(@this, allowNull);
}
