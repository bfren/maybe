// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Jeebs.Internals.None_Tests;

public class ToString_Tests
{
	[Fact]
	public void When_Not_ExceptionMsg_Returns_Msg_ToString()
	{
		// Arrange
		var message = new TestMsg();
		var expected = message.ToString();
		var maybe = F.None<int>(message);

		// Act
		var result = maybe.ToString();

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void When_ExceptionMsg_Returns_Msg_Type_And_Exception_Message()
	{
		// Arrange
		var value = Rnd.Str;
		var exception = new Exception(value);
		var maybe = F.None<int, TestExceptionMsg>(exception);

		// Act
		var result = maybe.ToString();

		// Assert
		Assert.Equal($"{typeof(TestExceptionMsg)}: {value}", result);
	}

	public record class TestMsg : IMsg;

	public record class TestExceptionMsg(Exception Value) : IExceptionMsg;
}
