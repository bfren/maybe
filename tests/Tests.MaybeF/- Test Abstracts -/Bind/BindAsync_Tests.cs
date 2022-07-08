// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class BindAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> actTask, Func<Maybe<int>, Func<int, ValueTask<Maybe<string>>>, ValueTask<Maybe<string>>> actValueTask)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var bindTask = Substitute.For<Func<int, Task<Maybe<string>>>>();
		var bindValueTask = Substitute.For<Func<int, ValueTask<Maybe<string>>>>();

		// Act
		var r0 = await actTask(maybe, bindTask);
		var r1 = await actValueTask(maybe, bindValueTask);

		// Assert
		var m0 = r0.AssertNone().AssertType<UnhandledExceptionMsg>();
		Assert.IsType<UnknownMaybeException>(m0.Value);
		var m1 = r1.AssertNone().AssertType<UnhandledExceptionMsg>();
		Assert.IsType<UnknownMaybeException>(m1.Value);
	}

	public abstract Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test01(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> actTask, Func<Maybe<int>, Func<int, ValueTask<Maybe<string>>>, ValueTask<Maybe<string>>> actValueTask)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var exception = new Exception();
		var throwFuncTask = Task<Maybe<string>> () => throw exception;
		var throwFuncValueTask = ValueTask<Maybe<string>> () => throw exception;

		// Act
		var r0 = await actTask(maybe, _ => throwFuncTask());
		var r1 = await actValueTask(maybe, _ => throwFuncValueTask());

		// Assert
		r0.AssertNone().AssertType<UnhandledExceptionMsg>();
		r1.AssertNone().AssertType<UnhandledExceptionMsg>();
	}

	public abstract Task Test02_If_None_Gets_None();

	protected static async Task Test02(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> actTask, Func<Maybe<int>, Func<int, ValueTask<Maybe<string>>>, ValueTask<Maybe<string>>> actValueTask)
	{
		// Arrange
		var maybe = Create.None<int>();
		var bindTask = Substitute.For<Func<int, Task<Maybe<string>>>>();
		var bindValueTask = Substitute.For<Func<int, ValueTask<Maybe<string>>>>();

		// Act
		var r0 = await actTask(maybe, bindTask);
		var r1 = await actValueTask(maybe, bindValueTask);

		// Assert
		r0.AssertNone();
		r1.AssertNone();
	}

	public abstract Task Test03_If_None_With_Msg_Gets_None_With_Same_Msg();

	protected static async Task Test03(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> actTask, Func<Maybe<int>, Func<int, ValueTask<Maybe<string>>>, ValueTask<Maybe<string>>> actValueTask)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var bindTask = Substitute.For<Func<int, Task<Maybe<string>>>>();
		var bindValueTask = Substitute.For<Func<int, ValueTask<Maybe<string>>>>();

		// Act
		var r0 = await actTask(maybe, bindTask);
		var r1 = await actValueTask(maybe, bindValueTask);

		// Assert
		var n0 = r0.AssertNone();
		Assert.Same(message, n0);
		var n1 = r1.AssertNone();
		Assert.Same(message, n1);
	}

	public abstract Task Test04_If_Some_Runs_Bind_Function();

	protected static async Task Test04(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> actTask, Func<Maybe<int>, Func<int, ValueTask<Maybe<string>>>, ValueTask<Maybe<string>>> actValueTask)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var bindTask = Substitute.For<Func<int, Task<Maybe<string>>>>();
		var bindValueTask = Substitute.For<Func<int, ValueTask<Maybe<string>>>>();

		// Act
		await actTask(maybe, bindTask);
		await actValueTask(maybe, bindValueTask);

		// Assert
		await bindTask.Received().Invoke(value);
		await bindValueTask.Received().Invoke(value);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
