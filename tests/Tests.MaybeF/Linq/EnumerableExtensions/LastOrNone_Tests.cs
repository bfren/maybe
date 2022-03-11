// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Linq.EnumerableExtensions_Tests;

public class LastOrNone_Tests : Abstracts.Enumerable.LastOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_List_Returns_None_With_ListIsEmptyReason()
	{
		Test00(list => list.LastOrNone());
	}

	[Fact]
	public override void Test01_No_Matching_Items_Returns_None_With_LastItemIsNullReason()
	{
		Test01((list, predicate) => list.LastOrNone(predicate));
	}

	[Fact]
	public override void Test02_Returns_Last_Element()
	{
		Test02(list => list.LastOrNone());
	}

	[Fact]
	public override void Test03_Returns_Last_Matching_Element()
	{
		Test03((list, predicate) => list.LastOrNone(predicate));
	}
}
