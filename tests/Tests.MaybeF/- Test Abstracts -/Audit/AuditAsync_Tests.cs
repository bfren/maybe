// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using MaybeF.Testing.Exceptions;

namespace Abstracts;

public abstract class AuditAsync_Tests
{
	#region General

	public abstract Task Test00_Null_Args_Returns_Original_Maybe();

	protected static async Task Test00(Func<Maybe<int>, Task<Maybe<int>>> actTask, Func<Maybe<int>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var r0 = await actTask(maybe);
		var r1 = await actValueTask(maybe);

		// Assert
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test01_If_Unknown_Maybe_Throws_UnknownMaybeException();

	protected static async Task Test01(Func<Maybe<int>, Task<Maybe<int>>> actTask, Func<Maybe<int>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var a0 = Task () => actTask(maybe);
		var a1 = async () => await actValueTask(maybe);

		// Assert
		await Assert.ThrowsAsync<UnknownMaybeException>(a0);
		await Assert.ThrowsAsync<UnknownMaybeException>(a1);
	}

	#endregion General

	#region Any

	public abstract Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Maybe();

	protected static async Task Test02(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> actTask, Func<Maybe<bool>, Action<Maybe<bool>>, ValueTask<Maybe<bool>>> actValueTask)
	{
		// Arrange
		var maybe = F.True;
		var audit = Substitute.For<Action<Maybe<bool>>>();

		// Act
		var r0 = await actTask(maybe, audit);
		var r1 = await actValueTask(maybe, audit);

		// Assert
		audit.Received(2).Invoke(maybe);
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test03_None_Runs_Audit_Action_And_Returns_Original_Maybe();

	protected static async Task Test03(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> actTask, Func<Maybe<bool>, Action<Maybe<bool>>, ValueTask<Maybe<bool>>> actValueTask)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var audit = Substitute.For<Action<Maybe<bool>>>();

		// Act
		var r0 = await actTask(maybe, audit);
		var r1 = await actValueTask(maybe, audit);

		// Assert
		audit.Received(2).Invoke(maybe);
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Maybe();

	protected static async Task Test04(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> actTask, Func<Maybe<bool>, Func<Maybe<bool>, ValueTask>, ValueTask<Maybe<bool>>> actValueTask)
	{
		// Arrange
		var maybe = F.True;
		var auditTask = Substitute.For<Func<Maybe<bool>, Task>>();
		var auditValueTask = Substitute.For<Func<Maybe<bool>, ValueTask>>();

		// Act
		var r0 = await actTask(maybe, auditTask);
		var r1 = await actValueTask(maybe, auditValueTask);

		// Assert
		await auditTask.Received().Invoke(maybe);
		await auditValueTask.Received().Invoke(maybe);
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test05_None_Runs_Audit_Func_And_Returns_Original_Maybe();

	protected static async Task Test05(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> actTask, Func<Maybe<bool>, Func<Maybe<bool>, ValueTask>, ValueTask<Maybe<bool>>> actValueTask)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var auditTask = Substitute.For<Func<Maybe<bool>, Task>>();
		var auditValueTask = Substitute.For<Func<Maybe<bool>, ValueTask>>();

		// Act
		var r0 = await actTask(maybe, auditTask);
		var r1 = await actValueTask(maybe, auditValueTask);

		// Assert
		await auditTask.Received().Invoke(maybe);
		await auditValueTask.Received().Invoke(maybe);
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test06(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> actTask, Func<Maybe<bool>, Action<Maybe<bool>>, ValueTask<Maybe<bool>>> actValueTask)
	{
		// Arrange
		var maybe = F.True;
		var throwException = void (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var r0 = await actTask(maybe, throwException);
		var r1 = await actValueTask(maybe, throwException);

		// Assert
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test07(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> actTask, Func<Maybe<bool>, Action<Maybe<bool>>, ValueTask<Maybe<bool>>> actValueTask)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var throwException = void (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var r0 = await actTask(maybe, throwException);
		var r1 = await actValueTask(maybe, throwException);

		// Assert
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test08(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> actTask, Func<Maybe<bool>, Func<Maybe<bool>, ValueTask>, ValueTask<Maybe<bool>>> actValueTask)
	{
		// Arrange
		var maybe = F.True;
		var throwExceptionTask = Task (Maybe<bool> _) => throw new MaybeTestException();
		var throwExceptionValueTask = ValueTask (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var r0 = await actTask(maybe, throwExceptionTask);
		var r1 = await actValueTask(maybe, throwExceptionValueTask);

		// Assert
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test09(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> actTask, Func<Maybe<bool>, Func<Maybe<bool>, ValueTask>, ValueTask<Maybe<bool>>> actValueTask)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var throwExceptionTask = Task (Maybe<bool> _) => throw new MaybeTestException();
		var throwExceptionValueTask = ValueTask (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var r0 = await actTask(maybe, throwExceptionTask);
		var r1 = await actValueTask(maybe, throwExceptionValueTask);

		// Assert
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	#endregion Any

	#region Some / None

	public abstract Task Test10_Some_Runs_Some_Action_And_Returns_Original_Maybe();

	protected static async Task Test10(Func<Maybe<int>, Action<int>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Action<int>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var some = Substitute.For<Action<int>>();

		// Act
		var r0 = await actTask(maybe, some);
		var r1 = await actValueTask(maybe, some);

		// Assert
		some.Received(2).Invoke(value);
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test11_Some_Runs_Some_Func_And_Returns_Original_Maybe();

	protected static async Task Test11(Func<Maybe<int>, Func<int, Task>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Func<int, ValueTask>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var someTask = Substitute.For<Func<int, Task>>();
		var someValueTask = Substitute.For<Func<int, ValueTask>>();

		// Act
		var r0 = await actTask(maybe, someTask);
		var r1 = await actValueTask(maybe, someValueTask);

		// Assert
		await someTask.Received().Invoke(value);
		await someValueTask.Received().Invoke(value);
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test12_None_Runs_None_Action_And_Returns_Original_Maybe();

	protected static async Task Test12(Func<Maybe<int>, Action<IMsg>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Action<IMsg>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var none = Substitute.For<Action<IMsg>>();

		// Act
		var r0 = await actTask(maybe, none);
		var r1 = await actValueTask(maybe, none);

		// Assert
		none.Received(2).Invoke(message);
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test13_None_Runs_None_Func_And_Returns_Original_Maybe();

	protected static async Task Test13(Func<Maybe<int>, Func<IMsg, Task>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Func<IMsg, ValueTask>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var noneTask = Substitute.For<Func<IMsg, Task>>();
		var noneValueTask = Substitute.For<Func<IMsg, ValueTask>>();

		// Act
		var r0 = await actTask(maybe, noneTask);
		var r1 = await actValueTask(maybe, noneValueTask);

		// Assert
		await noneTask.Received().Invoke(message);
		await noneValueTask.Received().Invoke(message);
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test14(Func<Maybe<int>, Action<int>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Action<int>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var exception = new Exception();
		var throwException = void (int _) => throw exception;

		// Act
		var r0 = await actTask(maybe, throwException);
		var r1 = await actValueTask(maybe, throwException);

		// Assert
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test15(Func<Maybe<int>, Func<int, Task>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Func<int, ValueTask>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var exception = new Exception();
		var throwExceptionTask = Task (int _) => throw exception;
		var throwExceptionValueTask = ValueTask (int _) => throw exception;

		// Act
		var r0 = await actTask(maybe, throwExceptionTask);
		var r1 = await actValueTask(maybe, throwExceptionValueTask);

		// Assert
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test16(Func<Maybe<int>, Action<IMsg>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Action<IMsg>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var maybe = Create.None<int>();
		var exception = new Exception();
		var throwException = void (IMsg _) => throw exception;

		// Act
		var r0 = await actTask(maybe, throwException);
		var r1 = await actValueTask(maybe, throwException);

		// Assert
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	public abstract Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test17(Func<Maybe<int>, Func<IMsg, Task>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Func<IMsg, ValueTask>, ValueTask<Maybe<int>>> actValueTask)
	{
		// Arrange
		var maybe = Create.None<int>();
		var exception = new Exception();
		var throwExceptionTask = Task (IMsg _) => throw exception;
		var throwExceptionValueTask = ValueTask (IMsg _) => throw exception;

		// Act
		var r0 = await actTask(maybe, throwExceptionTask);
		var r1 = await actValueTask(maybe, throwExceptionValueTask);

		// Assert
		Assert.Same(maybe, r0);
		Assert.Same(maybe, r1);
	}

	#endregion Some / None

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
