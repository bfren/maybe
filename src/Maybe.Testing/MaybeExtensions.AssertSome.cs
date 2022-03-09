// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Maybe.Internals;
using Xunit;

namespace Maybe.Testing;

public static partial class MaybeExtensions
{
	/// <summary>
	/// Assert that <paramref name="this"/> is <see cref="Some{T}"/> and return the wrapped value
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="this">Maybe</param>
	public static T AssertSome<T>(this Maybe<T> @this) =>
		Assert.IsType<Some<T>>(@this).Value;
}
