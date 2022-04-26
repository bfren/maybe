// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.EnumerableExtensions_Tests;

public class Map_Tests : Abstracts.Enumerable.Map_Tests
{
	[Fact]
	public override void Test00_Without_Map_Converts_Each_Item()
	{
		Test00(list => list.Map());
	}

	[Fact]
	public override void Test01_Without_Map_Removes_Nullable_Items()
	{
		Test01(list => list.Map());
	}

	[Fact]
	public override void Test02_With_Map_Converts_Each_Item()
	{
		Test02((list, map) => list.Map(map));
	}

	[Fact]
	public override void Test03_With_Map_Removes_Nullable_Items()
	{
		Test03((list, map) => list.Map(map));
	}

	[Fact]
	public override void Test04_With_Map_Removes_None_Values()
	{
		Test04((list, map) => list.Map(map));
	}

	[Fact]
	public override void Test05_List_Null_Returns_Empty_List()
	{
		Test05((list, map) => list.Map(map));
	}

	[Fact]
	public override void Test06_Map_Null_Returns_Empty_List()
	{
		Test06((list, map) => list.Map(map));
	}
}
