// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Jeebs.Random;
using MaybeF;
using MaybeF.Exceptions;
using MaybeF.Testing;
using NSubstitute;
using Xunit;
using static MaybeF.F.R;

namespace Abstracts;

public abstract class BindAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason();

	protected static async Task Test00(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		var result = await act(maybe, bind).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		var reason = Assert.IsType<UnhandledExceptionReason>(none);
		_ = Assert.IsType<UnknownMaybeException>(reason.Value);
	}

	public abstract Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionReason();

	protected static async Task Test01(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var exception = new Exception();
		var throwFunc = Task<Maybe<string>> () => throw exception;

		// Act
		var result = await act(maybe, _ => throwFunc()).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(none);
	}

	public abstract Task Test02_If_None_Gets_None();

	protected static async Task Test02(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		var result = await act(maybe, bind).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
	}

	public abstract Task Test03_If_None_With_Reason_Gets_None_With_Same_Reason();

	protected static async Task Test03(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var reason = new TestReason();
		var maybe = F.None<int>(reason);
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		var result = await act(maybe, bind).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public abstract Task Test04_If_Some_Runs_Bind_Function();

	protected static async Task Test04(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		_ = await act(maybe, bind).ConfigureAwait(false);

		// Assert
		_ = await bind.Received().Invoke(value).ConfigureAwait(false);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason : IReason;
}
