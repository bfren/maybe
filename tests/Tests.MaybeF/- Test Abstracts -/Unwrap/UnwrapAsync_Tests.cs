// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts;

public abstract class UnwrapAsync_Tests
{
	public abstract Task Test00_None_Runs_IfNone_Func_Returns_Value();

	protected static async Task Test00(Func<Task<Maybe<int>>, Func<int>, Task<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = Create.None<int>();
		var ifNone = Substitute.For<Func<int>>();
		ifNone.Invoke().Returns(value);

		// Act
		var result = await act(maybe.AsTask, ifNone).ConfigureAwait(false);

		// Assert
		ifNone.Received().Invoke();
		Assert.Equal(value, result);
	}

	public abstract Task Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value();

	protected static async Task Test01(Func<Task<Maybe<int>>, Func<IReason, int>, Task<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var reason = Substitute.For<IReason>();
		var maybe = F.None<int>(reason);
		var ifNone = Substitute.For<Func<IReason, int>>();
		ifNone.Invoke(reason).Returns(value);

		// Act
		var result = await act(maybe.AsTask, ifNone).ConfigureAwait(false);

		// Assert
		ifNone.Received().Invoke(reason);
		Assert.Equal(value, result);
	}

	public abstract Task Test02_Some_Returns_Value();

	protected static async Task Test02(Func<Task<Maybe<int>>, Task<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var result = await act(maybe.AsTask).ConfigureAwait(false);

		// Assert
		Assert.Equal(value, result);
	}
}
