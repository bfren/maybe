// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class UnwrapSingle_Tests : Abstracts.UnwrapSingle_Tests
{
	[Fact]
	public override void Test00_If_Unknown_Maybe_Returns_None_With_UnhknownMaybeTypeMsg()
	{
		Test00(mbe => F.UnwrapSingle<int, int>(mbe, null, null, null, null));
	}

	[Fact]
	public override void Test01_None_Returns_None()
	{
		Test01(mbe => F.UnwrapSingle<int, int>(mbe, null, null, null, null));
	}

	[Fact]
	public override void Test02_None_With_Msg_Returns_None_With_Msg()
	{
		Test02(mbe => F.UnwrapSingle<int, int>(mbe, null, null, null, null));
	}

	[Fact]
	public override void Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsMsg()
	{
		Test03(mbe => F.UnwrapSingle<int[], int>(mbe, null, null, null, null));
	}

	[Fact]
	public override void Test04_No_Items_Runs_NoItems()
	{
		Test04((mbe, noItems) => F.UnwrapSingle<int[], int>(mbe, noItems, null, null, null));
	}

	[Fact]
	public override void Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg()
	{
		Test05(mbe => F.UnwrapSingle<int[], int>(mbe, null, null, null, null));
	}

	[Fact]
	public override void Test06_Too_Many_Items_Runs_TooMany()
	{
		Test06((mbe, tooMany) => F.UnwrapSingle<int[], int>(mbe, null, tooMany, null, null));
	}

	[Fact]
	public override void Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg()
	{
		Test07(mbe => F.UnwrapSingle<int, int>(mbe, null, null, null, null));
	}

	[Fact]
	public override void Test08_Not_A_List_Runs_NotAList()
	{
		Test08((mbe, notAList) => F.UnwrapSingle<int, int>(mbe, null, null, null, notAList));
	}

	[Fact]
	public override void Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorMsg()
	{
		Test09(mbe => F.UnwrapSingle<int[], string>(mbe, null, null, null, null));
	}

	[Fact]
	public override void Test10_Incorrect_Type_Runs_IncorrectType()
	{
		Test10((mbe, incorrectType) => F.UnwrapSingle<int[], string>(mbe, null, null, incorrectType, null));
	}

	[Fact]
	public override void Test11_List_With_Single_Item_Returns_Single()
	{
		Test11(mbe => F.UnwrapSingle<int[], int>(mbe, null, null, null, null));
	}
}
