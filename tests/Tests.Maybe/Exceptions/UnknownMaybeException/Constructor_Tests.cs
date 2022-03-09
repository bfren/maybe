﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using Jeebs.Random;
using Xunit;

namespace Maybe.Exceptions.UnknownMaybeException_Tests;

public class Constructor_Tests
{
	[Fact]
	public void No_Args_Creates_Default_Exception()
	{
		// Arrange

		// Act
		var result = new UnknownMaybeException();

		// Assert
		Assert.Equal($"Exception of type '{typeof(UnknownMaybeException)}' was thrown.", result.Message);
		Assert.Null(result.InnerException);
	}

	[Fact]
	public void With_Message_Sets_Message()
	{
		// Arrange
		var message = Rnd.Str;

		// Act
		var result = new UnknownMaybeException(message);

		// Assert
		Assert.Equal(message, result.Message);
		Assert.Null(result.InnerException);
	}

	[Fact]
	public void With_Inner_Exception_Sets_InnerException()
	{
		// Arrange
		var inner = new Exception(Rnd.Str);

		// Act
		var result = new UnknownMaybeException(Rnd.Str, inner);

		// Assert
		Assert.Same(inner, result.InnerException);
	}
}
