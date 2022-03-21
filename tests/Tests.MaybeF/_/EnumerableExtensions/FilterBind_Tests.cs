// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.EnumerableExtensions_Tests;

public class FilterBind_Tests : Abstracts.Enumerable.FilterBind_Tests
{
	[Fact]
	public override void Test00_Binds_And_Returns_Only_Some_From_List()
	{
		Test00((list, map) => list.FilterBind(map));
	}

	[Fact]
	public override void Test01_Returns_Matching_Some_From_List()
	{
		Test01((list, map, predicate) => list.FilterBind(map, predicate));
	}
}
