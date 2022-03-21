// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using static MaybeF.F.EnumerableF.R;

namespace Abstracts.Enumerable;

public abstract class ElementAtOrNone_Tests
{
	public abstract void Test00_Empty_List_Returns_None_With_ListIsEmptyReason();

	protected static void Test00(Func<IEnumerable<int>, int, Maybe<int>> act)
	{
		// Arrange
		var list = Array.Empty<int>();

		// Act
		var result = act(list, 0);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ListIsEmptyReason>(none);
	}

	public abstract void Test01_No_Value_At_Index_Returns_None_With_ElementAtIsNullReason();

	protected static void Test01(Func<IEnumerable<int?>, int, Maybe<int?>> act)
	{
		// Arrange
		var list = new int?[] { Rnd.Int, Rnd.Int, Rnd.Int };

		// Act
		var result = act(list, 4);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ElementAtIsNullReason>(none);
	}

	public abstract void Test02_Value_At_Index_Returns_Some_With_Value();

	protected static void Test02(Func<IEnumerable<int>, int, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { Rnd.Int, value, Rnd.Int };

		// Act
		var result = act(list, 1);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}
}
