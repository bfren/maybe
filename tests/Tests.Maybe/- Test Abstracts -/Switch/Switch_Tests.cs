// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using Jeebs.Random;
using Maybe;
using Maybe.Exceptions;
using Maybe.Functions;
using NSubstitute;
using Xunit;

namespace Tests.Maybe.Abstracts;

public abstract class Switch_Tests
{
	public abstract void Test00_Return_Void_If_Unknown_Maybe_Throws_UnknownOptionException();

	protected static void Test00(Action<Maybe<int>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = void () => act(maybe);

		// Assert
		_ = Assert.Throws<UnknownMaybeException>(action);
	}

	public abstract void Test01_Return_Value_If_Unknown_Maybe_Throws_UnknownOptionException();

	protected static void Test01(Func<Maybe<int>, string> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = void () => act(maybe);

		// Assert
		_ = Assert.Throws<UnknownMaybeException>(action);
	}

	public abstract void Test02_Return_Void_If_None_Runs_None_Action_With_Reason();

	protected static void Test02(Action<Maybe<int>, Action<IReason>> act)
	{
		// Arrange
		var reason = new TestReason();
		var maybe = MaybeF.None<int>(reason);
		var none = Substitute.For<Action<IReason>>();

		// Act
		act(maybe, none);

		// Assert
		none.Received().Invoke(reason);
	}

	public abstract void Test03_Return_Value_If_None_Runs_None_Func_With_Reason();

	protected static void Test03(Func<Maybe<int>, Func<IReason, string>, string> act)
	{
		// Arrange
		var reason = new TestReason();
		var maybe = MaybeF.None<int>(reason);
		var none = Substitute.For<Func<IReason, string>>();

		// Act
		_ = act(maybe, none);

		// Assert
		_ = none.Received().Invoke(reason);
	}

	public abstract void Test04_Return_Void_If_Some_Runs_Some_Action_With_Value();

	protected static void Test04(Action<Maybe<int>, Action<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = MaybeF.Some(value);
		var some = Substitute.For<Action<int>>();

		// Act
		act(maybe, some);

		// Assert
		some.Received().Invoke(value);
	}

	public abstract void Test05_Return_Value_If_Some_Runs_Some_Func_With_Value();

	protected static void Test05(Func<Maybe<int>, Func<int, string>, string> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = MaybeF.Some(value);
		var some = Substitute.For<Func<int, string>>();

		// Act
		_ = act(maybe, some);

		// Assert
		_ = some.Received().Invoke(value);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason : IReason;
}
