// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF.Testing;

namespace MaybeF.OptionEqualityComparer_Tests;

public class Equals_Tests
{
	[Fact]
	public void When_One_Is_Unknown_Maybe_Returns_False()
	{
		// Arrange
		var fake = new FakeMaybe();
		var maybe = F.Some(Rnd.Int);
		var comparer = new MaybeEqualityComparer<int>();

		// Act
		var r0 = comparer.Equals(fake, maybe);
		var r1 = comparer.Equals(maybe, fake);

		// Assert
		Assert.False(r0);
		Assert.False(r1);
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
		var comparer = new MaybeEqualityComparer<int>();

		// Act
		var r0 = comparer.Equals(o0, o1);
		var r1 = comparer.Equals(o1, o2);

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
		var comparer = new MaybeEqualityComparer<int>();

		// Act
		var r0 = comparer.Equals(o0, o1);
		var r1 = comparer.Equals(o1, o2);

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
		var comparer = new MaybeEqualityComparer<int>();

		// Act
		var r0 = comparer.Equals(o0, o1);
		var r1 = comparer.Equals(o1, o0);

		// Assert
		Assert.False(r0);
		Assert.False(r1);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason0 : IReason;

	public record class TestReason1 : IReason;
}
