// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Linq.MaybeExtensions_Tests;

public class SelectMany_Tests
{
	[Fact]
	public void SelectMany_With_Some_Returns_Some()
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0);
		var o1 = F.Some(v1);

		// Act
		var result = from a in o0
					 from b in o1
					 select a + b;

		// Assert
		var some = result.AssertSome();
		Assert.Equal(v0 + v1, some);
	}

	[Fact]
	public async Task Async_SelectMany_With_Some_Returns_Some()
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0).AsTask;
		var o1 = F.Some(v1).AsTask;

		// Act
		var result = await (
			from a in o0
			from b in o1
			select a + b
		).ConfigureAwait(false);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(v0 + v1, some);
	}

	[Fact]
	public async Task Mixed_SelectMany_With_Some_Returns_Some()
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var v2 = Rnd.Int;
		var v3 = Rnd.Int;
		var o0 = F.Some(v0).AsTask;
		var o1 = F.Some(v1);
		var o2 = F.Some(v2).AsTask;
		var o3 = F.Some(v3);

		// Act
		var result = await (
			from a in o0
			from b in o1
			from c in o2
			from d in o3
			select a + b + c + d
		).ConfigureAwait(false);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(v0 + v1 + v2 + v3, some);
	}

	[Fact]
	public void SelectMany_With_None_Returns_None()
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0);
		var o1 = F.Some(v1);
		var o2 = F.None<int>(new InvalidIntegerReason());

		// Act
		var result = from a in o0
					 from b in o1
					 from c in o2
					 select a + b + c;

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<InvalidIntegerReason>(none);
	}

	[Fact]
	public async Task Async_SelectMany_With_None_Returns_None()
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0).AsTask;
		var o1 = F.Some(v1).AsTask;
		var o2 = F.None<int>(new InvalidIntegerReason()).AsTask;

		// Act
		var result = await (
			from a in o0
			from b in o1
			from c in o2
			select a + b + c
		).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<InvalidIntegerReason>(none);
	}

	[Fact]
	public async Task Mixed_SelectMany_With_None_Returns_None()
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0).AsTask;
		var o1 = F.Some(v1).AsTask;
		var o2 = F.None<int>(new InvalidIntegerReason());

		// Act
		var result = await (
			from a in o0
			from b in o1
			from c in o2
			select a + b + c
		).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<InvalidIntegerReason>(none);
	}

	public record class InvalidIntegerReason : IReason;
}
