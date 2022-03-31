// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts;

public abstract class Unwrap_Tests
{
	public abstract void Test00_None_Runs_IfNone_Func_Returns_Value();

	protected static void Test00(Func<Maybe<int>, Func<int>, int> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = Create.None<int>();
		var ifNone = Substitute.For<Func<int>>();
		ifNone.Invoke().Returns(value);

		// Act
		var result = act(maybe, ifNone);

		// Assert
		ifNone.Received().Invoke();
		Assert.Equal(value, result);
	}

	public abstract void Test01_None_With_Msg_Runs_IfNone_Func_Passes_Msg_Returns_Value();

	protected static void Test01(Func<Maybe<int>, Func<IMsg, int>, int> act)
	{
		// Arrange
		var value = Rnd.Int;
		var message = Substitute.For<IMsg>();
		var maybe = F.None<int>(message);
		var ifNone = Substitute.For<Func<IMsg, int>>();
		ifNone.Invoke(message).Returns(value);

		// Act
		var result = act(maybe, ifNone);

		// Assert
		ifNone.Received().Invoke(message);
		Assert.Equal(value, result);
	}

	public abstract void Test02_Some_Returns_Value();

	protected static void Test02(Func<Maybe<int>, int> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var result = act(maybe);

		// Assert
		Assert.Equal(value, result);
	}
}
