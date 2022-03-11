// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class UnwrapSingle_Tests : Abstracts.UnwrapSingle_Tests
{
	[Fact]
	public override void Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason()
	{
		Test00(mbe => F.UnwrapSingle<int, int>(mbe, null, null, null));
	}

	[Fact]
	public override void Test01_None_Returns_None()
	{
		Test01(mbe => F.UnwrapSingle<int, int>(mbe, null, null, null));
	}

	[Fact]
	public override void Test02_None_With_Reason_Returns_None_With_Reason()
	{
		Test02(mbe => F.UnwrapSingle<int, int>(mbe, null, null, null));
	}

	[Fact]
	public override void Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsReason()
	{
		Test03(mbe => F.UnwrapSingle<int[], int>(mbe, null, null, null));
	}

	[Fact]
	public override void Test04_No_Items_Runs_NoItems()
	{
		Test04((mbe, noItems) => F.UnwrapSingle<int[], int>(mbe, noItems, null, null));
	}

	[Fact]
	public override void Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorReason()
	{
		Test05(mbe => F.UnwrapSingle<int[], int>(mbe, null, null, null));
	}

	[Fact]
	public override void Test06_Too_Many_Items_Runs_TooMany()
	{
		Test06((mbe, tooMany) => F.UnwrapSingle<int[], int>(mbe, null, tooMany, null));
	}

	[Fact]
	public override void Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListReason()
	{
		Test07(mbe => F.UnwrapSingle<int, int>(mbe, null, null, null));
	}

	[Fact]
	public override void Test08_Not_A_List_Runs_NotAList()
	{
		Test08((mbe, notAList) => F.UnwrapSingle<int, int>(mbe, null, null, notAList));
	}

	[Fact]
	public override void Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorReason()
	{
		Test09(mbe => F.UnwrapSingle<int[], string>(mbe, null, null, null));
	}

	[Fact]
	public override void Test10_List_With_Single_Item_Returns_Single()
	{
		Test10(mbe => F.UnwrapSingle<int[], int>(mbe, null, null, null));
	}
}
