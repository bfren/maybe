// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using static MaybeF.F.M;

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
		result.AssertNone().AssertType<MaybeCannotBeNullMsg>();
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
		var result = F.Catch<int>(() => throw new Exception(message), null!);

		// Assert
		var none = result.AssertNone();
		var ex = Assert.IsType<UnhandledExceptionMsg>(none);
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
		result.AssertNone();
		handler.Received().Invoke(exception);
	}
}
