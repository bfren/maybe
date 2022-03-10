// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using Jeebs.Random;
using MaybeF;
using Xunit;

namespace Jeebs.Internals.None_Tests;

public class ToString_Tests
{
	[Fact]
	public void When_Not_ExceptionReason_Returns_Reason_ToString()
	{
		// Arrange
		var reason = new TestReason();
		var expected = reason.ToString();
		var maybe = F.None<int>(reason);

		// Act
		var result = maybe.ToString();

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void When_ExceptionReason_Returns_Reason_Type_And_Exception_Message()
	{
		// Arrange
		var value = Rnd.Str;
		var exception = new Exception(value);
		var maybe = F.None<int, TestExceptionReason>(exception);

		// Act
		var result = maybe.ToString();

		// Assert
		Assert.Equal($"{typeof(TestExceptionReason)}: {value}", result);
	}

	public record class TestReason : IReason;

	public record class TestExceptionReason(Exception Value) : IExceptionReason;
}
