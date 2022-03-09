// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using Jeebs.Random;
using Maybe;
using Maybe.Functions;
using Maybe.Testing;
using Maybe.Testing.Exceptions;
using NSubstitute;
using Xunit;
using static Maybe.Functions.MaybeF.R;

namespace Tests.Maybe.Abstracts;

public abstract class IfSome_Tests
{
	public abstract void Test00_Exception_In_IfSome_Action_Returns_None_With_UnhandledExceptionReason();

	protected static void Test00(Func<Maybe<int>, Action<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = MaybeF.Some(Rnd.Int);
		var ifSome = void (int _) => throw new MaybeTestException();

		// Act
		var result = act(maybe, ifSome);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(none);
	}

	public abstract void Test01_None_Returns_Original_Option();

	protected static void Test01(Func<Maybe<int>, Action<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var ifSome = Substitute.For<Action<int>>();

		// Act
		var result = act(maybe, ifSome);

		// Assert
		Assert.Same(maybe, result);
		ifSome.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	public abstract void Test02_Some_Runs_IfSome_Action_And_Returns_Original_Option();

	protected static void Test02(Func<Maybe<int>, Action<int>, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = MaybeF.Some(value);
		var ifSome = Substitute.For<Action<int>>();

		// Act
		var result = act(maybe, ifSome);

		// Assert
		Assert.Same(maybe, result);
		ifSome.Received().Invoke(value);
	}
}
