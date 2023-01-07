// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class UnwrapSingleAsync_Tests : Abstracts.UnwrapSingleAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnknownMaybeTypeMsg()
	{
		await Test00(mbe => mbe.UnwrapAsync(x => x.SingleValue<int>(null, null, null, null)));
	}

	[Fact]
	public override async Task Test01_None_Returns_None()
	{
		await Test01(mbe => mbe.UnwrapAsync(x => x.SingleValue<int>(null, null, null, null)));
	}

	[Fact]
	public override async Task Test02_None_With_Msg_Returns_None_With_Msg()
	{
		await Test02(mbe => mbe.UnwrapAsync(x => x.SingleValue<int>(null, null, null, null)));
	}

	[Fact]
	public override async Task Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsMsg()
	{
		await Test03(mbe => mbe.UnwrapAsync(x => x.SingleValue<int>(null, null, null, null)));
	}

	[Fact]
	public override async Task Test04_No_Items_Runs_NoItems()
	{
		await Test04((mbe, noItems) => mbe.UnwrapAsync(x => x.SingleValue<int>(noItems, null, null, null)));
	}

	[Fact]
	public override async Task Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg()
	{
		await Test05(mbe => mbe.UnwrapAsync(x => x.SingleValue<int>(null, null, null, null)));
	}

	[Fact]
	public override async Task Test06_Too_Many_Items_Runs_TooMany()
	{
		await Test06((mbe, tooMany) => mbe.UnwrapAsync(x => x.SingleValue<int>(null, tooMany, null, null)));
	}

	[Fact]
	public override async Task Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg()
	{
		await Test07(mbe => mbe.UnwrapAsync(x => x.SingleValue<int>(null, null, null, null)));
	}

	[Fact]
	public override async Task Test08_Not_A_List_Runs_NotAList()
	{
		await Test08((mbe, notAList) => mbe.UnwrapAsync(x => x.SingleValue<int>(null, null, null, notAList)));
	}

	[Fact]
	public override async Task Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorMsg()
	{
		await Test09(mbe => mbe.UnwrapAsync(x => x.SingleValue<string>(null, null, null, null)));
	}

	[Fact]
	public override async Task Test10_Incorrect_Type_Runs_IncorrectType()
	{
		await Test10((mbe, incorrectType) => mbe.UnwrapAsync(x => x.SingleValue<string>(null, null, incorrectType, null)));
	}

	[Fact]
	public override async Task Test11_List_With_Single_Item_Returns_Single()
	{
		await Test11(mbe => mbe.UnwrapAsync(x => x.SingleValue<int>(null, null, null, null)));
	}
}
