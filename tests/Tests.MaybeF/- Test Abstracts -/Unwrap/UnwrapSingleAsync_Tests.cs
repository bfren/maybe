﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using MaybeF.Testing;
using static MaybeF.F.R;

namespace Abstracts;

public abstract class UnwrapSingleAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason();

	protected static async Task Test00(Func<Task<Maybe<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = await act(maybe.AsTask).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		var reason = Assert.IsType<UnhandledExceptionReason>(none);
		_ = Assert.IsType<UnknownMaybeException>(reason.Value);
	}

	public abstract Task Test01_None_Returns_None();

	protected static async Task Test01(Func<Task<Maybe<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();

		// Act
		var result = await act(maybe.AsTask).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
	}

	public abstract Task Test02_None_With_Reason_Returns_None_With_Reason();

	protected static async Task Test02(Func<Task<Maybe<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var reason = new TestReason();
		var maybe = F.None<int>(reason);

		// Act
		var result = await act(maybe.AsTask).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public abstract Task Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsReason();

	protected static async Task Test03(Func<Task<Maybe<int[]>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var maybe = F.Some(empty);

		// Act
		var result = await act(maybe.AsTask).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnwrapSingleNoItemsReason>(none);
	}

	public abstract Task Test04_No_Items_Runs_NoItems();

	protected static async Task Test04(Func<Task<Maybe<int[]>>, Func<IReason>?, Task<Maybe<int>>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var maybe = F.Some(empty);
		var noItems = Substitute.For<Func<IReason>>();

		// Act
		_ = await act(maybe.AsTask, noItems).ConfigureAwait(false);

		// Assert
		_ = noItems.Received().Invoke();
	}

	public abstract Task Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorReason();

	protected static async Task Test05(Func<Task<Maybe<int[]>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var list = new[] { Rnd.Int, Rnd.Int };
		var maybe = F.Some(list);

		// Act
		var result = await act(maybe.AsTask).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnwrapSingleTooManyItemsErrorReason>(none);
	}

	public abstract Task Test06_Too_Many_Items_Runs_TooMany();

	protected static async Task Test06(Func<Task<Maybe<int[]>>, Func<IReason>?, Task<Maybe<int>>> act)
	{
		// Arrange
		var list = new[] { Rnd.Int, Rnd.Int };
		var maybe = F.Some(list);
		var tooMany = Substitute.For<Func<IReason>>();

		// Act
		_ = await act(maybe.AsTask, tooMany).ConfigureAwait(false);

		// Assert
		_ = tooMany.Received().Invoke();
	}

	public abstract Task Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListReason();

	protected static async Task Test07(Func<Task<Maybe<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var result = await act(maybe.AsTask).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnwrapSingleNotAListReason>(none);
	}

	public abstract Task Test08_Not_A_List_Runs_NotAList();

	protected static async Task Test08(Func<Task<Maybe<int>>, Func<IReason>?, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var notAList = Substitute.For<Func<IReason>>();

		// Act
		_ = await act(maybe.AsTask, notAList).ConfigureAwait(false);

		// Assert
		_ = notAList.Received().Invoke();
	}

	public abstract Task Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorReason();

	protected static async Task Test09(Func<Task<Maybe<int[]>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { value };
		var maybe = F.Some(list);

		// Act
		var result = await act(maybe.AsTask).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnwrapSingleIncorrectTypeErrorReason>(none);
	}

	public abstract Task Test10_List_With_Single_Item_Returns_Single();

	protected static async Task Test10(Func<Task<Maybe<int[]>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { value };
		var maybe = F.Some(list);

		// Act
		var result = await act(maybe.AsTask).ConfigureAwait(false);

		// Assert
		Assert.Equal(value, result);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason : IReason;
}