// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Enumerable;

public class IterateAsync_Tests : Abstracts.Enumerable.IterateAsync_Tests
{
	[Fact]
	public override async Task Test00_List_Is_Empty_Does_Nothing()
	{
		await Test00((list, f) => F.EnumerableF.IterateAsync(list, f));
	}

	[Fact]
	public override async Task Test01_Ignores_None_Values()
	{
		await Test01((list, f) => F.EnumerableF.IterateAsync(list, f));
	}

	[Fact]
	public override async Task Test02_Runs_Func_For_Some_Values()
	{
		await Test02((list, f) => F.EnumerableF.IterateAsync(list, f));
	}
}
