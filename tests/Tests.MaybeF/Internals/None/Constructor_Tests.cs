// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Internals;

namespace Jeebs.Internals.None_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Msg()
	{
		// Arrange
		var message = new TestMsg();

		// Act
		var result = new None<string>(message);

		// Assert
		Assert.Equal(message, result.Reason);
	}
}

public record class TestMsg : IMsg;
