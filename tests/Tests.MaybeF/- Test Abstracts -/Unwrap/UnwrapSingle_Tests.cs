// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class UnwrapSingle_Tests
{
	public abstract void Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test00(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		var message = Assert.IsType<UnhandledExceptionMsg>(none);
		Assert.IsType<UnknownMaybeException>(message.Value);
	}

	public abstract void Test01_None_Returns_None();

	protected static void Test01(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Create.None<int>();

		// Act
		var result = act(maybe);

		// Assert
		result.AssertNone();
	}

	public abstract void Test02_None_With_Msg_Returns_None_With_Msg();

	protected static void Test02(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		Assert.Same(message, none);
	}

	public abstract void Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsMsg();

	protected static void Test03(Func<Maybe<int[]>, Maybe<int>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var maybe = F.Some(empty);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnwrapSingleNoItemsMsg>(none);
	}

	public abstract void Test04_No_Items_Runs_NoItems();

	protected static void Test04(Func<Maybe<int[]>, Func<IMsg>?, Maybe<int>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var maybe = F.Some(empty);
		var noItems = Substitute.For<Func<IMsg>>();

		// Act
		act(maybe, noItems);

		// Assert
		noItems.Received().Invoke();
	}

	public abstract void Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg();

	protected static void Test05(Func<Maybe<int[]>, Maybe<int>> act)
	{
		// Arrange
		var list = new[] { Rnd.Int, Rnd.Int };
		var maybe = F.Some(list);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnwrapSingleTooManyItemsErrorMsg>(none);
	}

	public abstract void Test06_Too_Many_Items_Runs_TooMany();

	protected static void Test06(Func<Maybe<int[]>, Func<IMsg>?, Maybe<int>> act)
	{
		// Arrange
		var list = new[] { Rnd.Int, Rnd.Int };
		var maybe = F.Some(list);
		var tooMany = Substitute.For<Func<IMsg>>();

		// Act
		act(maybe, tooMany);

		// Assert
		tooMany.Received().Invoke();
	}

	public abstract void Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg();

	protected static void Test07(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnwrapSingleNotAListMsg>(none);
	}

	public abstract void Test08_Not_A_List_Runs_NotAList();

	protected static void Test08(Func<Maybe<int>, Func<IMsg>?, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var notAList = Substitute.For<Func<IMsg>>();

		// Act
		act(maybe, notAList);

		// Assert
		notAList.Received().Invoke();
	}

	public abstract void Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorMsg();

	protected static void Test09(Func<Maybe<int[]>, Maybe<string>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { value };
		var maybe = F.Some(list);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnwrapSingleIncorrectTypeErrorMsg>(none);
	}

	public abstract void Test10_List_With_Single_Item_Returns_Single();

	protected static void Test10(Func<Maybe<int[]>, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { value };
		var maybe = F.Some(list);

		// Act
		var result = act(maybe);

		// Assert
		Assert.Equal(value, result);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
