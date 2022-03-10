// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Xunit;

namespace MaybeF.Linq.EnumerableExtensions_Tests;

public class SingleOrNone_Tests : Abstracts.Enumerable.SingleOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_List_Returns_None_With_ListIsEmptyReason()
	{
		Test00(list => list.SingleOrNone());
	}

	[Fact]
	public override void Test01_Multiple_Items_Returns_None_With_MultipleItemsReason()
	{
		Test01(list => list.SingleOrNone());
	}

	[Fact]
	public override void Test02_No_Matching_Items_Returns_None_With_NoMatchingItemsReason()
	{
		Test02((list, predicate) => list.SingleOrNone(predicate));
	}

	[Fact]
	public override void Test03_Null_Item_Returns_None_With_NullItemReason()
	{
		Test03((list, predicate) => list.SingleOrNone(predicate));
	}

	[Fact]
	public override void Test04_Returns_Single_Element()
	{
		Test04(list => list.SingleOrNone());
	}

	[Fact]
	public override void Test05_Returns_Single_Matching_Element()
	{
		Test05((list, predicate) => list.SingleOrNone(predicate));
	}
}
