// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.EnumerableExtensions_Tests;

public class Filter_Tests : Abstracts.Enumerable.Filter_Tests
{
	[Fact]
	public override void Test00_Maps_And_Returns_Only_Some_From_List()
	{
		Test00(list => list.Filter());
	}

	[Fact]
	public override void Test01_Maps_And_Returns_Matching_Some_From_List()
	{
		Test01((list, predicate) => list.Filter(predicate));
	}

	[Fact]
	public override void Test02_Null_Input_Returns_Empty_List()
	{
		Test02((list, predicate) => list.Filter(predicate));
	}
}
