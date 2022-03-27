// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing.ReasonExtensions_Tests;

public class AssertType_Tests
{
	[Fact]
	public void Is_Type_Returns_Reason()
	{
		// Arrange
		var reason = (IReason)new TestReason();

		// Act
		var result = reason.AssertType<TestReason>();

		// Assert
		Assert.IsType<TestReason>(result);
	}

	[Fact]
	public void Is_Not_Type_Throws_Exception()
	{
		// Arrange
		var reason = Substitute.For<IReason>();

		// Act
		var action = void () => reason.AssertType<TestReason>();

		// Assert
		Assert.Throws<Xunit.Sdk.IsTypeException>(action);
	}

	public sealed record class TestReason : IReason;
}
