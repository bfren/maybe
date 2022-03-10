// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Threading.Tasks;
using Xunit;

namespace MaybeF.MaybeF_Tests;

public class UnwrapSingleAsync_Tests : Abstracts.UnwrapSingleAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason()
	{
		await Test00(mbe => F.UnwrapAsync(mbe, x => x.SingleValue<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_None_Returns_None()
	{
		await Test01(mbe => F.UnwrapAsync(mbe, x => x.SingleValue<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_None_With_Reason_Returns_None_With_Reason()
	{
		await Test02(mbe => F.UnwrapAsync(mbe, x => x.SingleValue<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsReason()
	{
		await Test03(mbe => F.UnwrapAsync(mbe, x => x.SingleValue<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_No_Items_Runs_NoItems()
	{
		await Test04((mbe, noItems) => F.UnwrapAsync(mbe, x => x.SingleValue<int>(noItems, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorReason()
	{
		await Test05(mbe => F.UnwrapAsync(mbe, x => x.SingleValue<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test06_Too_Many_Items_Runs_TooMany()
	{
		await Test06((mbe, tooMany) => F.UnwrapAsync(mbe, x => x.SingleValue<int>(null, tooMany, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListReason()
	{
		await Test07(mbe => F.UnwrapAsync(mbe, x => x.SingleValue<int>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test08_Not_A_List_Runs_NotAList()
	{
		await Test08((mbe, notAList) => F.UnwrapAsync(mbe, x => x.SingleValue<int>(null, null, notAList))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorReason()
	{
		await Test09(mbe => F.UnwrapAsync(mbe, x => x.SingleValue<string>(null, null, null))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test10_List_With_Single_Item_Returns_Single()
	{
		await Test10(mbe => F.UnwrapAsync(mbe, x => x.SingleValue<int>(null, null, null))).ConfigureAwait(false);
	}
}
