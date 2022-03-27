// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing;

public static partial class MaybeExtensions
{
	/// <summary>
	/// Assert that <paramref name="this"/> is <see cref="Internals.Some{T}"/> and the value is false
	/// </summary>
	/// <param name="this">Maybe</param>
	public static void AssertFalse(this Maybe<bool> @this) =>
		Assert.False(@this.AssertSome());
}
