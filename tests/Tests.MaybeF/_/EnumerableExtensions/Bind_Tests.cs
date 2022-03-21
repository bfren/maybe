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
	public override void Test01_Removes_None_Bound_Items()
	{
		Test01((list, bind) => list.Bind(bind));
	}

	[Fact]
	public override void Test02_Binds_Each_Item()
	{
		Test02((list, bind) => list.Bind(bind));
	}
}
