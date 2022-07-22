// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class FilterAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Maybe<int>, Task<Maybe<int>>> actTask, Func<Maybe<int>, ValueTask<Maybe<int>>>? actValueTask)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var r0 = await actTask(maybe);
		var r1 = actValueTask switch
		{
			{ } t =>
				await t(maybe),

			_ =>
				null
		};

		// Assert
		var m0 = r0.AssertNone().AssertType<UnhandledExceptionMsg>();
		Assert.IsType<UnknownMaybeException>(m0.Value);
		var m1 = r1?.AssertNone().AssertType<UnhandledExceptionMsg>();
		if (m1 is not null)
		{
			Assert.IsType<UnknownMaybeException>(m1?.Value);
		}
	}

	public abstract Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test01(Func<Maybe<string>, Func<string, Task<bool>>, Task<Maybe<string>>> actTask, Func<Maybe<string>, Func<string, ValueTask<bool>>, ValueTask<Maybe<string>>>? actValueTask)
	{
		// Arrange
		var maybe = F.Some(Rnd.Str);
		var exception = new Exception();
		var throwFuncTask = Task<bool> (string _) => throw exception;
		var throwFuncValueTask = ValueTask<bool> (string _) => throw exception;

		// Act
		var r0 = await actTask(maybe, throwFuncTask);
		var r1 = actValueTask switch
		{
			{ } t =>
				await t(maybe, throwFuncValueTask),

			_ =>
				null
		};

		// Assert
		r0.AssertNone().AssertType<UnhandledExceptionMsg>();
		r1?.AssertNone().AssertType<UnhandledExceptionMsg>();
	}

	public abstract Task Test02_When_Some_And_Predicate_True_Returns_Value();

	protected static async Task Test02(Func<Maybe<int>, Func<int, Task<bool>>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Func<int, ValueTask<bool>>, ValueTask<Maybe<int>>>? actValueTask)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var taskPredicate = Substitute.For<Func<int, Task<bool>>>();
		taskPredicate.Invoke(Arg.Any<int>()).Returns(true);
		var valueTaskPredicate = Substitute.For<Func<int, ValueTask<bool>>>();
		valueTaskPredicate.Invoke(Arg.Any<int>()).Returns(true);

		// Act
		var r0 = await actTask(maybe, taskPredicate);
		var r1 = actValueTask switch
		{
			{ } t =>
				await t(maybe, valueTaskPredicate),

			_ =>
				null
		};

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value, s0);
		var s1 = r1?.AssertSome();
		if (s1 is not null)
		{
			Assert.Equal(value, s1);
		}
	}

	public abstract Task Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg();

	protected static async Task Test03(Func<Maybe<string>, Func<string, Task<bool>>, Task<Maybe<string>>> actTask, Func<Maybe<string>, Func<string, ValueTask<bool>>, ValueTask<Maybe<string>>>? actValueTask)
	{
		// Arrange
		var value = Rnd.Str;
		var maybe = F.Some(value);
		var taskPredicate = Substitute.For<Func<string, Task<bool>>>();
		taskPredicate.Invoke(Arg.Any<string>()).Returns(false);
		var valueTaskPredicate = Substitute.For<Func<string, ValueTask<bool>>>();
		valueTaskPredicate.Invoke(Arg.Any<string>()).Returns(false);

		// Act
		var r0 = await actTask(maybe, taskPredicate);
		var r1 = actValueTask switch
		{
			{ } t =>
				await t(maybe, valueTaskPredicate),

			_ =>
				null
		};

		// Assert
		r0.AssertNone().AssertType<FilterPredicateWasFalseMsg>();
		r1?.AssertNone().AssertType<FilterPredicateWasFalseMsg>();
	}

	public abstract Task Test04_When_None_Returns_None_With_Original_Msg();

	protected static async Task Test04(Func<Maybe<int>, Func<int, Task<bool>>, Task<Maybe<int>>> actTask, Func<Maybe<int>, Func<int, ValueTask<bool>>, ValueTask<Maybe<int>>>? actValueTask)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var taskPredicate = Substitute.For<Func<int, Task<bool>>>();
		var valueTaskPredicate = Substitute.For<Func<int, ValueTask<bool>>>();

		// Act
		var r0 = await actTask(maybe, taskPredicate);
		var r1 = actValueTask switch
		{
			{ } t =>
				await t(maybe, valueTaskPredicate),

			_ =>
				null
		};

		// Assert
		var n0 = r0.AssertNone();
		Assert.Same(message, n0);
		await taskPredicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		var n1 = r1?.AssertNone();
		if (n1 is not null)
		{
			Assert.Same(message, n1);
			await taskPredicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		}
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
