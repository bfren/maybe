// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts;

public abstract class IsFalse_Tests
{
	public delegate bool IsFalse(Maybe<bool> maybe);

	public abstract void Test00_Is_Some_Returns_Opposite_Of_Value();

	protected static void Test00(IsFalse act)
	{
		// Arrange
		var value = Rnd.Flip;
		var maybe = F.Some(value);

		// Act
		var result = act(maybe);

		// Assert
		Assert.Equal(!value, result);
	}

	public abstract void Test01_Is_None_Returns_False();

	protected static void Test01(IsFalse act)
	{
		// Arrange
		var maybe = Create.None<bool>();

		// Act
		var result = act(maybe);

		// Assert
		Assert.False(result);
	}
}
