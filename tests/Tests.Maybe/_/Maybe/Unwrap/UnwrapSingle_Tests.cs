﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Xunit;

namespace Maybe.Maybe_Tests;

public class UnwrapSingle_Tests : Tests.Maybe.Abstracts.UnwrapSingle_Tests
{
	[Fact]
	public override void Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason()
	{
		Test00(mbe => mbe.UnwrapSingle<int>(null, null, null));
	}

	[Fact]
	public override void Test01_None_Returns_None()
	{
		Test01(mbe => mbe.UnwrapSingle<int>(null, null, null));
	}

	[Fact]
	public override void Test02_None_With_Reason_Returns_None_With_Reason()
	{
		Test02(mbe => mbe.UnwrapSingle<int>(null, null, null));
	}

	[Fact]
	public override void Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsReason()
	{
		Test03(mbe => mbe.UnwrapSingle<int>(null, null, null));
	}

	[Fact]
	public override void Test04_No_Items_Runs_NoItems()
	{
		Test04((mbe, noItems) => mbe.UnwrapSingle<int>(noItems, null, null));
	}

	[Fact]
	public override void Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorReason()
	{
		Test05(mbe => mbe.UnwrapSingle<int>(null, null, null));
	}

	[Fact]
	public override void Test06_Too_Many_Items_Runs_TooMany()
	{
		Test06((mbe, tooMany) => mbe.UnwrapSingle<int>(null, tooMany, null));
	}

	[Fact]
	public override void Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListReason()
	{
		Test07(mbe => mbe.UnwrapSingle<int>(null, null, null));
	}

	[Fact]
	public override void Test08_Not_A_List_Runs_NotAList()
	{
		Test08((mbe, notAList) => mbe.UnwrapSingle<int>(null, null, notAList));
	}

	[Fact]
	public override void Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorReason()
	{
		Test09(mbe => mbe.UnwrapSingle<string>(null, null, null));
	}

	[Fact]
	public override void Test10_List_With_Single_Item_Returns_Single()
	{
		Test10(mbe => mbe.UnwrapSingle<int>(null, null, null));
	}
}
