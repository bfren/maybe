// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class UnwrapSingleAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Task<Maybe<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = await act(maybe.AsTask);

		// Assert
		var none = result.AssertNone();
		var message = Assert.IsType<UnhandledExceptionMsg>(none);
		Assert.IsType<UnknownMaybeException>(message.Value);
	}

	public abstract Task Test01_None_Returns_None();

	protected static async Task Test01(Func<Task<Maybe<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();

		// Act
		var result = await act(maybe.AsTask);

		// Assert
		result.AssertNone();
	}

	public abstract Task Test02_None_With_Msg_Returns_None_With_Msg();

	protected static async Task Test02(Func<Task<Maybe<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);

		// Act
		var result = await act(maybe.AsTask);

		// Assert
		var none = result.AssertNone();
		Assert.Same(message, none);
	}

	public abstract Task Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsMsg();

	protected static async Task Test03(Func<Task<Maybe<int[]>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var maybe = F.Some(empty);

		// Act
		var result = await act(maybe.AsTask);

		// Assert
		var none = result.AssertNone().AssertType<UnwrapSingleErrorMsg>();
		Assert.Equal(UnwrapSingleError.NoItems, none.Error);
	}

	public abstract Task Test04_No_Items_Runs_NoItems();

	protected static async Task Test04(Func<Task<Maybe<int[]>>, Func<IMsg>?, Task<Maybe<int>>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var maybe = F.Some(empty);
		var noItems = Substitute.For<Func<IMsg>>();

		// Act
		await act(maybe.AsTask, noItems);

		// Assert
		noItems.Received().Invoke();
	}

	public abstract Task Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg();

	protected static async Task Test05(Func<Task<Maybe<int[]>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var list = new[] { Rnd.Int, Rnd.Int };
		var maybe = F.Some(list);

		// Act
		var result = await act(maybe.AsTask);

		// Assert
		var none = result.AssertNone().AssertType<UnwrapSingleErrorMsg>();
		Assert.Equal(UnwrapSingleError.TooManyItems, none.Error);
	}

	public abstract Task Test06_Too_Many_Items_Runs_TooMany();

	protected static async Task Test06(Func<Task<Maybe<int[]>>, Func<IMsg>?, Task<Maybe<int>>> act)
	{
		// Arrange
		var list = new[] { Rnd.Int, Rnd.Int };
		var maybe = F.Some(list);
		var tooMany = Substitute.For<Func<IMsg>>();

		// Act
		await act(maybe.AsTask, tooMany);

		// Assert
		tooMany.Received().Invoke();
	}

	public abstract Task Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg();

	protected static async Task Test07(Func<Task<Maybe<int>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var result = await act(maybe.AsTask);

		// Assert
		var none = result.AssertNone().AssertType<UnwrapSingleErrorMsg>();
		Assert.Equal(UnwrapSingleError.NotAList, none.Error);
	}

	public abstract Task Test08_Not_A_List_Runs_NotAList();

	protected static async Task Test08(Func<Task<Maybe<int>>, Func<IMsg>?, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var notAList = Substitute.For<Func<IMsg>>();

		// Act
		await act(maybe.AsTask, notAList);

		// Assert
		notAList.Received().Invoke();
	}

	public abstract Task Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorMsg();

	protected static async Task Test09(Func<Task<Maybe<int[]>>, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { value };
		var maybe = F.Some(list);

		// Act
		var result = await act(maybe.AsTask);

		// Assert
		var none = result.AssertNone().AssertType<UnwrapSingleErrorMsg>();
		Assert.Equal(UnwrapSingleError.IncorrectType, none.Error);
	}

	public abstract Task Test10_Incorrect_Type_Runs_IncorrectType();

	protected static async Task Test10(Func<Task<Maybe<int[]>>, Func<IMsg>?, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { value };
		var maybe = F.Some(list);
		var incorrectType = Substitute.For<Func<IMsg>>();

		// Act
		await act(maybe.AsTask, incorrectType);

		// Assert
		incorrectType.Received().Invoke();
	}

	public abstract Task Test11_List_With_Single_Item_Returns_Single();

	protected static async Task Test11(Func<Task<Maybe<int[]>>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { value };
		var maybe = F.Some(list);

		// Act
		var result = await act(maybe.AsTask);

		// Assert
		Assert.Equal(value, result);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
