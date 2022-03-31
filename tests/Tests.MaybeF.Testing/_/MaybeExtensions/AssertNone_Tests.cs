// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing.MaybeExtensions_Tests;

public class AssertNone_Tests
{
	[Fact]
	public void Is_Some__Throws_IsTypeException()
	{
		// Arrange
		var maybe = F.Some(Rnd.Lng);

		// Act
		var action = () => maybe.AssertNone();

		// Assert
		Assert.Throws<Xunit.Sdk.IsTypeException>(action);
	}

	[Fact]
	public void Is_None__Passes_Test__Returns_Msg()
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<string>(message);

		// Act
		var action = () => maybe.AssertNone();

		// Assert
		var none = action();
		Assert.Same(message, none);
	}

	public sealed record class TestMsg : IMsg;
}
