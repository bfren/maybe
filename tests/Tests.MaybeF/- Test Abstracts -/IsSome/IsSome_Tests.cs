// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts;

public abstract class IsSome_Tests
{
	public delegate bool IsSome<T>(Maybe<T> maybe, out T value);

	public abstract void Test00_Is_Some_Returns_True_Sets_Value();

	protected static void Test00(IsSome<int> act)
	{
		// Arrange
		var inValue = Rnd.Int;
		var maybe = F.Some(inValue);

		// Act
		var result = act(maybe, out int outValue);

		// Assert
		Assert.True(result);
		Assert.Equal(inValue, outValue);
	}

	public abstract void Test01_Is_Not_Some_Returns_False();

	protected static void Test01(IsSome<string> act)
	{
		// Arrange
		var maybe = Create.None<string>();

		// Act
		var result = act(maybe, out string outValue);

		// Assert
		Assert.False(result);
		Assert.Null(outValue);
	}
}
