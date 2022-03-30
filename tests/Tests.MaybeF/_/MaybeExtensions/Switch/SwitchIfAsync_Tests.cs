// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class SwitchIfAsync_Tests : Abstracts.SwitchIfAsync_Tests
{
	[Fact]
	public override async Task Test00_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test00((mbe, check) => mbe.SwitchIfAsync(check, null, null)).ConfigureAwait(false);
		await Test00((mbe, check) => mbe.SwitchIfAsync(check, ifFalse)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_None_Returns_Original_None()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test02((mbe, check) => mbe.SwitchIfAsync(check, null, null)).ConfigureAwait(false);
		await Test02((mbe, check) => mbe.SwitchIfAsync(check, ifFalse)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test03((mbe, check) => mbe.SwitchIfAsync(check, null, null)).ConfigureAwait(false);
		await Test03((mbe, check) => mbe.SwitchIfAsync(check, ifFalse)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Maybe()
	{
		await Test04((mbe, check) => mbe.SwitchIfAsync(check, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test05_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Maybe()
	{
		await Test05((mbe, check) => mbe.SwitchIfAsync(check, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test06_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		await Test06((mbe, check, ifTrue) => mbe.SwitchIfAsync(check, ifTrue, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test07_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		await Test07((mbe, check, ifFalse) => mbe.SwitchIfAsync(check, null, ifFalse)).ConfigureAwait(false);
		await Test07((mbe, check, ifFalse) => mbe.SwitchIfAsync(check, x => ifFalse(x).Msg)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test08_Check_Returns_True_Runs_IfTrue_Returns_Value()
	{
		await Test08((mbe, check, ifTrue) => mbe.SwitchIfAsync(check, ifTrue, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test09_Check_Returns_False_Runs_IfFalse_Returns_Value()
	{
		await Test09((mbe, check, ifFalse) => mbe.SwitchIfAsync(check, null, ifFalse)).ConfigureAwait(false);
		await Test09((mbe, check, ifFalse) => mbe.SwitchIfAsync(check, x => ifFalse(x).Msg)).ConfigureAwait(false);
	}

	#region Unused

	public override Task Test01_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input) =>
		Task.CompletedTask;

	#endregion Unused
}
