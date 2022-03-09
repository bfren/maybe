// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Xunit;

namespace Maybe.Functions.MaybeF_Tests.Enumerable;

public class FilterMap_Tests : Tests.Maybe.Abstracts.Enumerable.FilterMap_Tests
{
	[Fact]
	public override void Test00_Maps_And_Returns_Only_Some_From_List()
	{
		Test00((list, map) => MaybeF.EnumerableF.FilterMap(list, map, null));
	}

	[Fact]
	public override void Test01_Maps_And_Returns_Only_Some_From_List()
	{
		Test01((list, map) => MaybeF.EnumerableF.FilterMap(list, map, null));
	}

	[Fact]
	public override void Test02_Returns_Matching_Some_From_List()
	{
		Test02((list, map, predicate) => MaybeF.EnumerableF.FilterMap(list, map, predicate));
	}

	[Fact]
	public override void Test03_Returns_Matching_Some_From_List()
	{
		Test03((list, map, predicate) => MaybeF.EnumerableF.FilterMap(list, map, predicate));
	}
}
