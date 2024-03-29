// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Jeebs.Some_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Value()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = new Some<string>(value);

		// Assert
		Assert.Equal(value, result.Value);
	}
}
