// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF.Internals;

namespace MaybeF.Testing;

public static partial class MaybeExtensions
{
	/// <summary>
	/// Assert that <paramref name="this"/> is <see cref="None{T}"/> and return the Msg
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="this">Maybe</param>
	public static IMsg AssertNone<T>(this Maybe<T> @this) =>
		Assert.IsType<None<T>>(@this).Msg;
}
