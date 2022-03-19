// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class GetEnumerator_Tests
{
	[Fact]
	public void When_Some_Returns_Value()
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var result = 0;
		foreach (var item in maybe)
		{
			result = item;
		}

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void When_None_Does_Nothing()
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = Create.None<int>();

		// Act
		var result = value;
		foreach (var item in maybe)
		{
			result = 0;
		}

		// Assert
		Assert.Equal(value, result);
	}
}
