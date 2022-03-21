// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Enumerable;

public class Map_Tests : Abstracts.Enumerable.Map_Tests
{
	[Fact]
	public override void Test00_Without_Map_Converts_Each_Item()
	{
		Test00(F.EnumerableF.Map);
	}

	[Fact]
	public override void Test01_Without_Map_Removes_Nullable_Items()
	{
		Test01(F.EnumerableF.Map);
	}

	[Fact]
	public override void Test02_With_Map_Converts_Each_Item()
	{
		Test02(F.EnumerableF.Map);
	}

	[Fact]
	public override void Test03_With_Map_Removes_Nullable_Items()
	{
		Test03(F.EnumerableF.Map);
	}

	[Fact]
	public override void Test04_With_Map_Removes_None_Values()
	{
		Test04(F.EnumerableF.Map);
	}
}
