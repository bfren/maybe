// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Xunit;

namespace Maybe.Functions.MaybeF_Tests.Enumerable;

public class ElementAtOrNone_Tests : Tests.Maybe.Abstracts.Enumerable.ElementAtOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_List_Returns_None_With_ListIsEmptyReason()
	{
		Test00((list, index) => MaybeF.EnumerableF.ElementAtOrNone(list, index));
	}

	[Fact]
	public override void Test01_No_Value_At_Index_Returns_None_With_ElementAtIsNullReason()
	{
		Test01((list, index) => MaybeF.EnumerableF.ElementAtOrNone(list, index));
	}

	[Fact]
	public override void Test02_Value_At_Index_Returns_Some_With_Value()
	{
		Test02((list, index) => MaybeF.EnumerableF.ElementAtOrNone(list, index));
	}
}
