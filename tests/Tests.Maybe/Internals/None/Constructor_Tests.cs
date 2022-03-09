// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Maybe;
using Maybe.Internals;
using Xunit;

namespace Jeebs.Internals.None_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Reason()
	{
		// Arrange
		var reason = new TestReason();

		// Act
		var result = new None<string>(reason);

		// Assert
		Assert.Equal(reason, result.Reason);
	}
}

public record class TestReason : IReason;
