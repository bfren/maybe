﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class SwitchIfAsync_Tests : Abstracts.SwitchIfAsync_Tests
{
	[Fact]
	public override async Task Test00_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var ifFalse = Substitute.For<Func<int, IReason>>();
		await Test00((mbe, check) => F.SwitchIfAsync(mbe, check, null, null)).ConfigureAwait(false);
		await Test00((mbe, check) => F.SwitchIfAsync(mbe, check, ifFalse)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_None_Returns_Original_None()
	{
		var ifFalse = Substitute.For<Func<int, IReason>>();
		await Test01((mbe, check) => F.SwitchIfAsync(mbe, check, null, null)).ConfigureAwait(false);
		await Test01((mbe, check) => F.SwitchIfAsync(mbe, check, ifFalse)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionReason()
	{
		var ifFalse = Substitute.For<Func<int, IReason>>();
		await Test02((mbe, check) => F.SwitchIfAsync(mbe, check, null, null)).ConfigureAwait(false);
		await Test02((mbe, check) => F.SwitchIfAsync(mbe, check, ifFalse)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Option()
	{
		await Test03((mbe, check) => F.SwitchIfAsync(mbe, check, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Option()
	{
		await Test04((mbe, check) => F.SwitchIfAsync(mbe, check, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test05_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionReason()
	{
		await Test05((mbe, check, ifTrue) => F.SwitchIfAsync(mbe, check, ifTrue, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test06_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionReason()
	{
		await Test06((mbe, check, ifFalse) => F.SwitchIfAsync(mbe, check, null, ifFalse)).ConfigureAwait(false);
		await Test06((mbe, check, ifFalse) => F.SwitchIfAsync(mbe, check, x => ifFalse(x).Reason)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test07_Check_Returns_True_Runs_IfTrue_Returns_Value()
	{
		await Test07((mbe, check, ifTrue) => F.SwitchIfAsync(mbe, check, ifTrue, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test08_Check_Returns_False_Runs_IfFalse_Returns_Value()
	{
		await Test08((mbe, check, ifFalse) => F.SwitchIfAsync(mbe, check, null, ifFalse)).ConfigureAwait(false);
		await Test08((mbe, check, ifFalse) => F.SwitchIfAsync(mbe, check, x => ifFalse(x).Reason)).ConfigureAwait(false);
	}
}