﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using static MaybeF.F.M;

namespace MaybeF.Maybe_Tests;

public partial class Operator_Tests
{
	[Theory]
	[InlineData(18)]
	[InlineData("foo")]
	public void Implicit_With_Value_Returns_Some<T>(T input)
	{
		// Arrange

		// Act
		Maybe<T> result = input;

		// Assert
		var some = result.AssertSome();
		Assert.Equal(input, some);
	}

	[Theory]
	[InlineData(null)]
	public void Implicit_With_Null_Returns_None(object input)
	{
		// Arrange

		// Act
		Maybe<object> result = input;

		// Assert
		result.AssertNone().AssertType<NullValueMsg>();
	}
}
