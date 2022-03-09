// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using Jeebs.Random;
using Maybe.Testing;
using NSubstitute;
using Xunit;
using static Maybe.Functions.MaybeF.R;

namespace Maybe.Functions.MaybeF_Tests;

public class Catch_Tests
{
	[Fact]
	public void Executes_Chain()
	{
		// Arrange
		var value = Rnd.Int;

		// Act
		var result = MaybeF.Catch(() => MaybeF.Some(value), MaybeF.DefaultHandler);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	[Fact]
	public void Catches_Exception_Without_Handler()
	{
		// Arrange
		var message = Rnd.Str;

		// Act
		var result = MaybeF.Catch<int>(() => throw new Exception(message), MaybeF.DefaultHandler);

		// Assert
		var none = result.AssertNone();
		var ex = Assert.IsType<UnhandledExceptionReason>(none);
		Assert.Contains(message, ex.ToString());
	}

	[Fact]
	public void Catches_Exception_With_Handler()
	{
		// Arrange
		var message = Rnd.Str;
		var exception = new Exception(message);
		var handler = Substitute.For<MaybeF.Handler>();

		// Act
		var result = MaybeF.Catch<int>(() => throw exception, handler);

		// Assert
		_ = result.AssertNone();
		_ = handler.Received().Invoke(exception);
	}
}
