// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class SwitchIfAsync_Tests : Abstracts.SwitchIfAsync_Tests
{
	[Fact]
	public override async Task Test00_Unknown_Maybe_Returns_None_With_UnknownMaybeTypeMsg()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test00((mbe, check) => mbe.SwitchIfAsync(check, null, null));
		await Test00((mbe, check) => mbe.SwitchIfAsync(check, ifFalse));
	}

	[Fact]
	public override async Task Test02_Predicate_Null_Returns_None_With_SwitchIfPredicateCannotBeNullMsg()
	{
		await Test02((mbe, check) => mbe.SwitchIfAsync(check, null, null));
		await Test02((mbe, check) => mbe.SwitchIfAsync(check, null, null));
	}

	[Fact]
	public override async Task Test03_None_Returns_Original_None()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test03((mbe, check) => mbe.SwitchIfAsync(check, null, null));
		await Test03((mbe, check) => mbe.SwitchIfAsync(check, ifFalse));
	}

	[Fact]
	public override async Task Test04_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test04((mbe, check) => mbe.SwitchIfAsync(check, null, null));
		await Test04((mbe, check) => mbe.SwitchIfAsync(check, ifFalse));
	}

	[Fact]
	public override async Task Test05_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Maybe()
	{
		await Test05((mbe, check) => mbe.SwitchIfAsync(check, null, null));
	}

	[Fact]
	public override async Task Test06_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Maybe()
	{
		await Test06((mbe, check) => mbe.SwitchIfAsync(check, null, null));
	}

	[Fact]
	public override async Task Test07_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		await Test07((mbe, check, ifTrue) => mbe.SwitchIfAsync(check, ifTrue, null));
	}

	[Fact]
	public override async Task Test08_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		await Test08((mbe, check, ifFalse) => mbe.SwitchIfAsync(check, null, ifFalse));
		await Test08((mbe, check, ifFalse) => mbe.SwitchIfAsync(check, x => ifFalse(x).Reason));
	}

	[Fact]
	public override async Task Test09_Check_Returns_True_Runs_IfTrue_Returns_Value()
	{
		await Test09((mbe, check, ifTrue) => mbe.SwitchIfAsync(check, ifTrue, null));
	}

	[Fact]
	public override async Task Test10_Check_Returns_False_Runs_IfFalse_Returns_Value()
	{
		await Test10((mbe, check, ifFalse) => mbe.SwitchIfAsync(check, null, ifFalse));
		await Test10((mbe, check, ifFalse) => mbe.SwitchIfAsync(check, x => ifFalse(x).Reason));
	}

	[Fact]
	public override async Task Test11_Is_Some__Returns_Result_Of_Check()
	{
		await Test11((mbe, check) => mbe.SwitchIfAsync(check));
	}

	[Fact]
	public override async Task Test12_Is_None__Returns_False()
	{
		await Test12((mbe, check) => mbe.SwitchIfAsync(check));
	}

	#region Unused

	[Theory]
	[InlineData(null)]
	public override Task Test01_If_Null_Returns_None_With_MaybeCannotBeNullMsg(Maybe<int> input) =>
		Task.FromResult(input);

	#endregion Unused
}
