// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing.MaybeExtensions_Tests;

public class AssertSome_Tests
{
	[Fact]
	public void Is_None__Throws_IsTypeException()
	{
		// Arrange
		var maybe = Create.None<string>();

		// Act
		var action = () => maybe.AssertSome();

		// Assert
		Assert.Throws<Xunit.Sdk.IsTypeException>(action);
	}

	[Fact]
	public void Is_Some__Passes_Test__Returns_Value()
	{
		// Arrange
		var value = Rnd.Lng;
		var maybe = F.Some(value);

		// Act
		var action = () => maybe.AssertSome();

		// Assert
		var some = action();
		Assert.Equal(value, some);
	}

	public sealed record class TestMsg : IMsg;
}
