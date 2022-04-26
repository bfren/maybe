// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using static MaybeF.F.M;

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
		result.AssertNone().AssertType<MaybeCannotBeNullMsg>();
	}

	[Fact]
	public async Task Executes_Chain()
	{
		// Arrange
		var value = Rnd.Int;

		// Act
		var result = await F.CatchAsync(() => F.Some(value).AsTask, F.DefaultHandler);

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
		var result = await F.CatchAsync<int>(() => throw new Exception(message), null!);

		// Assert
		var none = result.AssertNone();
		var ex = Assert.IsType<UnhandledExceptionMsg>(none);
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
		var result = await F.CatchAsync<int>(() => throw exception, handler);

		// Assert
		result.AssertNone();
		handler.Received().Invoke(exception);
	}
}
