// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using NSubstitute.ExceptionExtensions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class IfNullAsync_Tests
{
	public abstract Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Maybe<object?>, Func<Task<Maybe<object?>>>, Task<Maybe<object?>>> act)
	{
		// Arrange
		var some = F.Some<object>(null, true);
		var none = F.None<object?, NullValueMsg>();
		var throws = Substitute.For<Func<Task<Maybe<object?>>>>();
		throws.Invoke().ThrowsAsync<Exception>();

		// Act
		var r0 = await act(some, throws);
		var r1 = await act(none, throws);

		// Assert
		var n0 = r0.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(n0);
		var n1 = r1.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(n1);
	}

	public abstract Task Test01_Some_With_Null_Value_Runs_IfNull_Func();

	protected static async Task Test01(Func<Maybe<object?>, Func<Task<Maybe<object?>>>, Task<Maybe<object?>>> act)
	{
		// Arrange
		var some = F.Some<object>(null, true);
		var ifNull = Substitute.For<Func<Task<Maybe<object?>>>>();

		// Act
		await act(some, ifNull);

		// Assert
		await ifNull.Received().Invoke();
	}

	public abstract Task Test02_None_With_NullValueMsg_Runs_IfNull_Func();

	protected static async Task Test02(Func<Maybe<object>, Func<Task<Maybe<object>>>, Task<Maybe<object>>> act)
	{
		// Arrange
		var none = F.None<object, NullValueMsg>();
		var ifNull = Substitute.For<Func<Task<Maybe<object>>>>();

		// Act
		await act(none, ifNull);

		// Assert
		await ifNull.Received().Invoke();
	}

	public abstract Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Msg();

	protected static async Task Test03(Func<Maybe<object?>, Func<IMsg>, Task<Maybe<object?>>> act)
	{
		// Arrange
		var maybe = F.Some<object>(null, true);
		var ifNull = Substitute.For<Func<IMsg>>();
		var message = new TestMsg();
		ifNull.Invoke().Returns(message);

		// Act
		var result = await act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(message, none);
	}

	public abstract Task Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Msg();

	protected static async Task Test04(Func<Maybe<object>, Func<IMsg>, Task<Maybe<object>>> act)
	{
		// Arrange
		var maybe = F.None<object, NullValueMsg>();
		var ifNull = Substitute.For<Func<IMsg>>();
		var message = new TestMsg();
		ifNull.Invoke().Returns(message);

		// Act
		var result = await act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(message, none);
	}

	public abstract Task Test05_Null_Maybe_Runs_IfNull_Func(Maybe<int> input);

	protected static async Task Test05(Func<Func<Task<Maybe<int>>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var ifNull = Substitute.For<Func<Task<Maybe<int>>>>();

		// Act
		await act(ifNull);

		// Assert
		await ifNull.Received().Invoke();
	}

	public abstract Task Test06_Some_With_Null__Runs_IfNull();

	protected static async Task Test06(Func<Maybe<string?>, Func<Task<uint>>, Func<string?, Task<uint>>, Task<Maybe<uint>>> act)
	{
		// Arrange
		var some = F.Some<string?>(() => null, true, F.DefaultHandler);
		var ifNull = Substitute.For<Func<Task<uint>>>();
		var ifSome = Substitute.For<Func<string?, Task<uint>>>();

		// Act
		await act(some, ifNull, ifSome);

		// Assert
		await ifNull.Received().Invoke();
		await ifSome.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	public abstract Task Test07_Some_With_Value__Runs_IfSome();

	protected static async Task Test07(Func<Maybe<string?>, Func<Task<uint>>, Func<string?, Task<uint>>, Task<Maybe<uint>>> act)
	{
		// Arrange
		var value = Rnd.Str;
		var some = F.Some<string?>(value);
		var ifNull = Substitute.For<Func<Task<uint>>>();
		var ifSome = Substitute.For<Func<string?, Task<uint>>>();

		// Act
		await act(some, ifNull, ifSome);

		// Assert
		await ifNull.DidNotReceiveWithAnyArgs().Invoke();
		await ifSome.Received().Invoke(value);
	}

	public abstract Task Test08_None_With_NullValueMsg__Runs_IfNull();

	protected static async Task Test08(Func<Maybe<string?>, Func<Task<uint>>, Func<string?, Task<uint>>, Task<Maybe<uint>>> act)
	{
		// Arrange
		var none = F.None<string?, NullValueMsg>();
		var ifNull = Substitute.For<Func<Task<uint>>>();
		var ifSome = Substitute.For<Func<string?, Task<uint>>>();

		// Act
		await act(none, ifNull, ifSome);

		// Assert
		await ifNull.Received().Invoke();
		await ifSome.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	public abstract Task Test09_None_With_Msg__Returns_None();

	protected static async Task Test09(Func<Maybe<string?>, Func<Task<uint>>, Func<string?, Task<uint>>, Task<Maybe<uint>>> act)
	{
		// Arrange
		var msg = new TestMsg();
		var maybe = F.None<string?>(msg);
		var ifNull = Substitute.For<Func<Task<uint>>>();
		var ifSome = Substitute.For<Func<string?, Task<uint>>>();

		// Act
		var result = await act(maybe, ifNull, ifSome);

		// Assert
		var none = result.AssertNone();
		Assert.Same(msg, none);
		await ifNull.DidNotReceiveWithAnyArgs().Invoke();
		await ifSome.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	public abstract Task Test10_Exception_In_IfNull__Uses_Handler();

	protected static async Task Test10(Func<Maybe<Guid?>, Func<Task<string>>, Func<Guid?, Task<string>>, F.Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var some = F.Some<Guid?>(() => null, true, F.DefaultHandler);
		var none = F.None<Guid?, NullValueMsg>();
		var ifNull = Substitute.For<Func<Task<string>>>();
		var message = Rnd.Str;
		var ex = new Exception(message);
		ifNull.Invoke().ThrowsAsync(ex);
		var ifSome = Substitute.For<Func<Guid?, Task<string>>>();
		var handler = Substitute.For<F.Handler>();

		// Act
		var r0 = await act(some, ifNull, ifSome, handler);
		var r1 = await act(none, ifNull, ifSome, handler);

		// Assert
		r0.AssertNone();
		r1.AssertNone();
		handler.Received(2).Invoke(ex);
	}

	public abstract Task Test11_Exception_In_IfSome__Uses_Handler();

	protected static async Task Test11(Func<Maybe<Guid?>, Func<Task<string>>, Func<Guid?, Task<string>>, F.Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Guid;
		var some = F.Some<Guid?>(value);
		var ifNull = Substitute.For<Func<Task<string>>>();
		var ifSome = Substitute.For<Func<Guid?, Task<string>>>();
		var message = Rnd.Str;
		var ex = new Exception(message);
		ifSome.Invoke(value).ThrowsAsync(ex);
		var handler = Substitute.For<F.Handler>();

		// Act
		var result = await act(some, ifNull, ifSome, handler);

		// Assert
		result.AssertNone();
		handler.Received().Invoke(ex);
	}

	public abstract Task Test12_Exception_In_IfNull__Uses_DefaultHandler();

	protected static async Task Test12(Func<Maybe<Guid?>, Func<Task<Maybe<string>>>, Func<Guid?, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var some = F.Some<Guid?>(() => null, true, F.DefaultHandler);
		var none = F.None<Guid?, NullValueMsg>();
		var ifNull = Substitute.For<Func<Task<Maybe<string>>>>();
		ifNull.Invoke().ThrowsAsync(new Exception());
		var ifSome = Substitute.For<Func<Guid?, Task<Maybe<string>>>>();

		// Act
		var r0 = await act(some, ifNull, ifSome);
		var r1 = await act(none, ifNull, ifSome);

		// Assert
		r0.AssertNone().AssertType<UnhandledExceptionMsg>();
		r1.AssertNone().AssertType<UnhandledExceptionMsg>();
	}

	public abstract Task Test13_Exception_In_IfSome__Uses_DefaultHandler();

	protected static async Task Test13(Func<Maybe<Guid?>, Func<Task<Maybe<string>>>, Func<Guid?, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Guid;
		var some = F.Some<Guid?>(value);
		var none = F.None<Guid?, NullValueMsg>();
		var ifNull = Substitute.For<Func<Task<Maybe<string>>>>();
		var ifSome = Substitute.For<Func<Guid?, Task<Maybe<string>>>>();
		ifSome.Invoke(value).ThrowsAsync(new Exception());

		// Act
		var result = await act(some, ifNull, ifSome);

		// Assert
		result.AssertNone().AssertType<UnhandledExceptionMsg>();
	}

	public sealed record class TestMsg : IMsg;
}
