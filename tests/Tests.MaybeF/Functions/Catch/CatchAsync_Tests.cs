// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using static MaybeF.F.M;

namespace MaybeF.F_Tests;

public class CatchAsync_Tests
{
	[Theory]
	[InlineData(null, null)]
	public async Task Catches_Null_Maybe(Func<Task<Maybe<int>>> taskInput, Func<ValueTask<Maybe<int>>> valueTaskInput)
	{
		// Arrange

		// Act
		var r0 = await F.CatchAsync(taskInput, F.DefaultHandler);
		var r1 = await F.CatchAsync(valueTaskInput, F.DefaultHandler);

		// Assert
		r0.AssertNone().AssertType<MaybeCannotBeNullMsg>();
		r1.AssertNone().AssertType<MaybeCannotBeNullMsg>();
	}

	[Fact]
	public async Task Executes_Chain()
	{
		// Arrange
		var value = Rnd.Int;

		// Act
		var r0 = await F.CatchAsync(() => F.Some(value).AsTask(), F.DefaultHandler);
		var r1 = await F.CatchAsync(() => F.Some(value).AsValueTask(), F.DefaultHandler);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(value, s1);
	}

	[Fact]
	public async Task Catches_Exception_Without_Handler()
	{
		// Arrange
		var message = Rnd.Str;

		// Act
		var r0 = await F.CatchAsync(Task<Maybe<int>> () => throw new Exception(message), null!);
		var r1 = await F.CatchAsync(ValueTask<Maybe<int>> () => throw new Exception(message), null!);

		// Assert
		var e0 = r0.AssertNone().AssertType<UnhandledExceptionMsg>();
		Assert.Contains(message, e0.ToString());
		var e1 = r1.AssertNone().AssertType<UnhandledExceptionMsg>();
		Assert.Contains(message, e1.ToString());
	}

	[Fact]
	public async Task Catches_Exception_With_Handler()
	{
		// Arrange
		var message = Rnd.Str;
		var exception = new Exception(message);
		var handler = Substitute.For<F.Handler>();

		// Act
		var r0 = await F.CatchAsync(Task<Maybe<int>> () => throw exception, handler);
		var r1 = await F.CatchAsync(ValueTask<Maybe<int>> () => throw exception, handler);

		// Assert
		r0.AssertNone();
		r1.AssertNone();
		handler.Received(2).Invoke(exception);
	}
}
