// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using MaybeF;
using MaybeF.Testing;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static MaybeF.F.R;

namespace Abstracts;

public abstract class IfNullAsync_Tests
{
	public abstract Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionReason();

	protected static async Task Test00(Func<Maybe<object?>, Func<Task<Maybe<object?>>>, Task<Maybe<object?>>> act)
	{
		// Arrange
		var some = F.Some<object>(null, true);
		var none = F.None<object?, NullValueReason>();
		var throws = Substitute.For<Func<Task<Maybe<object?>>>>();
		_ = throws.Invoke().Throws<Exception>();

		// Act
		var r0 = await act(some, throws).ConfigureAwait(false);
		var r1 = await act(none, throws).ConfigureAwait(false);

		// Assert
		var n0 = r0.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(n0);
		var n1 = r1.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(n1);
	}

	public abstract Task Test01_Some_With_Null_Value_Runs_IfNull_Func();

	protected static async Task Test01(Func<Maybe<object?>, Func<Task<Maybe<object?>>>, Task<Maybe<object?>>> act)
	{
		// Arrange
		var some = F.Some<object>(null, true);
		var ifNull = Substitute.For<Func<Task<Maybe<object?>>>>();

		// Act
		_ = await act(some, ifNull).ConfigureAwait(false);

		// Assert
		_ = await ifNull.Received().Invoke().ConfigureAwait(false);
	}

	public abstract Task Test02_None_With_NullValueReason_Runs_IfNull_Func();

	protected static async Task Test02(Func<Maybe<object>, Func<Task<Maybe<object>>>, Task<Maybe<object>>> act)
	{
		// Arrange
		var none = F.None<object, NullValueReason>();
		var ifNull = Substitute.For<Func<Task<Maybe<object>>>>();

		// Act
		_ = await act(none, ifNull).ConfigureAwait(false);

		// Assert
		_ = await ifNull.Received().Invoke().ConfigureAwait(false);
	}

	public abstract Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason();

	protected static async Task Test03(Func<Maybe<object?>, Func<IReason>, Task<Maybe<object?>>> act)
	{
		// Arrange
		var maybe = F.Some<object>(null, true);
		var ifNull = Substitute.For<Func<IReason>>();
		var reason = new TestReason();
		_ = ifNull.Invoke().Returns(reason);

		// Act
		var result = await act(maybe, ifNull).ConfigureAwait(false);

		// Assert
		_ = ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public abstract Task Test04_None_With_NullValueReason_Runs_IfNull_Func_Returns_None_With_Reason();

	protected static async Task Test04(Func<Maybe<object>, Func<IReason>, Task<Maybe<object>>> act)
	{
		// Arrange
		var maybe = F.None<object, NullValueReason>();
		var ifNull = Substitute.For<Func<IReason>>();
		var reason = new TestReason();
		_ = ifNull.Invoke().Returns(reason);

		// Act
		var result = await act(maybe, ifNull).ConfigureAwait(false);

		// Assert
		_ = ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public sealed record class TestReason : IReason;
}
