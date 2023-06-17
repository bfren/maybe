// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.EnumerableExtensions_Tests;

public class Bind_Tests : Abstracts.Enumerable.Bind_Tests
{
	[Fact]
	public override void Test00_Removes_None_Input_Items()
	{
		Test00((list, bind) => list.Bind(bind));
	}

	[Fact]
	public override void Test01_Binds_Each_Item()
	{
		Test01((list, bind) => list.Bind(bind));
	}

	[Fact]
	public override void Test02_List_Null_Returns_Empty_List()
	{
		Test02((list, bind) => list.Bind(bind));
	}

	[Fact]
	public override void Test03_Map_Null_Returns_Empty_List()
	{
		Test03((list, bind) => list.Bind(bind));
	}
}
