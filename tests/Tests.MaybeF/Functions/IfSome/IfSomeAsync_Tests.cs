﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class IfSomeAsync_Tests : Abstracts.IfSomeAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_In_IfSome_Func_Returns_None_With_UnhandledExceptionReason()
	{
		await Test00((mbe, ifSome) => F.IfSomeAsync(mbe, ifSome)).ConfigureAwait(false);
		await Test00((mbe, ifSome) => F.IfSomeAsync(mbe.AsTask, ifSome)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_None_Returns_Original_Option()
	{
		await Test01((mbe, ifSome) => F.IfSomeAsync(mbe, ifSome)).ConfigureAwait(false);
		await Test01((mbe, ifSome) => F.IfSomeAsync(mbe.AsTask, ifSome)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_Some_Runs_IfSome_Func_And_Returns_Original_Option()
	{
		await Test02((mbe, ifSome) => F.IfSomeAsync(mbe, ifSome)).ConfigureAwait(false);
		await Test02((mbe, ifSome) => F.IfSomeAsync(mbe.AsTask, ifSome)).ConfigureAwait(false);
	}
}
