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
	public void Is_None__Passes_Test__Returns_Reason()
	{
		// Arrange
		var reason = new TestReason();
		var maybe = F.None<string>(reason);

		// Act
		var action = () => maybe.AssertNone();

		// Assert
		var none = action();
		Assert.Same(reason, none);
	}

	public sealed record class TestReason : IReason;
}
