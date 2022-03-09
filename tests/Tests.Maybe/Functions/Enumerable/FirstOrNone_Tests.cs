// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Xunit;

namespace Maybe.Functions.MaybeF_Tests.Enumerable;

public class FirstOrNone_Tests : Tests.Maybe.Abstracts.Enumerable.FirstOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_List_Returns_None_With_ListIsEmptyReason()
	{
		Test00(list => MaybeF.EnumerableF.FirstOrNone(list, null));
	}

	[Fact]
	public override void Test01_No_Matching_Items_Returns_None_With_FirstItemIsNullReason()
	{
		Test01((list, predicate) => MaybeF.EnumerableF.FirstOrNone(list, predicate));
	}

	[Fact]
	public override void Test02_Returns_First_Element()
	{
		Test02(list => MaybeF.EnumerableF.FirstOrNone(list, null));
	}

	[Fact]
	public override void Test03_Returns_First_Matching_Element()
	{
		Test03((list, predicate) => MaybeF.EnumerableF.FirstOrNone(list, predicate));
	}
}
