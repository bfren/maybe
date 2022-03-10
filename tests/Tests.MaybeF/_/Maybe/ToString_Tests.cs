// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class ToString_Tests
{
	[Fact]
	public void Some_With_Value_Returns_Value_ToString()
	{
		// Arrange
		var value = Rnd.Lng;
		var maybe = F.Some(value);

		// Act
		var result = maybe.ToString();

		// Assert
		Assert.Equal(value.ToString(), result);
	}

	[Fact]
	public void Some_Value_Is_Null_Returns_Type()
	{
		// Arrange
		int? value = null;
		var maybe = F.Some(value, true);

		// Act
		var result = maybe.ToString();

		// Assert
		Assert.Equal("Some: " + typeof(int?), result);
	}

	[Fact]
	public void None_Returns_Reason_ToString()
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
	public void None_With_ExceptionReason_Returns_Reason_Type_And_Exception_Message()
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
