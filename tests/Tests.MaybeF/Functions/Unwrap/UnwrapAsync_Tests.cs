// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class UnwrapAsync_Tests : Abstracts.UnwrapAsync_Tests
{
	[Fact]
	public override async Task Test00_None_Runs_IfNone_Func_Returns_Value()
	{
		await Test00((mbe, ifNone) => F.UnwrapAsync(mbe, x => x.Value(ifNone()))).ConfigureAwait(false);
		await Test00((mbe, ifNone) => F.UnwrapAsync(mbe, x => x.Value(ifNone))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value()
	{
		await Test01((mbe, ifNone) => F.UnwrapAsync(mbe, x => x.Value(ifNone))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_Some_Returns_Value()
	{
		await Test02(mbe => F.UnwrapAsync(mbe, x => x.Value(Rnd.Int))).ConfigureAwait(false);
		await Test02(mbe => F.UnwrapAsync(mbe, x => x.Value(Substitute.For<Func<int>>()))).ConfigureAwait(false);
		await Test02(mbe => F.UnwrapAsync(mbe, x => x.Value(Substitute.For<Func<IReason, int>>()))).ConfigureAwait(false);
	}
}
