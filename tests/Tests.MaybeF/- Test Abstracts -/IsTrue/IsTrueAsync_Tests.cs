// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts;

public abstract class IsTrueAsync_Tests
{
	public delegate Task<bool> IsTrue(Task<Maybe<bool>> maybe);

	public abstract Task Test00_Is_Some_Returns_Value();

	protected static async Task Test00(IsTrue act)
	{
		// Arrange
		var value = Rnd.Flip;
		var maybe = F.Some(value).AsTask;

		// Act
		var result = await act(maybe);

		// Assert
		Assert.Equal(value, result);
	}

	public abstract Task Test01_Is_None_Returns_False();

	protected static async Task Test01(IsTrue act)
	{
		// Arrange
		var maybe = Create.None<bool>().AsTask;

		// Act
		var result = await act(maybe);

		// Assert
		Assert.False(result);
	}
}
