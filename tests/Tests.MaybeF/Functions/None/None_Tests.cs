// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class None_Tests
{
	[Fact]
	public void Returns_None_Without_Reason()
	{
		// Arrange

		// Act
		var result = Create.None<int>();

		// Assert
		result.AssertNone();
	}

	[Fact]
	public void Returns_None_With_Reason_Object()
	{
		// Arrange
		var reason = Substitute.For<IReason>();

		// Act
		var result = F.None<int>(reason);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	[Fact]
	public void Returns_None_With_Reason_Type()
	{
		// Arrange

		// Act
		var result = F.None<int, TestReason>();

		// Assert
		var none = result.AssertNone();
		Assert.IsType<TestReason>(none);
	}

	[Fact]
	public void Returns_None_With_Reason_Exception_Type()
	{
		// Arrange
		var exception = new Exception();

		// Act
		var result = F.None<int, TestExceptionReason>(exception);

		// Assert
		var none = result.AssertNone();
		var reason = Assert.IsType<TestExceptionReason>(none);
		Assert.Same(exception, reason.Value);
	}

	public record class TestReason : IReason;

	public record class TestExceptionReason(Exception Value) : IExceptionReason;
}
