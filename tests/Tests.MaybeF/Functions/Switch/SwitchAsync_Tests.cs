// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class SwitchAsync_Tests : Abstracts.SwitchAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var some = Substitute.For<Func<int, Task<string>>>();
		var none = Substitute.For<Func<IMsg, Task<string>>>();
		await Test00(mbe => F.SwitchAsync(mbe, some, none));
		await Test00(mbe => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Theory]
	[InlineData(null)]
	public override async Task Test01_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input)
	{
		var some = Substitute.For<Func<int, Task<string>>>();
		var none = Substitute.For<Func<IMsg, Task<string>>>();
		await Test01(() => F.SwitchAsync(input, some, none));
		await Test01(() => F.SwitchAsync(Task.FromResult(input), some, none));
	}

	[Fact]
	public override async Task Test02_If_None_Runs_None_Func_With_Msg()
	{
		var some = Substitute.For<Func<int, Task<string>>>();
		await Test02((mbe, none) => F.SwitchAsync(mbe, some, none));
		await Test02((mbe, none) => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Fact]
	public override async Task Test03_If_Some_Runs_Some_Func_With_Value()
	{
		var none = Substitute.For<Func<IMsg, Task<string>>>();
		await Test03((mbe, some) => F.SwitchAsync(mbe, some, none));
		await Test03((mbe, some) => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Fact]
	public override async Task Test04_If_None_And_None_Func_Is_Null_Throws_ArgumentNullException()
	{
		var some = Substitute.For<Func<int, Task<string>>>();
		await Test04((mbe, none) => F.SwitchAsync(mbe, some, none));
		await Test04((mbe, none) => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Fact]
	public override async Task Test05_If_Some_And_Some_Func_Is_Null_Throws_ArgumentNullException()
	{
		var none = Substitute.For<Func<IMsg, Task<string>>>();
		await Test05((mbe, some) => F.SwitchAsync(mbe, some, none));
		await Test05((mbe, some) => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Fact]
	public override async Task Test06_If_Some_Runs_Some_Func_With_Value()
	{
		var none = Substitute.For<Func<Task<Maybe<string>>>>();
		await Test06((mbe, some) => F.SwitchAsync(mbe, some, none));
		await Test06((mbe, some) => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Fact]
	public override async Task Test07_If_None_Runs_None_Func()
	{
		var some = Substitute.For<Func<int, Task<Maybe<string>>>>();
		await Test07((mbe, none) => F.SwitchAsync(mbe, some, none));
		await Test07((mbe, none) => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Fact]
	public override async Task Test08_If_Some_And_Some_Func_Is_Null_Returns_None_With_SomeFunctionCannotBeNullMsg()
	{
		var none = Substitute.For<Func<Task<Maybe<string>>>>();
		await Test08((mbe, some) => F.SwitchAsync(mbe, some, none));
		await Test08((mbe, some) => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Fact]
	public override async Task Test09_If_None_And_None_Func_Is_Null_Returns_None_With_NoneFunctionCannotBeNullMsg()
	{
		var some = Substitute.For<Func<int, Task<Maybe<string>>>>();
		await Test09((mbe, none) => F.SwitchAsync(mbe, some, none));
		await Test09((mbe, none) => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Fact]
	public override async Task Test10_If_Unknown_Maybe_Returns_UnknownMaybeTypeMsg()
	{
		var some = Substitute.For<Func<int, Task<Maybe<string>>>>();
		var none = Substitute.For<Func<Task<Maybe<string>>>>();
		await Test10(mbe => F.SwitchAsync(mbe, some, none));
		await Test10(mbe => F.SwitchAsync(mbe.AsTask(), some, none));
	}

	[Theory]
	[InlineData(null)]
	public override async Task Test11_If_Null_Returns_None_With_MaybeCannotBeNullMsg(Maybe<int> input)
	{
		var some = Substitute.For<Func<int, Task<Maybe<string>>>>();
		var none = Substitute.For<Func<Task<Maybe<string>>>>();
		await Test11(() => F.SwitchAsync(input, some, none));
		await Test11(() => F.SwitchAsync(Task.FromResult(input), some, none));
	}
}
