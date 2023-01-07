// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using static MaybeF.F.M;

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

	public abstract void Test03_Return_Void_If_None_And_None_Func_Is_Null_Throws_ArgumentNullException();

	protected static void Test03(Action<Maybe<int>, Action, Action<IMsg>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);

		// Act
		var action = () => act(maybe, null!, null!);

		// Assert
		Assert.Throws<ArgumentNullException>(action);
	}

	public abstract void Test04_Return_Void_If_None_Runs_None_Action_With_Msg();

	protected static void Test04(Action<Maybe<int>, Action<IMsg>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var none = Substitute.For<Action<IMsg>>();

		// Act
		act(maybe, none);

		// Assert
		none.Received().Invoke(message);
	}

	public abstract void Test05_Return_Value_If_None_And_None_Func_Is_Null_Throws_ArgumentNullException();

	protected static void Test05(Func<Maybe<int>, string, Func<string>, Func<IMsg, string>, string> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);

		// Act
		var action = () => act(maybe, null!, null!, null!);

		// Assert
		Assert.Throws<ArgumentNullException>(action);
	}

	public abstract void Test06_Return_Value_If_None_Runs_None_Func_With_Msg();

	protected static void Test06(Func<Maybe<int>, Func<IMsg, string>, string> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var none = Substitute.For<Func<IMsg, string>>();

		// Act
		act(maybe, none);

		// Assert
		none.Received().Invoke(message);
	}

	public abstract void Test07_Return_Void_If_Some_And_Some_Func_Is_Null_Throws_ArgumentNullException();

	protected static void Test07(Action<Maybe<int>, Action<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var action = () => act(maybe, null!);

		// Assert
		Assert.Throws<ArgumentNullException>(action);
	}

	public abstract void Test08_Return_Void_If_Some_Runs_Some_Action_With_Value();

	protected static void Test08(Action<Maybe<int>, Action<int>> act)
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

	public abstract void Test09_Return_Value_If_Some_And_Some_Func_Is_Null_Throws_ArgumentNullException();

	protected static void Test09(Func<Maybe<int>, Func<int, string>, string> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var action = () => act(maybe, null!);

		// Assert
		Assert.Throws<ArgumentNullException>(action);
	}

	public abstract void Test10_Return_Value_If_Some_Runs_Some_Func_With_Value();

	protected static void Test10(Func<Maybe<int>, Func<int, string>, string> act)
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

	public abstract void Test11_Return_Maybe_If_Some_Runs_Some_Func_With_Value();

	protected static void Test11(Func<Maybe<int>, Func<int, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var some = Substitute.For<Func<int, Maybe<string>>>();

		// Act
		act(maybe, some);

		// Assert
		some.Received().Invoke(value);
	}

	public abstract void Test12_Return_Maybe_If_None_Runs_None_Func();

	protected static void Test12(Func<Maybe<int>, Func<IMsg, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var none = Substitute.For<Func<IMsg, Maybe<string>>>();

		// Act
		act(maybe, none);

		// Assert
		none.Received().Invoke(message);
	}

	public abstract void Test13_Return_Maybe_If_Some_And_Some_Func_Is_Null_Returns_None_With_SomeFunctionCannotBeNullMsg();

	protected static void Test13(Func<Maybe<int>, Func<int, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var result = act(maybe, null!);

		// Assert
		result.AssertNone().AssertType<SomeFunctionCannotBeNullMsg>();
	}

	public abstract void Test14_Return_Maybe_If_None_And_None_Func_Is_Null_Returns_None_With_NoneFunctionCannotBeNullMsg();

	protected static void Test14(Func<Maybe<int>, Func<Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);

		// Act
		var result = act(maybe, null!);

		// Assert
		result.AssertNone().AssertType<NoneFunctionCannotBeNullMsg>();
	}

	public abstract void Test15_Return_Maybe_If_Unknown_Maybe_Returns_UnknownMaybeTypeMsg();

	protected static void Test15(Func<Maybe<int>, Maybe<string>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = act(maybe);

		// Assert
		var msg = result.AssertNone().AssertType<UnknownMaybeTypeMsg>();
		Assert.Equal(typeof(FakeMaybe), msg.MaybeType);
	}

	public abstract void Test16_If_Null_Returns_None_With_MaybeCannotBeNullMsg(Maybe<int> input);

	protected static void Test16(Func<Maybe<string>> act)
	{
		// Arrange

		// Act
		var result = act();

		// Assert
		result.AssertNone().AssertType<MaybeCannotBeNullMsg>();
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
