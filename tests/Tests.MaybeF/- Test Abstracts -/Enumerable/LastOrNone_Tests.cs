﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using static MaybeF.F.EnumerableF.R;

namespace Abstracts.Enumerable;

public abstract class LastOrNone_Tests
{
	public abstract void Test00_Empty_List_Returns_None_With_ListIsEmptyReason();

	protected static void Test00(Func<IEnumerable<int>, Maybe<int>> act)
	{
		// Arrange
		var list = Array.Empty<int>();

		// Act
		var result = act(list);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<ListIsEmptyReason>(none);
	}

	public abstract void Test01_No_Matching_Items_Returns_None_With_LastItemIsNullReason();

	protected static void Test01(Func<IEnumerable<int?>, Func<int?, bool>, Maybe<int?>> act)
	{
		// Arrange
		var list = new int?[] { Rnd.Int, Rnd.Int, Rnd.Int };
		var predicate = Substitute.For<Func<int?, bool>>();
		_ = predicate.Invoke(Arg.Any<int?>()).Returns(false);

		// Act
		var result = act(list, predicate);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<LastItemIsNullReason>(none);
	}

	public abstract void Test02_Returns_Last_Element();

	protected static void Test02(Func<IEnumerable<int>, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { Rnd.Int, Rnd.Int, value };

		// Act
		var result = act(list);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract void Test03_Returns_Last_Matching_Element();

	protected static void Test03(Func<IEnumerable<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var list = new[] { Rnd.Int, value, Rnd.Int };
		var predicate = Substitute.For<Func<int, bool>>();
		_ = predicate.Invoke(value).Returns(true);

		// Act
		var result = act(list, predicate);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}
}
