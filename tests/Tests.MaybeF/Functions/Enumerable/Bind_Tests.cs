// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Enumerable;

public class Bind_Tests : Abstracts.Enumerable.Bind_Tests
{
	[Fact]
	public override void Test00_Removes_None_Input_Items()
	{
		Test00(F.EnumerableF.Bind);
	}

	[Fact]
	public override void Test01_Removes_None_Bound_Items()
	{
		Test01(F.EnumerableF.Bind);
	}

	[Fact]
	public override void Test02_Binds_Each_Item()
	{
		Test02(F.EnumerableF.Bind);
	}

	[Fact]
	public override void Test03_List_Null_Returns_Empty_List()
	{
		Test03(F.EnumerableF.Bind);
	}

	[Fact]
	public override void Test04_Map_Null_Returns_Empty_List()
	{
		Test04(F.EnumerableF.Bind);
	}
}
