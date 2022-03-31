// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class FilterAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Maybe<int>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = await act(maybe).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		var message = Assert.IsType<UnhandledExceptionMsg>(none);
		Assert.IsType<UnknownMaybeException>(message.Value);
	}

	public abstract Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test01(Func<Maybe<string>, Func<string, Task<bool>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Str);
		var exception = new Exception();
		var throwFunc = Task<bool> (string _) => throw exception;

		// Act
		var result = await act(maybe, throwFunc).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(none);
	}

	public abstract Task Test02_When_Some_And_Predicate_True_Returns_Value();

	protected static async Task Test02(Func<Maybe<int>, Func<int, Task<bool>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var predicate = Substitute.For<Func<int, Task<bool>>>();
		predicate.Invoke(Arg.Any<int>()).Returns(true);

		// Act
		var result = await act(maybe, predicate).ConfigureAwait(false);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract Task Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg();

	protected static async Task Test03(Func<Maybe<string>, Func<string, Task<bool>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Str;
		var maybe = F.Some(value);
		var predicate = Substitute.For<Func<string, Task<bool>>>();
		predicate.Invoke(Arg.Any<string>()).Returns(false);

		// Act
		var result = await act(maybe, predicate).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<FilterPredicateWasFalseMsg>(none);
	}

	public abstract Task Test04_When_None_Returns_None_With_Original_Msg();

	protected static async Task Test04(Func<Maybe<int>, Func<int, Task<bool>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var predicate = Substitute.For<Func<int, Task<bool>>>();

		// Act
		var result = await act(maybe, predicate).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.Same(message, none);
		await predicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>()).ConfigureAwait(false);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
