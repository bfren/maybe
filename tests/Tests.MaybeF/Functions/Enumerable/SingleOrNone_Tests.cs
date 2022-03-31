// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Enumerable;

public class SingleOrNone_Tests : Abstracts.Enumerable.SingleOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg()
	{
		Test00(list => F.EnumerableF.SingleOrNone(list, null));
	}

	[Fact]
	public override void Test01_Multiple_Items_Returns_None_With_MultipleItemsMsg()
	{
		Test01(list => F.EnumerableF.SingleOrNone(list, null));
	}

	[Fact]
	public override void Test02_No_Matching_Items_Returns_None_With_NoMatchingItemsMsg()
	{
		Test02((list, predicate) => F.EnumerableF.SingleOrNone(list, predicate));
	}

	[Fact]
	public override void Test03_Null_Item_Returns_None_With_NullItemMsg()
	{
		Test03((list, predicate) => F.EnumerableF.SingleOrNone(list, predicate));
	}

	[Fact]
	public override void Test04_Returns_Single_Element()
	{
		Test04(list => F.EnumerableF.SingleOrNone(list, null));
	}

	[Fact]
	public override void Test05_Returns_Single_Matching_Element()
	{
		Test05((list, predicate) => F.EnumerableF.SingleOrNone(list, predicate));
	}
}
