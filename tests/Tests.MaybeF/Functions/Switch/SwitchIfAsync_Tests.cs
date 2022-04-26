// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class SwitchIfAsync_Tests : Abstracts.SwitchIfAsync_Tests
{
	[Fact]
	public override async Task Test00_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test00((mbe, check) => F.SwitchIfAsync(mbe, check, null, null));
		await Test00((mbe, check) => F.SwitchIfAsync(mbe, check, ifFalse));
	}

	[Theory]
	[InlineData(null)]
	public override async Task Test01_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input)
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test01(check => F.SwitchIfAsync(Task.FromResult(input), check, null, null));
		await Test01(check => F.SwitchIfAsync(Task.FromResult(input), check, ifFalse));
	}

	[Fact]
	public override async Task Test02_Predicate_Null_Returns_None_With_SwitchIfPredicateCannotBeNullMsg()
	{
		await Test02((mbe, check) => F.SwitchIfAsync(mbe, check, null, null));
		await Test02((mbe, check) => F.SwitchIfAsync(mbe, check, null, null));
	}

	[Fact]
	public override async Task Test03_None_Returns_Original_None()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test03((mbe, check) => F.SwitchIfAsync(mbe, check, null, null));
		await Test03((mbe, check) => F.SwitchIfAsync(mbe, check, ifFalse));
	}

	[Fact]
	public override async Task Test04_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		var ifFalse = Substitute.For<Func<int, IMsg>>();
		await Test04((mbe, check) => F.SwitchIfAsync(mbe, check, null, null));
		await Test04((mbe, check) => F.SwitchIfAsync(mbe, check, ifFalse));
	}

	[Fact]
	public override async Task Test05_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Maybe()
	{
		await Test05((mbe, check) => F.SwitchIfAsync(mbe, check, null, null));
	}

	[Fact]
	public override async Task Test06_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Maybe()
	{
		await Test06((mbe, check) => F.SwitchIfAsync(mbe, check, null, null));
	}

	[Fact]
	public override async Task Test07_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		await Test07((mbe, check, ifTrue) => F.SwitchIfAsync(mbe, check, ifTrue, null));
	}

	[Fact]
	public override async Task Test08_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		await Test08((mbe, check, ifFalse) => F.SwitchIfAsync(mbe, check, null, ifFalse));
		await Test08((mbe, check, ifFalse) => F.SwitchIfAsync(mbe, check, x => ifFalse(x).Reason));
	}

	[Fact]
	public override async Task Test09_Check_Returns_True_Runs_IfTrue_Returns_Value()
	{
		await Test09((mbe, check, ifTrue) => F.SwitchIfAsync(mbe, check, ifTrue, null));
	}

	[Fact]
	public override async Task Test10_Check_Returns_False_Runs_IfFalse_Returns_Value()
	{
		await Test10((mbe, check, ifFalse) => F.SwitchIfAsync(mbe, check, null, ifFalse));
		await Test10((mbe, check, ifFalse) => F.SwitchIfAsync(mbe, check, x => ifFalse(x).Reason));
	}
}
