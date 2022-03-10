// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF.Testing;
using Xunit;

namespace MaybeF.MaybeF_Tests;

public class False_Tests
{
	[Fact]
	public void Returns_Some_With_Value_False()
	{
		// Arrange

		// Act
		var result = F.False;

		// Assert
		result.AssertFalse();
	}
}
