// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class UnwrapAsync_Tests : Abstracts.UnwrapAsync_Tests
{
	[Fact]
	public override async Task Test00_None_Runs_IfNone_Func_Returns_Value()
	{
		await Test00((mbe, ifNone) => mbe.UnwrapAsync(x => x.Value(ifNone()))).ConfigureAwait(false);
		await Test00((mbe, ifNone) => mbe.UnwrapAsync(x => x.Value(ifNone))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_None_With_Msg_Runs_IfNone_Func_Passes_Msg_Returns_Value()
	{
		await Test01((mbe, ifNone) => mbe.UnwrapAsync(x => x.Value(ifNone))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_Some_Returns_Value()
	{
		await Test02(mbe => mbe.UnwrapAsync(x => x.Value(Rnd.Int))).ConfigureAwait(false);
		await Test02(mbe => mbe.UnwrapAsync(x => x.Value(Substitute.For<Func<int>>()))).ConfigureAwait(false);
		await Test02(mbe => mbe.UnwrapAsync(x => x.Value(Substitute.For<Func<IMsg, int>>()))).ConfigureAwait(false);
	}
}
