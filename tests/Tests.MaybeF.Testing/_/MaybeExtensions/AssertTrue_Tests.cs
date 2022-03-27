// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing.MaybeExtensions_Tests;

public class AssertTrue_Tests
{
	[Fact]
	public void Is_None__Throws_IsTypeException()
	{
		// Arrange
		var none = Create.None<bool>();

		// Act
		var action = () => none.AssertTrue();

		// Assert
		Assert.Throws<Xunit.Sdk.IsTypeException>(action);
	}

	[Fact]
	public void Is_Some__With_Value_False__Throws_FalseException()
	{
		// Arrange
		var some = F.False;

		// Act
		var action = () => some.AssertTrue();

		// Assert
		Assert.Throws<Xunit.Sdk.TrueException>(action);
	}

	[Fact]
	public void Is_Some__With_Value_True__Passes_Test()
	{
		// Arrange
		var some = F.True;

		// Act
		var action = () => some.AssertTrue();

		// Assert
		action();
	}
}
