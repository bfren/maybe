// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;

namespace Abstracts;

public abstract class Switch_Tests
{
	public abstract void Test00_Return_Void_If_Unknown_Maybe_Throws_UnknownMaybeException();

	protected static void Test00(Action<Maybe<int>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = void () => act(maybe);

		// Assert
		Assert.Throws<UnknownMaybeException>(action);
	}

	public abstract void Test01_Return_Value_If_Unknown_Maybe_Throws_UnknownMaybeException();

	protected static void Test01(Func<Maybe<int>, string> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = void () => act(maybe);

		// Assert
		Assert.Throws<UnknownMaybeException>(action);
	}

	public abstract void Test02_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input);

	protected static void Test02(Func<string> act)
	{
		// Arrange

		// Act
		var action = void () => act();

		// Assert
		Assert.Throws<MaybeCannotBeNullException>(action);
	}

	public abstract void Test03_Return_Void_If_None_Runs_None_Action_With_Reason();

	protected static void Test03(Action<Maybe<int>, Action<IReason>> act)
	{
		// Arrange
		var reason = new TestReason();
		var maybe = F.None<int>(reason);
		var none = Substitute.For<Action<IReason>>();

		// Act
		act(maybe, none);

		// Assert
		none.Received().Invoke(reason);
	}

	public abstract void Test04_Return_Value_If_None_Runs_None_Func_With_Reason();

	protected static void Test04(Func<Maybe<int>, Func<IReason, string>, string> act)
	{
		// Arrange
		var reason = new TestReason();
		var maybe = F.None<int>(reason);
		var none = Substitute.For<Func<IReason, string>>();

		// Act
		act(maybe, none);

		// Assert
		none.Received().Invoke(reason);
	}

	public abstract void Test05_Return_Void_If_Some_Runs_Some_Action_With_Value();

	protected static void Test05(Action<Maybe<int>, Action<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var some = Substitute.For<Action<int>>();

		// Act
		act(maybe, some);

		// Assert
		some.Received().Invoke(value);
	}

	public abstract void Test06_Return_Value_If_Some_Runs_Some_Func_With_Value();

	protected static void Test06(Func<Maybe<int>, Func<int, string>, string> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var some = Substitute.For<Func<int, string>>();

		// Act
		act(maybe, some);

		// Assert
		some.Received().Invoke(value);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason : IReason;
}
