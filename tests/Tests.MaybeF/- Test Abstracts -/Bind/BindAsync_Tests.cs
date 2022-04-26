// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class BindAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		var result = await act(maybe, bind);

		// Assert
		var msg = result.AssertNone().AssertType<UnhandledExceptionMsg>();
		Assert.IsType<UnknownMaybeException>(msg.Value);
	}

	public abstract Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test01(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var exception = new Exception();
		var throwFunc = Task<Maybe<string>> () => throw exception;

		// Act
		var result = await act(maybe, _ => throwFunc());

		// Assert
		result.AssertNone().AssertType<UnhandledExceptionMsg>();
	}

	public abstract Task Test02_If_None_Gets_None();

	protected static async Task Test02(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		var result = await act(maybe, bind);

		// Assert
		result.AssertNone();
	}

	public abstract Task Test03_If_None_With_Msg_Gets_None_With_Same_Msg();

	protected static async Task Test03(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		var result = await act(maybe, bind);

		// Assert
		var none = result.AssertNone();
		Assert.Same(message, none);
	}

	public abstract Task Test04_If_Some_Runs_Bind_Function();

	protected static async Task Test04(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		await act(maybe, bind);

		// Assert
		await bind.Received().Invoke(value);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
