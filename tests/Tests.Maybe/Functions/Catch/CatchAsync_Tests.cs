﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Jeebs.Random;
using MaybeF.Testing;
using NSubstitute;
using Xunit;
using static MaybeF.F.R;

namespace MaybeF.MaybeF_Tests;

public class CatchAsync_Tests
{
	[Fact]
	public async Task Executes_Chain()
	{
		// Arrange
		var value = Rnd.Int;

		// Act
		var result = await F.CatchAsync(() => F.Some(value).AsTask, F.DefaultHandler).ConfigureAwait(false);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	[Fact]
	public async Task Catches_Exception_Without_Handler()
	{
		// Arrange
		var message = Rnd.Str;

		// Act
		var result = await F.CatchAsync<int>(() => throw new Exception(message), F.DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		var ex = Assert.IsType<UnhandledExceptionReason>(none);
		Assert.Contains(message, ex.ToString());
	}

	[Fact]
	public async Task Catches_Exception_With_Handler()
	{
		// Arrange
		var message = Rnd.Str;
		var exception = new Exception(message);
		var handler = Substitute.For<F.Handler>();

		// Act
		var result = await F.CatchAsync<int>(() => throw exception, handler).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
		_ = handler.Received().Invoke(exception);
	}
}
