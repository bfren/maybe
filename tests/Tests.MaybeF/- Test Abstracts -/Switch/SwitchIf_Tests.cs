// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Testing.Exceptions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class SwitchIf_Tests
{
	public abstract void Test00_Unknown_Maybe_Returns_None_With_UnknownMaybeTypeMsg();

	protected static void Test00(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var check = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(maybe, check);

		// Assert
		var msg = result.AssertNone().AssertType<UnknownMaybeTypeMsg>();
		Assert.Equal(typeof(FakeMaybe), msg.MaybeType);
	}

	public abstract void Test01_If_Null_Returns_None_With_MaybeCannotBeNullMsg(Maybe<int> input);

	protected static void Test01(Func<Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var check = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(check);

		// Assert
		result.AssertNone().AssertType<MaybeCannotBeNullMsg>();
	}

	public abstract void Test02_Predicate_Null_Returns_None_With_SwitchIfPredicateCannotBeNullMsg();

	protected static void Test02(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Create.None<int>();

		// Act
		var result = act(maybe, null!);

		// Assert
		result.AssertNone().AssertType<SwitchIfPredicateCannotBeNullMsg>();
	}

	public abstract void Test03_None_Returns_Original_None();

	protected static void Test03(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var check = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(maybe, check);

		// Assert
		result.AssertNone();
		Assert.Same(maybe, result);
	}

	public abstract void Test04_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

	protected static void Test04(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = bool (int _) => throw new MaybeTestException();

		// Act
		var result = act(maybe, check);

		// Assert
		result.AssertNone().AssertType<SwitchIfFuncExceptionMsg>();
	}

	public abstract void Test05_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Maybe();

	protected static void Test05(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(Arg.Any<int>()).Returns(true);

		// Act
		var result = act(maybe, check);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract void Test06_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Maybe();

	protected static void Test06(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(Arg.Any<int>()).Returns(false);

		// Act
		var result = act(maybe, check);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract void Test07_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

	protected static void Test07(Func<Maybe<int>, Func<int, bool>, Func<int, None<int>>, Maybe<int>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(Arg.Any<int>()).Returns(true);
		var ifTrue = None<int> (int _) => throw new MaybeTestException();

		// Act
		var result = act(maybe, check, ifTrue);

		// Assert
		result.AssertNone().AssertType<SwitchIfFuncExceptionMsg>();
	}

	public abstract void Test08_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

	protected static void Test08(Func<Maybe<int>, Func<int, bool>, Func<int, None<int>>, Maybe<int>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(Arg.Any<int>()).Returns(false);
		var ifFalse = None<int> (int _) => throw new MaybeTestException();

		// Act
		var result = act(maybe, check, ifFalse);

		// Assert
		result.AssertNone().AssertType<SwitchIfFuncExceptionMsg>();
	}

	public abstract void Test09_Check_Returns_True_Runs_IfTrue_Returns_Value();

	protected static void Test09(Func<Maybe<int>, Func<int, bool>, Func<int, Maybe<int>>, Maybe<int>> act)
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var maybe = F.Some(v0);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(v0).Returns(true);
		var ifTrue = Substitute.For<Func<int, Maybe<int>>>();
		ifTrue.Invoke(v0).Returns(F.Some(v0 + v1));

		// Act
		var result = act(maybe, check, ifTrue);

		// Assert
		ifTrue.Received().Invoke(v0);
		var some = result.AssertSome();
		Assert.Equal(v0 + v1, some);
	}

	public abstract void Test10_Check_Returns_False_Runs_IfFalse_Returns_Value();

	protected static void Test10(Func<Maybe<int>, Func<int, bool>, Func<int, None<int>>, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(value).Returns(false);
		var ifFalse = Substitute.For<Func<int, None<int>>>();
		ifFalse.Invoke(value).Returns(F.None<int, TestMsg>());

		// Act
		var result = act(maybe, check, ifFalse);

		// Assert
		ifFalse.Received().Invoke(value);
		result.AssertNone().AssertType<TestMsg>();
	}

	public abstract void Test11_Is_Some__Returns_Result_Of_Check();

	protected static void Test11(Func<Maybe<int>, Func<int, bool>, bool> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var value = Rnd.Flip;
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(default)
			.ReturnsForAnyArgs(value);

		// Act
		var result = act(maybe, check);

		// Assert
		Assert.Equal(value, result);
	}

	public abstract void Test12_Is_None__Returns_False();

	protected static void Test12(Func<Maybe<int>, Func<int, bool>, bool> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var check = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(maybe, check);

		// Assert
		Assert.False(result);
		check.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	public record class FakeMaybe : Maybe<int> { }

	public sealed record class TestMsg : IMsg;
}
