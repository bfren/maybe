// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF.Testing;
using static MaybeF.F.R;

namespace MaybeF.F_Tests;

public class Catch_Tests
{
	[Theory]
	[InlineData(null)]
	public void Catches_Null_Maybe(Func<Maybe<int>> input)
	{
		// Arrange

		// Act
		var result = F.Catch(input, F.DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<MaybeCannotBeNullReason>(none);
	}

	[Fact]
	public void Executes_Chain()
	{
		// Arrange
		var value = Rnd.Int;

		// Act
		var result = F.Catch(() => F.Some(value), F.DefaultHandler);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	[Fact]
	public void Catches_Exception_Without_Handler()
	{
		// Arrange
		var message = Rnd.Str;

		// Act
		var result = F.Catch<int>(() => throw new Exception(message), F.DefaultHandler);

		// Assert
		var none = result.AssertNone();
		var ex = Assert.IsType<UnhandledExceptionReason>(none);
		Assert.Contains(message, ex.ToString());
	}

	[Fact]
	public void Catches_Exception_With_Handler()
	{
		// Arrange
		var message = Rnd.Str;
		var exception = new Exception(message);
		var handler = Substitute.For<F.Handler>();

		// Act
		var result = F.Catch<int>(() => throw exception, handler);

		// Assert
		_ = result.AssertNone();
		_ = handler.Received().Invoke(exception);
	}
}
