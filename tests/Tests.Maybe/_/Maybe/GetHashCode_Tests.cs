// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Jeebs.Random;
using Maybe.Exceptions;
using Maybe.Functions;
using NSubstitute;
using Xunit;

namespace Maybe.Maybe_Tests;

public class GetHashCode_Tests
{
	[Fact]
	public void If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = void () => maybe.GetHashCode();

		// Assert
		_ = Assert.Throws<UnknownMaybeException>(action);
	}

	[Fact]
	public void Some_With_Same_Value_Generates_Same_HashCode()
	{
		// Arrange
		var value = Rnd.Str;
		var s0 = MaybeF.Some(value);
		var s1 = MaybeF.Some(value);

		// Act
		var h0 = s0.GetHashCode();
		var h1 = s1.GetHashCode();

		// Assert
		Assert.Equal(h0, h1);
	}

	[Fact]
	public void Some_With_Same_Type_And_Different_Value_Generates_Different_HashCode()
	{
		// Arrange
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;
		var s0 = MaybeF.Some(v0);
		var s1 = MaybeF.Some(v1);

		// Act
		var h0 = s0.GetHashCode();
		var h1 = s1.GetHashCode();

		// Assert
		Assert.NotEqual(h0, h1);
	}

	[Fact]
	public void Some_With_Null_Value_And_Same_Type_Generates_Same_HashCode()
	{
		// Arrange
		const string? v0 = null;
		const string? v1 = null;
		var s0 = MaybeF.Some(v0, true);
		var s1 = MaybeF.Some(v1, true);

		// Act
		var h0 = s0.GetHashCode();
		var h1 = s1.GetHashCode();

		// Assert
		Assert.Equal(h0, h1);
	}

	[Fact]
	public void Some_With_Null_Value_And_Different_Type_Generates_Different_HashCode()
	{
		// Arrange
		const string? v0 = null;
		int? v1 = null;
		var s0 = MaybeF.Some(v0, true);
		var s1 = MaybeF.Some(v1, true);

		// Act
		var h0 = s0.GetHashCode();
		var h1 = s1.GetHashCode();

		// Assert
		Assert.NotEqual(h0, h1);
	}

	[Fact]
	public void None_With_Same_Type_And_Same_Reason_Generates_Same_HashCode()
	{
		// Arrange
		var reason = Substitute.For<IReason>();
		var n0 = MaybeF.None<int>(reason);
		var n1 = MaybeF.None<int>(reason);

		// Act
		var h0 = n0.GetHashCode();
		var h1 = n1.GetHashCode();

		// Assert
		Assert.Equal(h0, h1);
	}

	[Fact]
	public void None_With_Different_Type_And_Same_Reason_Generates_Different_HashCode()
	{
		// Arrange
		var reason = Substitute.For<IReason>();
		var n0 = MaybeF.None<int>(reason);
		var n1 = MaybeF.None<string>(reason);

		// Act
		var h0 = n0.GetHashCode();
		var h1 = n1.GetHashCode();

		// Assert
		Assert.NotEqual(h0, h1);
	}

	[Fact]
	public void None_With_Same_Type_And_Different_Reason_Generates_Different_HashCode()
	{
		// Arrange
		var m0 = new TestReason0();
		var m1 = new TestReason1();
		var n0 = MaybeF.None<int>(m0);
		var n1 = MaybeF.None<int>(m1);

		// Act
		var h0 = n0.GetHashCode();
		var h1 = n1.GetHashCode();

		// Assert
		Assert.NotEqual(h0, h1);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason0 : IReason;

	public record class TestReason1 : IReason;
}
