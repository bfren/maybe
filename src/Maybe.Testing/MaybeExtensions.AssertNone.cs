// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Maybe.Internals;
using Xunit;

namespace Maybe.Testing;

public static partial class MaybeExtensions
{
	/// <summary>
	/// Assert that <paramref name="this"/> is <see cref="None{T}"/> and return the Reason
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="this">Maybe</param>
	public static IReason AssertNone<T>(this Maybe<T> @this) =>
		Assert.IsType<None<T>>(@this).Reason;
}
