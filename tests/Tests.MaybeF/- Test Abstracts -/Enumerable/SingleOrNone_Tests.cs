// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using static MaybeF.F.EnumerableF.M;

namespace Abstracts.Enumerable;

public abstract class SingleOrNone_Tests
{
	public abstract void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg();

	protected static void Test00(Func<IEnumerable<int>, Maybe<int>> act)
	{
		// Arrange
		var list = Array.Empty<int>();

		// Act
		var r0 = act(null!);
		var r1 = act(list);

		// Assert
		r0.AssertNone().AssertType<ListIsEmptyMsg>();
		r1.AssertNone().AssertType<ListIsEmptyMsg>();
	}

	public abstract void Test01_Multiple_Items_Returns_None_With_MultipleItemsMsg();

	protected static void Test01(Func<IEnumerable<int>, Maybe<int>> act)
	{
		// Arrange
		var list = new int[] { Rnd.Int, Rnd.Int, Rnd.Int };

		// Act
		var result = act(list);

		// Assert
		result.AssertNone().AssertType<MultipleItemsMsg>();
	}

	public abstract void Test02_No_Matching_Items_Returns_None_With_NoMatchingItemsMsg();

	protected static void Test02(Func<IEnumerable<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var list = new int[] { Rnd.Int, Rnd.Int, Rnd.Int };
		var predicate = Substitute.For<Func<int, bool>>();
		predicate.Invoke(Arg.Any<int>()).Returns(false);

		// Act
		var result = act(list, predicate);

		// Assert
		result.AssertNone().AssertType<NoMatchingItemsMsg>();
	}

	public abstract void Test03_Null_Item_Returns_None_With_NullItemMsg();

	protected static void Test03(Func<IEnumerable<int?>, Func<int?, bool>, Maybe<int?>> act)
	{
		// Arrange
		var list = new int?[] { Rnd.Int, null, Rnd.Int };
		var predicate = Substitute.For<Func<int?, bool>>();
		predicate.Invoke(null).Returns(true);

		// Act
		var result = act(list, predicate);

		// Assert
		result.AssertNone().AssertType<NullItemMsg>();
	}

	public abstract void Test04_Returns_Single_Element();

	protected static void Test04(Func<IEnumerable<int>, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { value };

		// Act
		var result = act(list);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract void Test05_Returns_Single_Matching_Element();

	protected static void Test05(Func<IEnumerable<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { Rnd.Int, value, Rnd.Int };
		var predicate = Substitute.For<Func<int, bool>>();
		predicate.Invoke(value).Returns(true);

		// Act
		var result = act(list, predicate);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}
}
