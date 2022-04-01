// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class IfSomeAsync_Tests : Abstracts.IfSomeAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_In_IfSome_Func_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((mbe, ifSome) => mbe.AsTask.IfSomeAsync(x => ifSome(x)));
		await Test00((mbe, ifSome) => mbe.AsTask.IfSomeAsync(ifSome));
	}

	[Fact]
	public override async Task Test01_None_Returns_Original_Maybe()
	{
		await Test01((mbe, ifSome) => mbe.AsTask.IfSomeAsync(x => ifSome(x)));
		await Test01((mbe, ifSome) => mbe.AsTask.IfSomeAsync(ifSome));
	}

	[Fact]
	public override async Task Test02_Some_Runs_IfSome_Func_And_Returns_Original_Maybe()
	{
		await Test02((mbe, ifSome) => mbe.AsTask.IfSomeAsync(x => ifSome(x)));
		await Test02((mbe, ifSome) => mbe.AsTask.IfSomeAsync(ifSome));
	}
}
