// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF.Testing;

namespace MaybeF.Maybe_Tests;

public class Equals_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData(56897)]
	[InlineData("ransfl39vdv")]
	[InlineData(true)]
	public void When_Other_Is_Not_Maybe_Returns_False(object? other)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = maybe.Equals(other);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Some_Compares_Values()
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0);
		var o1 = F.Some(v0);
		var o2 = F.Some(v1);

		// Act
		var r0 = o0.Equals(o1);
		var r1 = o1.Equals(o2);

		// Assert
		Assert.True(r0);
		Assert.False(r1);
	}

	[Fact]
	public void None_Compares_Reasons()
	{
		// Arrange
		var m0 = new TestReason0();
		var m1 = new TestReason1();
		var o0 = F.None<int>(m0);
		var o1 = F.None<int>(m0);
		var o2 = F.None<int>(m1);

		// Act
		var r0 = o0.Equals(o1);
		var r1 = o1.Equals(o2);

		// Assert
		Assert.True(r0);
		Assert.False(r1);
	}

	[Fact]
	public void Mixed_Returns_False()
	{
		// Arrange
		var o0 = F.Some(Rnd.Int);
		var o1 = Create.None<int>();

		// Act
		var r0 = o0.Equals(o1);
		var r1 = o1.Equals(o0);

		// Assert
		Assert.False(r0);
		Assert.False(r1);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason0 : IReason;

	public record class TestReason1 : IReason;
}
