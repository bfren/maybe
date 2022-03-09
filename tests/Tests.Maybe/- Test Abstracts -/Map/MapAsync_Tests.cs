// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Jeebs.Random;
using Maybe;
using Maybe.Exceptions;
using Maybe.Functions;
using Maybe.Testing;
using NSubstitute;
using Xunit;
using static Maybe.Functions.MaybeF.R;

namespace Tests.Maybe.Abstracts;

public abstract class MapAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason();

	protected static async Task Test00(Func<Maybe<int>, Func<int, Task<string>>, MaybeF.Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var map = Substitute.For<Func<int, Task<string>>>();

		// Act
		var result = await act(maybe, map, MaybeF.DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		var reason = Assert.IsType<UnhandledExceptionReason>(none);
		_ = Assert.IsType<UnknownMaybeException>(reason.Value);
	}

	public abstract Task Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionReason();

	protected static async Task Test01(Func<Maybe<string>, Func<string, Task<int>>, MaybeF.Handler, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = MaybeF.Some(Rnd.Str);
		var exception = new Exception();
		var throwFunc = Task<int> (string _) => throw exception;

		// Act
		var result = await act(maybe, throwFunc, MaybeF.DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(none);
	}

	public abstract Task Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None();

	protected static async Task Test02(Func<Maybe<string>, Func<string, Task<int>>, MaybeF.Handler, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = MaybeF.Some(Rnd.Str);
		var handler = Substitute.For<MaybeF.Handler>();
		var exception = new Exception();
		var throwFunc = Task<int> (string _) => throw exception;

		// Act
		var result = await act(maybe, throwFunc, handler).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
		_ = handler.Received().Invoke(exception);
	}

	public abstract Task Test03_If_None_Returns_None();

	protected static async Task Test03(Func<Maybe<int>, Func<int, Task<string>>, MaybeF.Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var map = Substitute.For<Func<int, Task<string>>>();

		// Act
		var result = await act(maybe, map, MaybeF.DefaultHandler).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
	}

	public abstract Task Test04_If_None_With_Reason_Returns_None_With_Same_Reason();

	protected static async Task Test04(Func<Maybe<int>, Func<int, Task<string>>, MaybeF.Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var reason = new TestReason();
		var maybe = MaybeF.None<int>(reason);
		var map = Substitute.For<Func<int, Task<string>>>();

		// Act
		var result = await act(maybe, map, MaybeF.DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public abstract Task Test05_If_Some_Runs_Map_Function();

	protected static async Task Test05(Func<Maybe<int>, Func<int, Task<string>>, MaybeF.Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = MaybeF.Some(value);
		var map = Substitute.For<Func<int, Task<string>>>();

		// Act
		_ = await act(maybe, map, MaybeF.DefaultHandler).ConfigureAwait(false);

		// Assert
		_ = await map.Received().Invoke(value).ConfigureAwait(false);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason : IReason;
}
