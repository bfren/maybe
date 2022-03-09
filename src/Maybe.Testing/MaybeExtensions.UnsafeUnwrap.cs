// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace Maybe.Testing;

public static partial class MaybeExtensions
{
	/// <summary>
	/// Assume <paramref name="this"/> is a <see cref="Internals.Some{T}"/> and get the value -
	/// useful to get values during the Arrange section of a unit test
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="this">Maybe</param>
	public static T UnsafeUnwrap<T>(this Maybe<T> @this) =>
		@this.Unwrap(() => throw new Exceptions.UnsafeUnwrapException());
}
