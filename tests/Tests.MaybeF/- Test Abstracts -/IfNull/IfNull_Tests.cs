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

	public abstract void Test06_Some_With_Null__Runs_IfNull();

	protected static void Test06(Func<Maybe<int?>, Func<uint>, Func<int?, uint>, Maybe<uint>> act)
	{
		// Arrange
		var some = F.Some<int?>(() => null, true, F.DefaultHandler);
		var ifNull = Substitute.For<Func<uint>>();
		var ifSome = Substitute.For<Func<int?, uint>>();

		// Act
		act(some, ifNull, ifSome);

		// Assert
		ifNull.Received().Invoke();
		ifSome.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	public abstract void Test07_Some_With_Value__Runs_IfSome();

	protected static void Test07(Func<Maybe<string?>, Func<uint>, Func<string?, uint>, Maybe<uint>> act)
	{
		// Arrange
		var value = Rnd.Str;
		var some = F.Some<string?>(value);
		var ifNull = Substitute.For<Func<uint>>();
		var ifSome = Substitute.For<Func<string?, uint>>();

		// Act
		act(some, ifNull, ifSome);

		// Assert
		ifNull.DidNotReceiveWithAnyArgs().Invoke();
		ifSome.Received().Invoke(value);
	}

	public abstract void Test08_None_With_NullValueMsg__Runs_IfNull();

	protected static void Test08(Func<Maybe<string?>, Func<uint>, Func<string?, uint>, Maybe<uint>> act)
	{
		// Arrange
		var none = F.None<string?, NullValueMsg>();
		var ifNull = Substitute.For<Func<uint>>();
		var ifSome = Substitute.For<Func<string?, uint>>();

		// Act
		act(none, ifNull, ifSome);

		// Assert
		ifNull.Received().Invoke();
		ifSome.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	public abstract void Test09_None_With_Msg__Returns_None();

	protected static void Test09(Func<Maybe<string?>, Func<uint>, Func<string?, uint>, Maybe<uint>> act)
	{
		// Arrange
		var msg = new TestMsg();
		var maybe = F.None<string?>(msg);
		var ifNull = Substitute.For<Func<uint>>();
		var ifSome = Substitute.For<Func<string?, uint>>();

		// Act
		var result = act(maybe, ifNull, ifSome);

		// Assert
		var none = result.AssertNone();
		Assert.Same(msg, none);
		ifNull.DidNotReceiveWithAnyArgs().Invoke();
		ifSome.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	public abstract void Test10_Exception_In_IfNull__Uses_Handler();

	protected static void Test10(Func<Maybe<Guid?>, Func<string>, Func<Guid?, string>, F.Handler, Maybe<string>> act)
	{
		// Arrange
		var some = F.Some<Guid?>(() => null, true, F.DefaultHandler);
		var none = F.None<Guid?, NullValueMsg>();
		var ifNull = Substitute.For<Func<string>>();
		var message = Rnd.Str;
		var ex = new Exception(message);
		ifNull.Invoke()
			.Throws(ex);
		var ifSome = Substitute.For<Func<Guid?, string>>();
		var handler = Substitute.For<F.Handler>();

		// Act
		var r0 = act(some, ifNull, ifSome, handler);
		var r1 = act(none, ifNull, ifSome, handler);

		// Assert
		r0.AssertNone();
		r1.AssertNone();
		handler.Received(2).Invoke(ex);
	}

	public abstract void Test11_Exception_In_IfSome__Uses_Handler();

	protected static void Test11(Func<Maybe<Guid?>, Func<string>, Func<Guid?, string>, F.Handler, Maybe<string>> act)
	{
		// Arrange
		var value = Rnd.Guid;
		var some = F.Some<Guid?>(value);
		var ifNull = Substitute.For<Func<string>>();
		var ifSome = Substitute.For<Func<Guid?, string>>();
		var message = Rnd.Str;
		var ex = new Exception(message);
		ifSome.Invoke(value)
			.Throws(ex);
		var handler = Substitute.For<F.Handler>();

		// Act
		var result = act(some, ifNull, ifSome, handler);

		// Assert
		result.AssertNone();
		handler.Received().Invoke(ex);
	}

	public abstract void Test12_Exception_In_IfNull__Uses_DefaultHandler();

	protected static void Test12(Func<Maybe<Guid?>, Func<Maybe<string>>, Func<Guid?, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var some = F.Some<Guid?>(() => null, true, F.DefaultHandler);
		var none = F.None<Guid?, NullValueMsg>();
		var ifNull = Substitute.For<Func<Maybe<string>>>();
		ifNull.Invoke()
			.Throws(new Exception());
		var ifSome = Substitute.For<Func<Guid?, Maybe<string>>>();

		// Act
		var r0 = act(some, ifNull, ifSome);
		var r1 = act(none, ifNull, ifSome);

		// Assert
		r0.AssertNone().AssertType<UnhandledExceptionMsg>();
		r1.AssertNone().AssertType<UnhandledExceptionMsg>();
	}

	public abstract void Test13_Exception_In_IfSome__Uses_DefaultHandler();

	protected static void Test13(Func<Maybe<Guid?>, Func<Maybe<string>>, Func<Guid?, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var value = Rnd.Guid;
		var some = F.Some<Guid?>(value);
		var none = F.None<Guid?, NullValueMsg>();
		var ifNull = Substitute.For<Func<Maybe<string>>>();
		var ifSome = Substitute.For<Func<Guid?, Maybe<string>>>();
		ifSome.Invoke(value)
			.Throws(new Exception());

		// Act
		var result = act(some, ifNull, ifSome);

		// Assert
		result.AssertNone().AssertType<UnhandledExceptionMsg>();
	}

	public sealed record class TestMsg : IMsg;
}
