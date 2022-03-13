// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Testing;
using MaybeF.Testing.Exceptions;
using static MaybeF.F.R;

namespace Abstracts;

public abstract class SomeAsync_Tests
{
	public abstract Task Test00_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionReason();

	protected static async Task Test00(Func<Func<Task<int>>, F.Handler, Task<Maybe<int>>> act)
	{
		// Arrange
		var throwFunc = Task<int> () => throw new MaybeTestException();

		// Act
		var result = await act(throwFunc, F.DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(none);
	}

	public abstract Task Test01_Nullable_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionReason();

	protected static async Task Test01(Func<Func<Task<int?>>, bool, F.Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var throwFunc = Task<int?> () => throw new MaybeTestException();

		// Act
		var r0 = await act(throwFunc, true, F.DefaultHandler).ConfigureAwait(false);
		var r1 = await act(throwFunc, false, F.DefaultHandler).ConfigureAwait(false);

		// Assert
		var n0 = r0.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(n0);
		var n1 = r1.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(n1);
	}

	public abstract Task Test02_Exception_Thrown_With_Handler_Returns_None_Calls_Handler();

	protected static async Task Test02(Func<Func<Task<int>>, F.Handler, Task<Maybe<int>>> act)
	{
		// Arrange
		var handler = Substitute.For<F.Handler>();
		var exception = new Exception();
		var throwFunc = Task<int> () => throw exception;

		// Act
		var result = await act(throwFunc, handler).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
		_ = handler.Received().Invoke(exception);
	}

	public abstract Task Test03_Nullable_Exception_Thrown_With_Handler_Returns_None_Calls_Handler();

	protected static async Task Test03(Func<Func<Task<int?>>, bool, F.Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var handler = Substitute.For<F.Handler>();
		var exception = new Exception();
		var throwFunc = Task<int?> () => throw exception;

		// Act
		var r0 = await act(throwFunc, true, handler).ConfigureAwait(false);
		var r1 = await act(throwFunc, false, handler).ConfigureAwait(false);

		// Assert
		_ = r0.AssertNone();
		_ = r1.AssertNone();
		_ = handler.Received(2).Invoke(exception);
	}

	public abstract Task Test04_Null_Input_Returns_None();

	protected static async Task Test04(Func<Func<Task<int?>>, F.Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var value = Task<int?> () => Task.FromResult<int?>(null);

		// Act
		var result = await act(value, F.DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<NullValueReason>(none);
	}

	public abstract Task Test05_Nullable_Allow_Null_False_Null_Input_Returns_None_With_AllowNullWasFalseReason();

	protected static async Task Test05(Func<Func<Task<int?>>, bool, F.Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var value = Task<int?> () => Task.FromResult<int?>(null);

		// Act
		var result = await act(value, false, F.DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<AllowNullWasFalseReason>(none);
	}

	public abstract Task Test06_Nullable_Allow_Null_True_Null_Input_Returns_Some_With_Null_Value();

	protected static async Task Test06(Func<Func<Task<int?>>, bool, F.Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var value = Task<int?> () => Task.FromResult<int?>(null);

		// Act
		var result = await act(value, true, F.DefaultHandler).ConfigureAwait(false);

		// Assert
		var some = result.AssertSome();
		Assert.Null(some);
	}

	public abstract Task Test07_Not_Null_Returns_Some();

	protected static async Task Test07(Func<Func<Task<object>>, F.Handler, Task<Maybe<object>>> act)
	{
		// Arrange
		var v0 = Rnd.Str;
		var f0 = Task<object> () => Task.FromResult<object>(v0);

		var v1 = Rnd.Int;
		var f1 = Task<object> () => Task.FromResult<object>(v1);

		var v2 = Rnd.Guid;
		var f2 = Task<object> () => Task.FromResult<object>(v2);

		// Act
		var r0 = await act(f0, F.DefaultHandler).ConfigureAwait(false);
		var r1 = await act(f1, F.DefaultHandler).ConfigureAwait(false);
		var r2 = await act(f2, F.DefaultHandler).ConfigureAwait(false);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(v0, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(v1, s1);
		var s2 = r2.AssertSome();
		Assert.Equal(v2, s2);
	}

	public abstract Task Test08_Nullable_Not_Null_Returns_Some();

	protected static async Task Test08(Func<Func<Task<object?>>, bool, F.Handler, Task<Maybe<object?>>> act)
	{
		// Arrange
		var v0 = Rnd.Str;
		var f0 = Task<object?> () => Task.FromResult<object?>(v0);

		var v1 = Rnd.Int;
		var f1 = Task<object?> () => Task.FromResult<object?>(v1);

		var v2 = Rnd.Guid;
		var f2 = Task<object?> () => Task.FromResult<object?>(v2);

		// Act
		var r0 = await act(f0, false, F.DefaultHandler).ConfigureAwait(false);
		var r1 = await act(f1, false, F.DefaultHandler).ConfigureAwait(false);
		var r2 = await act(f2, false, F.DefaultHandler).ConfigureAwait(false);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(v0, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(v1, s1);
		var s2 = r2.AssertSome();
		Assert.Equal(v2, s2);
	}
}
