﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using MaybeF.Internals;
using MaybeF.Testing.Exceptions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class SwitchIfAsync_Tests
{
	public abstract Task Test00_Unknown_Maybe_Throws_UnknownMaybeException();

	protected static async Task Test00(Func<Task<Maybe<int>>, Func<int, bool>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var check = Substitute.For<Func<int, bool>>();

		// Act
		var action = Task () => act(maybe.AsTask, check);

		// Assert
		await Assert.ThrowsAsync<UnknownMaybeException>(action).ConfigureAwait(false);
	}

	public abstract Task Test01_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input);

	protected static async Task Test01(Func<Func<int, bool>, Task<Maybe<int>>> act)
	{
		// Arrange
		var check = Substitute.For<Func<int, bool>>();

		// Act
		var action = Task () => act(check);

		// Assert
		await Assert.ThrowsAsync<MaybeCannotBeNullException>(action).ConfigureAwait(false);
	}

	public abstract Task Test02_None_Returns_Original_None();

	protected static async Task Test02(Func<Task<Maybe<int>>, Func<int, bool>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var check = Substitute.For<Func<int, bool>>();

		// Act
		var result = await act(maybe.AsTask, check).ConfigureAwait(false);

		// Assert
		result.AssertNone();
		Assert.Same(maybe, result);
	}

	public abstract Task Test03_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

	protected static async Task Test03(Func<Task<Maybe<int>>, Func<int, bool>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = bool (int _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe.AsTask, check).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<SwitchIfFuncExceptionMsg>(none);
	}

	public abstract Task Test04_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Maybe();

	protected static async Task Test04(Func<Task<Maybe<int>>, Func<int, bool>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(Arg.Any<int>()).Returns(true);

		// Act
		var result = await act(maybe.AsTask, check).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test05_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Maybe();

	protected static async Task Test05(Func<Task<Maybe<int>>, Func<int, bool>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(Arg.Any<int>()).Returns(false);

		// Act
		var result = await act(maybe.AsTask, check).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test06_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

	protected static async Task Test06(Func<Task<Maybe<int>>, Func<int, bool>, Func<int, None<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(Arg.Any<int>()).Returns(true);
		var ifTrue = None<int> (int _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe.AsTask, check, ifTrue).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<SwitchIfFuncExceptionMsg>(none);
	}

	public abstract Task Test07_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

	protected static async Task Test07(Func<Task<Maybe<int>>, Func<int, bool>, Func<int, None<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(Arg.Any<int>()).Returns(false);
		var ifFalse = None<int> (int _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe.AsTask, check, ifFalse).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<SwitchIfFuncExceptionMsg>(none);
	}

	public abstract Task Test08_Check_Returns_True_Runs_IfTrue_Returns_Value();

	protected static async Task Test08(Func<Task<Maybe<int>>, Func<int, bool>, Func<int, Maybe<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var maybe = F.Some(v0);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(v0).Returns(true);
		var ifTrue = Substitute.For<Func<int, Maybe<int>>>();
		ifTrue.Invoke(v0).Returns(F.Some(v0 + v1));

		// Act
		var result = await act(maybe.AsTask, check, ifTrue).ConfigureAwait(false);

		// Assert
		ifTrue.Received().Invoke(v0);
		var some = result.AssertSome();
		Assert.Equal(v0 + v1, some);
	}

	public abstract Task Test09_Check_Returns_False_Runs_IfFalse_Returns_Value();

	protected static async Task Test09(Func<Task<Maybe<int>>, Func<int, bool>, Func<int, None<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var check = Substitute.For<Func<int, bool>>();
		check.Invoke(value).Returns(false);
		var ifFalse = Substitute.For<Func<int, None<int>>>();
		ifFalse.Invoke(value).Returns(F.None<int, TestMsg>());

		// Act
		var result = await act(maybe.AsTask, check, ifFalse).ConfigureAwait(false);

		// Assert
		ifFalse.Received().Invoke(value);
		var none = result.AssertNone();
		Assert.IsType<TestMsg>(none);
	}

	public record class FakeMaybe : Maybe<int> { }

	public sealed record class TestMsg : IMsg;
}
