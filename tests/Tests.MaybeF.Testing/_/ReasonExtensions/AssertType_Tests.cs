// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing.MsgExtensions_Tests;

public class AssertType_Tests
{
	[Fact]
	public void Is_Type_Returns_Msg()
	{
		// Arrange
		var message = (IMsg)new TestMsg();

		// Act
		var result = message.AssertType<TestMsg>();

		// Assert
		Assert.IsType<TestMsg>(result);
	}

	[Fact]
	public void Is_Not_Type_Throws_IsTypeException()
	{
		// Arrange
		var message = Substitute.For<IMsg>();

		// Act
		var action = void () => message.AssertType<TestMsg>();

		// Assert
		Assert.Throws<Xunit.Sdk.IsTypeException>(action);
	}

	public sealed record class TestMsg : IMsg;
}
