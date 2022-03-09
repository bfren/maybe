// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using Jeebs.Random;
using Maybe;
using Maybe.Functions;
using Maybe.Testing;
using NSubstitute;
using Xunit;

namespace Tests.Maybe.Abstracts;

public abstract class Unwrap_Tests
{
	public abstract void Test00_None_Runs_IfNone_Func_Returns_Value();

	protected static void Test00(Func<Maybe<int>, Func<int>, int> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = Create.None<int>();
		var ifNone = Substitute.For<Func<int>>();
		_ = ifNone.Invoke().Returns(value);

		// Act
		var result = act(maybe, ifNone);

		// Assert
		_ = ifNone.Received().Invoke();
		Assert.Equal(value, result);
	}

	public abstract void Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value();

	protected static void Test01(Func<Maybe<int>, Func<IReason, int>, int> act)
	{
		// Arrange
		var value = Rnd.Int;
		var reason = Substitute.For<IReason>();
		var maybe = MaybeF.None<int>(reason);
		var ifNone = Substitute.For<Func<IReason, int>>();
		_ = ifNone.Invoke(reason).Returns(value);

		// Act
		var result = act(maybe, ifNone);

		// Assert
		_ = ifNone.Received().Invoke(reason);
		Assert.Equal(value, result);
	}

	public abstract void Test02_Some_Returns_Value();

	protected static void Test02(Func<Maybe<int>, int> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = MaybeF.Some(value);

		// Act
		var result = act(maybe);

		// Assert
		Assert.Equal(value, result);
	}
}
