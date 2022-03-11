﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class IfNullAsync_Tests : Abstracts.IfNullAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionReason()
	{
		await Test00((mbe, ifNull) => F.IfNullAsync(mbe, ifNull)).ConfigureAwait(false);
		await Test00((mbe, ifNull) => F.IfNullAsync(mbe.AsTask, ifNull)).ConfigureAwait(false);
		await Test00((mbe, ifNull) => F.IfNullAsync(mbe.AsTask, () => { _ = ifNull(); return new TestReason(); })).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_Some_With_Null_Value_Runs_IfNull_Func()
	{
		await Test01((mbe, ifNull) => F.IfNullAsync(mbe, ifNull)).ConfigureAwait(false);
		await Test01((mbe, ifNull) => F.IfNullAsync(mbe.AsTask, ifNull)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_None_With_NullValueReason_Runs_IfNull_Func()
	{
		await Test02((mbe, ifNull) => F.IfNullAsync(mbe, ifNull)).ConfigureAwait(false);
		await Test02((mbe, ifNull) => F.IfNullAsync(mbe.AsTask, ifNull)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason()
	{
		await Test03((mbe, ifNull) => F.IfNullAsync(mbe.AsTask, ifNull)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_None_With_NullValueReason_Runs_IfNull_Func_Returns_None_With_Reason()
	{
		await Test04((mbe, ifNull) => F.IfNullAsync(mbe.AsTask, ifNull)).ConfigureAwait(false);
	}
}