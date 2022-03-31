// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Enumerable;

public class ElementAtOrNone_Tests : Abstracts.Enumerable.ElementAtOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg()
	{
		Test00((list, index) => F.EnumerableF.ElementAtOrNone(list, index));
	}

	[Fact]
	public override void Test01_No_Value_At_Index_Returns_None_With_ElementAtIsNullMsg()
	{
		Test01((list, index) => F.EnumerableF.ElementAtOrNone(list, index));
	}

	[Fact]
	public override void Test02_Value_At_Index_Returns_Some_With_Value()
	{
		Test02((list, index) => F.EnumerableF.ElementAtOrNone(list, index));
	}
}
