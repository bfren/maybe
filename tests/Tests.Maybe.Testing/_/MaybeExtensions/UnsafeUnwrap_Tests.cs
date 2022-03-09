// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Jeebs.Random;
using Maybe.Functions;
using Maybe.Testing;
using Maybe.Testing.Exceptions;
using Xunit;

namespace MaybeExtensions_Tests;

public class UnsafeUnwrap_Tests
{
	[Fact]
	public void Some_Returns_Value()
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = MaybeF.Some(value);

		// Act
		var result = maybe.UnsafeUnwrap();

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void None_Throws_UnsafeUnwrapException()
	{
		// Arrange
		var maybe = Create.None<int>();

		// Act
		var action = void () => maybe.UnsafeUnwrap();

		// Assert
		_ = Assert.Throws<UnsafeUnwrapException>(action);
	}
}
