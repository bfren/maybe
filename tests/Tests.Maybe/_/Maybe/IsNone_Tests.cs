﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Jeebs.Random;
using MaybeF.Testing;
using Xunit;

namespace MaybeF.Maybe_Tests;

public class IsNone_Tests
{
	[Fact]
	public void Is_Some_Returns_False()
	{
		// Arrange
		var some = F.Some(Rnd.Str);

		// Act
		var result = some.IsNone;

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Is_None_Returns_True()
	{
		// Arrange
		var none = Create.None<string>();

		// Act
		var result = none.IsNone;

		// Assert
		Assert.True(result);
	}
}
