// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Jeebs.Some_Tests;

public class ToString_Tests
{
	[Fact]
	public void With_Value_Returns_Value_ToString()
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
	public void Value_Is_Null_Returns_Type()
	{
		// Arrange
		int? value = null;
		var maybe = F.Some(value, true);

		// Act
		var result = maybe.ToString();

		// Assert
		Assert.Equal("Some: " + typeof(int?), result);
	}
}
