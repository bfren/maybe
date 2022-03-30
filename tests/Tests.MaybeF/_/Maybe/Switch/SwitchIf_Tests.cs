// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class SwitchIf_Tests : Abstracts.SwitchIf_Tests
{
	[Fact]
	public override void Test00_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		Test00((mbe, check) => mbe.SwitchIf(check, null, null));
		Test00((mbe, check) => mbe.SwitchIf(check, ifFalse));
	}

	[Fact]
	public override void Test02_None_Returns_Original_None()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		Test02((mbe, check) => mbe.SwitchIf(check, null, null));
		Test02((mbe, check) => mbe.SwitchIf(check, ifFalse));
	}

	[Fact]
	public override void Test03_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		Test03((mbe, check) => mbe.SwitchIf(check, null, null));
		Test03((mbe, check) => mbe.SwitchIf(check, ifFalse));
	}

	[Fact]
	public override void Test04_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Maybe()
	{
		Test04((mbe, check) => mbe.SwitchIf(check, null, null));
	}

	[Fact]
	public override void Test05_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Maybe()
	{
		Test05((mbe, check) => mbe.SwitchIf(check, null, null));
	}

	[Fact]
	public override void Test06_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		Test06((mbe, check, ifTrue) => mbe.SwitchIf(check, ifTrue, null));
	}

	[Fact]
	public override void Test07_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		Test07((mbe, check, ifFalse) => mbe.SwitchIf(check, null, ifFalse));
		Test07((mbe, check, ifFalse) => mbe.SwitchIf(check, x => ifFalse(x).Msg));
	}

	[Fact]
	public override void Test08_Check_Returns_True_Runs_IfTrue_Returns_Value()
	{
		Test08((mbe, check, ifTrue) => mbe.SwitchIf(check, ifTrue, null));
	}

	[Fact]
	public override void Test09_Check_Returns_False_Runs_IfFalse_Returns_Value()
	{
		Test09((mbe, check, ifFalse) => mbe.SwitchIf(check, null, ifFalse));
		Test09((mbe, check, ifFalse) => mbe.SwitchIf(check, x => ifFalse(x).Msg));
	}

	#region Unused

	[Theory]
	[InlineData(null)]
	public override void Test01_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input) =>
		Assert.Null(input);

	#endregion Unused
}
