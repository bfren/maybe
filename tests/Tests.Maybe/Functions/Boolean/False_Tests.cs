// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Maybe.Testing;
using Xunit;

namespace Maybe.Functions.MaybeF_Tests;

public class False_Tests
{
	[Fact]
	public void Returns_Some_With_Value_False()
	{
		// Arrange

		// Act
		var result = MaybeF.False;

		// Assert
		result.AssertFalse();
	}
}
