// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class SwitchAsync_Tests : Abstracts.SwitchAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Rnd.Str));
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), Task.FromResult(Rnd.Str)));
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Task.FromResult(Rnd.Str)));
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<string>>()));
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<Task<string>>>()));
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<Task<string>>>()));
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<IMsg, string>>()));
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<IMsg, Task<string>>>()));
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<IMsg, Task<string>>>()));
	}

	[Fact]
	public override async Task Test02_If_None_Runs_None_Func_With_Msg()
	{
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestMsg()).GetAwaiter().GetResult()));
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), none(new TestMsg())));
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestMsg())));
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestMsg()).GetAwaiter().GetResult()));
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), () => none(new TestMsg())));
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestMsg())));
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), x => none(x).GetAwaiter().GetResult()));
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), none));
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none));
	}

	[Fact]
	public override async Task Test03_If_Some_Runs_Some_Func_With_Value()
	{
		await Test03((mbe, some) => mbe.SwitchAsync(some, Rnd.Str));
		await Test03((mbe, some) => mbe.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Task.FromResult(Rnd.Str)));
		await Test03((mbe, some) => mbe.SwitchAsync(some, Task.FromResult(Rnd.Str)));
		await Test03((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<string>>()));
		await Test03((mbe, some) => mbe.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<Task<string>>>()));
		await Test03((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<Task<string>>>()));
		await Test03((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<IMsg, string>>()));
		await Test03((mbe, some) => mbe.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<IMsg, Task<string>>>()));
		await Test03((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<IMsg, Task<string>>>()));
	}

	[Fact]
	public override async Task Test04_If_None_And_None_Func_Is_Null_Throws_ArgumentNullException()
	{
		await Test04((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), none));
		await Test04((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none));
	}

	[Fact]
	public override async Task Test05_If_Some_And_Some_Func_Is_Null_Throws_ArgumentNullException()
	{
		await Test05((mbe, some) => mbe.SwitchAsync(some, Rnd.Str));
		await Test05((mbe, some) => mbe.SwitchAsync(some, Task.FromResult(Rnd.Str)));
		await Test05((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<string>>()));
		await Test05((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<Task<string>>>()));
		await Test05((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<IMsg, string>>()));
		await Test05((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<IMsg, Task<string>>>()));
	}

	[Fact]
	public override async Task Test06_If_Some_Runs_Some_Func_With_Value()
	{
		await Test06((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<Task<Maybe<string>>>>()));
	}

	[Fact]
	public override async Task Test07_If_None_Runs_None_Func()
	{
		await Test07((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<Maybe<string>>>>(), none));
	}

	[Fact]
	public override async Task Test08_If_Some_And_Some_Func_Is_Null_Returns_None_With_SomeFunctionCannotBeNullMsg()
	{
		await Test08((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<Task<Maybe<string>>>>()));
	}

	[Fact]
	public override async Task Test09_If_None_And_None_Func_Is_Null_Returns_None_With_NoneFunctionCannotBeNullMsg()
	{
		await Test09((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<Maybe<string>>>>(), none));
	}

	[Fact]
	public override async Task Test10_If_Unknown_Maybe_Returns_UnknownMaybeTypeMsg()
	{
		var some = Substitute.For<Func<int, Task<Maybe<string>>>>();
		var none = Substitute.For<Func<Task<Maybe<string>>>>();
		await Test10(mbe => mbe.SwitchAsync(some, none));
	}

	#region Unused

	[Theory]
	[InlineData(null)]
	public override Task Test01_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input) =>
		Task.FromResult(input);

	[Theory]
	[InlineData(null)]
	public override Task Test11_If_Null_Returns_None_With_MaybeCannotBeNullMsg(Maybe<int> input) =>
		Task.FromResult(input);

	#endregion Unused
}
