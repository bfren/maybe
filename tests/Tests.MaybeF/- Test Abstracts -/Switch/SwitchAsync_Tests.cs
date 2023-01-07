// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class SwitchAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Throws_UnknownMaybeException();

	protected static async Task Test00(Func<Maybe<int>, Task<string>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = async Task<string> () => await act(maybe);

		// Assert
		await Assert.ThrowsAsync<UnknownMaybeException>(action);
	}

	public abstract Task Test01_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input);

	protected static async Task Test01(Func<Task<string>> act)
	{
		// Arrange

		// Act
		var action = async Task<string> () => await act();

		// Assert
		await Assert.ThrowsAsync<MaybeCannotBeNullException>(action);
	}

	public abstract Task Test02_If_None_Runs_None_Func_With_Msg();

	protected static async Task Test02(Func<Maybe<int>, Func<IMsg, Task<string>>, Task<string>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var none = Substitute.For<Func<IMsg, Task<string>>>();

		// Act
		await act(maybe, none);

		// Assert
		await none.Received().Invoke(message);
	}

	public abstract Task Test03_If_Some_Runs_Some_Func_With_Value();

	protected static async Task Test03(Func<Maybe<int>, Func<int, Task<string>>, Task<string>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var some = Substitute.For<Func<int, Task<string>>>();

		// Act
		await act(maybe, some);

		// Assert
		await some.Received().Invoke(value);
	}

	public abstract Task Test04_If_None_And_None_Func_Is_Null_Throws_ArgumentNullException();

	protected static async Task Test04(Func<Maybe<int>, Func<IMsg, Task<string>>, Task<string>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);

		// Act
		var action = () => act(maybe, null!);

		// Assert
		await Assert.ThrowsAsync<ArgumentNullException>(action);
	}

	public abstract Task Test05_If_Some_And_Some_Func_Is_Null_Throws_ArgumentNullException();

	protected static async Task Test05(Func<Maybe<int>, Func<int, Task<string>>, Task<string>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var action = () => act(maybe, null!);

		// Assert
		await Assert.ThrowsAsync<ArgumentNullException>(action);
	}

	public abstract Task Test06_If_Some_Runs_Some_Func_With_Value();

	protected static async Task Test06(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var some = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		await act(maybe, some);

		// Assert
		await some.Received().Invoke(value);
	}

	public abstract Task Test07_If_None_Runs_None_Func();

	protected static async Task Test07(Func<Maybe<int>, Func<Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var none = Substitute.For<Func<Task<Maybe<string>>>>();

		// Act
		await act(maybe, none);

		// Assert
		await none.Received().Invoke();
	}

	public abstract Task Test08_If_Some_And_Some_Func_Is_Null_Returns_None_With_SomeFunctionCannotBeNullMsg();

	protected static async Task Test08(Func<Maybe<int>, Func<int, Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var result = await act(maybe, null!);

		// Assert
		result.AssertNone().AssertType<SomeFunctionCannotBeNullMsg>();
	}

	public abstract Task Test09_If_None_And_None_Func_Is_Null_Returns_None_With_NoneFunctionCannotBeNullMsg();

	protected static async Task Test09(Func<Maybe<int>, Func<Task<Maybe<string>>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);

		// Act
		var result = await act(maybe, null!);

		// Assert
		result.AssertNone().AssertType<NoneFunctionCannotBeNullMsg>();
	}

	public abstract Task Test10_If_Unknown_Maybe_Returns_UnknownMaybeTypeMsg();

	protected static async Task Test10(Func<Maybe<int>, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = await act(maybe);

		// Assert
		var msg = result.AssertNone().AssertType<UnknownMaybeTypeMsg>();
		Assert.Equal(typeof(FakeMaybe), msg.MaybeType);
	}

	public abstract Task Test11_If_Null_Returns_None_With_MaybeCannotBeNullMsg(Maybe<int> input);

	protected static async Task Test11(Func<Task<Maybe<string>>> act)
	{
		// Arrange

		// Act
		var result = await act();

		// Assert
		result.AssertNone().AssertType<MaybeCannotBeNullMsg>();
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
