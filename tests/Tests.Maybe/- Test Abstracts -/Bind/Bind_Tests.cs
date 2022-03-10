// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using Jeebs.Random;
using MaybeF;
using MaybeF.Exceptions;
using MaybeF.Testing;
using NSubstitute;
using Xunit;
using static MaybeF.F.R;

namespace Abstracts;

public abstract class Bind_Tests
{
	public abstract void Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason();

	protected static void Test00(Func<Maybe<int>, Func<int, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var bind = Substitute.For<Func<int, Maybe<string>>>();

		// Act
		var result = act(maybe, bind);

		// Assert
		var none = result.AssertNone();
		var reason = Assert.IsType<UnhandledExceptionReason>(none);
		_ = Assert.IsType<UnknownMaybeException>(reason.Value);
	}

	public abstract void Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionReason();

	protected static void Test01(Func<Maybe<int>, Func<int, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var exception = new Exception();
		var throwFunc = Maybe<string> () => throw exception;

		// Act
		var result = act(maybe, _ => throwFunc());

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(none);
	}

	public abstract void Test02_If_None_Gets_None();

	protected static void Test02(Func<Maybe<int>, Func<int, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var bind = Substitute.For<Func<int, Maybe<string>>>();

		// Act
		var result = act(maybe, bind);

		// Assert
		_ = result.AssertNone();
	}

	public abstract void Test03_If_None_With_Reason_Gets_None_With_Same_Reason();

	protected static void Test03(Func<Maybe<int>, Func<int, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var reason = new TestReason();
		var maybe = F.None<int>(reason);
		var bind = Substitute.For<Func<int, Maybe<string>>>();

		// Act
		var result = act(maybe, bind);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public abstract void Test04_If_Some_Runs_Bind_Function();

	protected static void Test04(Func<Maybe<int>, Func<int, Maybe<string>>, Maybe<string>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var bind = Substitute.For<Func<int, Maybe<string>>>();

		// Act
		_ = act(maybe, bind);

		// Assert
		_ = bind.Received().Invoke(value);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason : IReason;
}
