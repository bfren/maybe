// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing.MaybeExtensions_Tests;

public class AssertFalse_Tests
{
	[Fact]
	public void Is_None__Throws_IsTypeException()
	{
		// Arrange
		var none = Create.None<bool>();

		// Act
		var action = () => none.AssertFalse();

		// Assert
		Assert.Throws<Xunit.Sdk.IsTypeException>(action);
	}

	[Fact]
	public void Is_Some__With_Value_True__Throws_FalseException()
	{
		// Arrange
		var some = F.True;

		// Act
		var action = () => some.AssertFalse();

		// Assert
		Assert.Throws<Xunit.Sdk.FalseException>(action);
	}

	[Fact]
	public void Is_Some__With_Value_False__Passes_Test()
	{
		// Arrange
		var some = F.False;

		// Act
		var action = () => some.AssertFalse();

		// Assert
		action();
	}
}
