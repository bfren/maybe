// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Enumerable;

public class FilterBind_Tests : Abstracts.Enumerable.FilterBind_Tests
{
	[Fact]
	public override void Test00_Binds_And_Returns_Only_Some_From_List()
	{
		Test00((list, map) => F.EnumerableF.FilterBind(list, map, null));
	}

	[Fact]
	public override void Test01_Returns_Matching_Some_From_List()
	{
		Test01((list, map, predicate) => F.EnumerableF.FilterBind(list, map, predicate));
	}

	[Fact]
	public override void Test02_List_Null_Returns_Empty_List()
	{
		Test02((list, map, predicate) => F.EnumerableF.FilterBind(list, map, predicate));
	}

	[Fact]
	public override void Test03_Bind_Null_Returns_Empty_List()
	{
		Test03((list, map, predicate) => F.EnumerableF.FilterBind(list, map, predicate));
	}
}
