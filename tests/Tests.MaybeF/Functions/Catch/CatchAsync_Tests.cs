// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF.Testing;
using static MaybeF.F.R;

namespace MaybeF.F_Tests;

public class CatchAsync_Tests
{
	[Theory]
	[InlineData(null)]
	public async Task Catches_Null_Maybe(Func<Task<Maybe<int>>> input)
	{
		// Arrange

		// Act
		var result = await F.CatchAsync(input, F.DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<MaybeCannotBeNullReason>(none);
	}

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
