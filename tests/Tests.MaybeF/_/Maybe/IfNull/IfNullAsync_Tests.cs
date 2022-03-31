// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class IfNullAsync_Tests : Abstracts.IfNullAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((mbe, ifNull) => mbe.IfNullAsync(ifNull)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_Some_With_Null_Value_Runs_IfNull_Func()
	{
		await Test01((mbe, ifNull) => mbe.IfNullAsync(ifNull)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_None_With_NullValueMsg_Runs_IfNull_Func()
	{
		await Test02((mbe, ifNull) => mbe.IfNullAsync(ifNull)).ConfigureAwait(false);
	}

	#region Unused

	public override Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Msg() =>
		Task.CompletedTask;

	public override Task Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Msg() =>
		Task.CompletedTask;

	public override Task Test05_Null_Maybe_Runs_IfNull_Func(Maybe<int> input) =>
		Task.CompletedTask;

	#endregion Unused
}
