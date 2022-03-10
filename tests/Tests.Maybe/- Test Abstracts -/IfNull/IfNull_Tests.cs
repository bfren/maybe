// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using MaybeF;
using MaybeF.Testing;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static MaybeF.F.R;

namespace Abstracts;

public abstract class IfNull_Tests
{
	public abstract void Test00_Exception_In_IfNull_Func_Returns_None_With_UnhandledExceptionReason();

	protected static void Test00(Func<Maybe<object?>, Func<Maybe<object?>>, Maybe<object?>> act)
	{
		// Arrange
		var some = F.Some<object>(null, true);
		var none = F.None<object?, NullValueReason>();
		var throws = Substitute.For<Func<Maybe<object?>>>();
		_ = throws.Invoke().Throws<Exception>();

		// Act
		var r0 = act(some, throws);
		var r1 = act(none, throws);

		// Assert
		var n0 = r0.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(n0);
		var n1 = r1.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(n1);
	}

	public abstract void Test01_Some_With_Null_Value_Runs_IfNull_Func();

	protected static void Test01(Func<Maybe<object?>, Func<Maybe<object?>>, Maybe<object?>> act)
	{
		// Arrange
		var maybe = F.Some<object>(null, true);
		var ifNull = Substitute.For<Func<Maybe<object?>>>();

		// Act
		_ = act(maybe, ifNull);

		// Assert
		_ = ifNull.Received().Invoke();
	}

	public abstract void Test02_None_With_NullValueReason_Runs_IfNull_Func();

	protected static void Test02(Func<Maybe<object>, Func<Maybe<object>>, Maybe<object>> act)
	{
		// Arrange
		var maybe = F.None<object, NullValueReason>();
		var ifNull = Substitute.For<Func<Maybe<object>>>();

		// Act
		_ = act(maybe, ifNull);

		// Assert
		_ = ifNull.Received().Invoke();
	}

	public abstract void Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason();

	protected static void Test03(Func<Maybe<object?>, Func<IReason>, Maybe<object?>> act)
	{
		// Arrange
		var maybe = F.Some<object>(null, true);
		var ifNull = Substitute.For<Func<IReason>>();
		var reason = new TestReason();
		_ = ifNull.Invoke().Returns(reason);

		// Act
		var result = act(maybe, ifNull);

		// Assert
		_ = ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public abstract void Test04_None_With_NullValueReason_Runs_IfNull_Func_Returns_None_With_Reason();

	protected static void Test04(Func<Maybe<object>, Func<IReason>, Maybe<object>> act)
	{
		// Arrange
		var maybe = F.None<object, NullValueReason>();
		var ifNull = Substitute.For<Func<IReason>>();
		var reason = new TestReason();
		_ = ifNull.Invoke().Returns(reason);

		// Act
		var result = act(maybe, ifNull);

		// Assert
		_ = ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public sealed record class TestReason : IReason;
}
