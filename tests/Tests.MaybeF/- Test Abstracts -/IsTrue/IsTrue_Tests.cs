// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts;

public abstract class IsTrue_Tests
{
	public delegate bool IsTrue(Maybe<bool> maybe);

	public abstract void Test00_Is_Some_Returns_Value();

	protected static void Test00(IsTrue act)
	{
		// Arrange
		var value = Rnd.Flip;
		var maybe = F.Some(value);

		// Act
		var result = act(maybe);

		// Assert
		Assert.Equal(value, result);
	}

	public abstract void Test01_Is_None_Returns_False();

	protected static void Test01(IsTrue act)
	{
		// Arrange
		var maybe = Create.None<bool>();

		// Act
		var result = act(maybe);

		// Assert
		Assert.False(result);
	}
}
