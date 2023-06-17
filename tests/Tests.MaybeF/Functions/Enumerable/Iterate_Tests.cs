// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Enumerable;

public class Iterate_Tests : Abstracts.Enumerable.Iterate_Tests
{
	[Fact]
	public override void Test00_List_Is_Empty_Does_Nothing()
	{
		Test00((list, f) => F.EnumerableF.Iterate(list, f));
	}

	[Fact]
	public override void Test01_Ignores_None_Values()
	{
		Test01((list, f) => F.EnumerableF.Iterate(list, f));
	}

	[Fact]
	public override void Test02_Runs_Func_For_Some_Values()
	{
		Test02((list, f) => F.EnumerableF.Iterate(list, f));
	}
}
