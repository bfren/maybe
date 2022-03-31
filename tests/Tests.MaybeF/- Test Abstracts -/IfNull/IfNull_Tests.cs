// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using NSubstitute.ExceptionExtensions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class IfNull_Tests
{
	public abstract void Test00_Exception_In_IfNull_Func_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test00(Func<Maybe<object?>, Func<Maybe<object?>>, Maybe<object?>> act)
	{
		// Arrange
		var some = F.Some<object>(null, true);
		var none = F.None<object?, NullValueMsg>();
		var throws = Substitute.For<Func<Maybe<object?>>>();
		throws.Invoke().Throws<Exception>();

		// Act
		var r0 = act(some, throws);
		var r1 = act(none, throws);

		// Assert
		var n0 = r0.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(n0);
		var n1 = r1.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(n1);
	}

	public abstract void Test01_Some_With_Null_Value_Runs_IfNull_Func();

	protected static void Test01(Func<Maybe<object?>, Func<Maybe<object?>>, Maybe<object?>> act)
	{
		// Arrange
		var maybe = F.Some<object>(null, true);
		var ifNull = Substitute.For<Func<Maybe<object?>>>();

		// Act
		act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
	}

	public abstract void Test02_None_With_NullValueMsg_Runs_IfNull_Func();

	protected static void Test02(Func<Maybe<object>, Func<Maybe<object>>, Maybe<object>> act)
	{
		// Arrange
		var maybe = F.None<object, NullValueMsg>();
		var ifNull = Substitute.For<Func<Maybe<object>>>();

		// Act
		act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
	}

	public abstract void Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Msg();

	protected static void Test03(Func<Maybe<object?>, Func<IMsg>, Maybe<object?>> act)
	{
		// Arrange
		var maybe = F.Some<object>(null, true);
		var ifNull = Substitute.For<Func<IMsg>>();
		var message = new TestMsg();
		ifNull.Invoke().Returns(message);

		// Act
		var result = act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(message, none);
	}

	public abstract void Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Msg();

	protected static void Test04(Func<Maybe<object>, Func<IMsg>, Maybe<object>> act)
	{
		// Arrange
		var maybe = F.None<object, NullValueMsg>();
		var ifNull = Substitute.For<Func<IMsg>>();
		var message = new TestMsg();
		ifNull.Invoke().Returns(message);

		// Act
		var result = act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(message, none);
	}

	public abstract void Test05_Null_Maybe_Runs_IfNull_Func(Maybe<int> input);

	protected static void Test05(Func<Func<Maybe<int>>, Maybe<int>> act)
	{
		// Arrange
		var ifNull = Substitute.For<Func<Maybe<int>>>();

		// Act
		act(ifNull);

		// Assert
		ifNull.Received().Invoke();
	}

	public sealed record class TestMsg : IMsg;
}
