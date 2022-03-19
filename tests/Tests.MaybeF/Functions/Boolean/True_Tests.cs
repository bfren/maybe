// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class True_Tests
{
	[Fact]
	public void Returns_Some_With_Value_True()
	{
		// Arrange

		// Act
		var result = F.True;

		// Assert
		result.AssertTrue();
	}
}
