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

	protected static async Task Test00(Func<Maybe<int>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = await act(maybe);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test01_If_Unknown_Maybe_Throws_UnknownMaybeException();

	protected static async Task Test01(Func<Maybe<int>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = Task () => act(maybe);

		// Assert
		await Assert.ThrowsAsync<UnknownMaybeException>(action);
	}

	#endregion General

	#region Any

	public abstract Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Maybe();

	protected static async Task Test02(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = F.True;
		var audit = Substitute.For<Action<Maybe<bool>>>();

		// Act
		var result = await act(maybe, audit);

		// Assert
		audit.Received().Invoke(maybe);
		Assert.Same(maybe, result);
	}

	public abstract Task Test03_None_Runs_Audit_Action_And_Returns_Original_Maybe();

	protected static async Task Test03(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var audit = Substitute.For<Action<Maybe<bool>>>();

		// Act
		var result = await act(maybe, audit);

		// Assert
		audit.Received().Invoke(maybe);
		Assert.Same(maybe, result);
	}

	public abstract Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Maybe();

	protected static async Task Test04(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = F.True;
		var audit = Substitute.For<Func<Maybe<bool>, Task>>();

		// Act
		var result = await act(maybe, audit);

		// Assert
		await audit.Received().Invoke(maybe);
		Assert.Same(maybe, result);
	}

	public abstract Task Test05_None_Runs_Audit_Func_And_Returns_Original_Maybe();

	protected static async Task Test05(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var audit = Substitute.For<Func<Maybe<bool>, Task>>();

		// Act
		var result = await act(maybe, audit);

		// Assert
		await audit.Received().Invoke(maybe);
		Assert.Same(maybe, result);
	}

	public abstract Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test06(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = F.True;
		var throwException = void (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test07(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var throwException = void (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test08(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = F.True;
		var throwException = Task (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test09(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var throwException = Task (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	#endregion Any

	#region Some / None

	public abstract Task Test10_Some_Runs_Some_Action_And_Returns_Original_Maybe();

	protected static async Task Test10(Func<Maybe<int>, Action<int>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var some = Substitute.For<Action<int>>();

		// Act
		var result = await act(maybe, some);

		// Assert
		some.Received().Invoke(value);
		Assert.Same(maybe, result);
	}

	public abstract Task Test11_Some_Runs_Some_Func_And_Returns_Original_Maybe();

	protected static async Task Test11(Func<Maybe<int>, Func<int, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var some = Substitute.For<Func<int, Task>>();

		// Act
		var result = await act(maybe, some);

		// Assert
		await some.Received().Invoke(value);
		Assert.Same(maybe, result);
	}

	public abstract Task Test12_None_Runs_None_Action_And_Returns_Original_Maybe();

	protected static async Task Test12(Func<Maybe<int>, Action<IMsg>, Task<Maybe<int>>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var none = Substitute.For<Action<IMsg>>();

		// Act
		var result = await act(maybe, none);

		// Assert
		none.Received().Invoke(message);
		Assert.Same(maybe, result);
	}

	public abstract Task Test13_None_Runs_None_Func_And_Returns_Original_Maybe();

	protected static async Task Test13(Func<Maybe<int>, Func<IMsg, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var none = Substitute.For<Func<IMsg, Task>>();

		// Act
		var result = await act(maybe, none);

		// Assert
		await none.Received().Invoke(message);
		Assert.Same(maybe, result);
	}

	public abstract Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test14(Func<Maybe<int>, Action<int>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var exception = new Exception();
		var throwException = void (int _) => throw exception;

		// Act
		var result = await act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test15(Func<Maybe<int>, Func<int, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var exception = new Exception();
		var throwException = Task (int _) => throw exception;

		// Act
		var result = await act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test16(Func<Maybe<int>, Action<IMsg>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var exception = new Exception();
		var throwException = void (IMsg _) => throw exception;

		// Act
		var result = await act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Maybe();

	protected static async Task Test17(Func<Maybe<int>, Func<IMsg, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var exception = new Exception();
		var throwException = Task (IMsg _) => throw exception;

		// Act
		var result = await act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	#endregion Some / None

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
