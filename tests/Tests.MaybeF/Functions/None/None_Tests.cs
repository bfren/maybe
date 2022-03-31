// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class None_Tests
{
	[Fact]
	public void Returns_None_Without_Msg()
	{
		// Arrange

		// Act
		var result = Create.None<int>();

		// Assert
		result.AssertNone();
	}

	[Fact]
	public void Returns_None_With_Msg_Object()
	{
		// Arrange
		var message = Substitute.For<IMsg>();

		// Act
		var result = F.None<int>(message);

		// Assert
		var none = result.AssertNone();
		Assert.Same(message, none);
	}

	[Fact]
	public void Returns_None_With_Msg_Type()
	{
		// Arrange

		// Act
		var result = F.None<int, TestMsg>();

		// Assert
		var none = result.AssertNone();
		Assert.IsType<TestMsg>(none);
	}

	[Fact]
	public void Returns_None_With_Msg_Exception_Type()
	{
		// Arrange
		var exception = new Exception();

		// Act
		var result = F.None<int, TestExceptionMsg>(exception);

		// Assert
		var none = result.AssertNone();
		var message = Assert.IsType<TestExceptionMsg>(none);
		Assert.Same(exception, message.Value);
	}

	public record class TestMsg : IMsg;

	public record class TestExceptionMsg(Exception Value) : IExceptionMsg;
}
