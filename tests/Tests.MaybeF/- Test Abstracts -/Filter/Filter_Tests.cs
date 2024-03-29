// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class Filter_Tests
{
	public abstract void Test00_If_Unknown_Maybe_Returns_None_With_UnknownMaybeTypeMsg();

	protected static void Test00(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = act(maybe);

		// Assert
		var msg = result.AssertNone().AssertType<UnknownMaybeTypeMsg>();
		Assert.Equal(typeof(FakeMaybe), msg.MaybeType);
	}

	public abstract void Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test01(Func<Maybe<string>, Func<string, bool>, Maybe<string>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Str);
		var exception = new Exception();
		var throwFunc = bool (string _) => throw exception;

		// Act
		var result = act(maybe, throwFunc);

		// Assert
		result.AssertNone().AssertType<UnhandledExceptionMsg>();
	}

	public abstract void Test02_When_Some_And_Predicate_True_Returns_Value();

	protected static void Test02(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var predicate = Substitute.For<Func<int, bool>>();
		predicate.Invoke(Arg.Any<int>()).Returns(true);

		// Act
		var result = act(maybe, predicate);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract void Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg();

	protected static void Test03(Func<Maybe<string>, Func<string, bool>, Maybe<string>> act)
	{
		// Arrange
		var value = Rnd.Str;
		var maybe = F.Some(value);
		var predicate = Substitute.For<Func<string, bool>>();
		predicate.Invoke(Arg.Any<string>()).Returns(false);

		// Act
		var result = act(maybe, predicate);

		// Assert
		result.AssertNone().AssertType<FilterPredicateWasFalseMsg>();
	}

	public abstract void Test04_When_None_Returns_None_With_Original_Msg();

	protected static void Test04(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var predicate = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(maybe, predicate);

		// Assert
		var none = result.AssertNone();
		Assert.Same(message, none);
		predicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
