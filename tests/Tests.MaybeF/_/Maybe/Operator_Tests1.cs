// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public partial class Operator_Tests
{
	[Fact]
	public void Equals_When_Equal_Returns_True()
	{
		// Arrange
		var value = Rnd.Int;
		var some = F.Some(value);

		// Act
		var r0 = some == value;
		var r1 = value == some;

		// Assert
		Assert.True(r0);
		Assert.True(r1);
	}

	[Fact]
	public void Equals_When_Not_Equal_Returns_False()
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var some = F.Some(v0);

		// Act
		var r0 = some == v1;
		var r1 = v1 == some;

		// Assert
		Assert.False(r0);
		Assert.False(r1);
	}

	public record class TestReason : IReason;
}
