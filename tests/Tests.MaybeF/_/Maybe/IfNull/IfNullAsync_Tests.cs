// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class IfNullAsync_Tests : Abstracts.IfNullAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((mbe, ifNull) => mbe.IfNullAsync(ifNull));
	}

	[Fact]
	public override async Task Test01_Some_With_Null_Value_Runs_IfNull_Func()
	{
		await Test01((mbe, ifNull) => mbe.IfNullAsync(ifNull));
	}

	[Fact]
	public override async Task Test02_None_With_NullValueMsg_Runs_IfNull_Func()
	{
		await Test02((mbe, ifNull) => mbe.IfNullAsync(ifNull));
	}

	[Fact]
	public override async Task Test06_Some_With_Null__Runs_IfNull()
	{
		await Test06((mbe, ifNull, ifSome) => mbe.IfNullAsync(ifNull, ifSome, F.DefaultHandler));
		await Test06((mbe, ifNull, ifSome) => mbe.IfNullAsync(async () => F.Some(await ifNull()), async x => F.Some(await ifSome(x))));
	}

	[Fact]
	public override async Task Test07_Some_With_Value__Runs_IfSome()
	{
		await Test07((mbe, ifNull, ifSome) => mbe.IfNullAsync(ifNull, ifSome, F.DefaultHandler));
		await Test07((mbe, ifNull, ifSome) => mbe.IfNullAsync(async () => F.Some(await ifNull()), async x => F.Some(await ifSome(x))));
	}

	[Fact]
	public override async Task Test08_None_With_NullValueMsg__Runs_IfNull()
	{
		await Test08((mbe, ifNull, ifSome) => mbe.IfNullAsync(ifNull, ifSome, F.DefaultHandler));
		await Test08((mbe, ifNull, ifSome) => mbe.IfNullAsync(async () => F.Some(await ifNull()), async x => F.Some(await ifSome(x))));
	}

	[Fact]
	public override async Task Test09_None_With_Msg__Returns_None()
	{
		await Test09((mbe, ifNull, ifSome) => mbe.IfNullAsync(ifNull, ifSome, F.DefaultHandler));
		await Test09((mbe, ifNull, ifSome) => mbe.IfNullAsync(async () => F.Some(await ifNull()), async x => F.Some(await ifSome(x))));
	}

	[Fact]
	public override async Task Test10_Exception_In_IfNull__Uses_Handler()
	{
		await Test10((mbe, ifNull, ifSome, handler) => mbe.IfNullAsync(ifNull, ifSome, handler));
	}

	[Fact]
	public override async Task Test11_Exception_In_IfSome__Uses_Handler()
	{
		await Test11((mbe, ifNull, ifSome, handler) => mbe.IfNullAsync(ifNull, ifSome, handler));
	}

	[Fact]
	public override async Task Test12_Exception_In_IfNull__Uses_DefaultHandler()
	{
		await Test12((mbe, ifNull, ifSome) => mbe.IfNullAsync(ifNull, ifSome));
	}

	[Fact]
	public override async Task Test13_Exception_In_IfSome__Uses_DefaultHandler()
	{
		await Test13((mbe, ifNull, ifSome) => mbe.IfNullAsync(ifNull, ifSome));
	}

	#region Unused

	[Fact]
	public override Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Msg() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Msg() =>
		Task.CompletedTask;

	[Theory]
	[InlineData(null)]
	public override Task Test05_Null_Maybe_Runs_IfNull_Func(Maybe<int> input) =>
		Task.FromResult(input);

	#endregion Unused
}
