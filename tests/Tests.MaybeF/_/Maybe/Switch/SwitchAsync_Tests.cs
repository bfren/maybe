﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class SwitchAsync_Tests : Abstracts.SwitchAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Rnd.Str)).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), Task.FromResult(Rnd.Str))).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Task.FromResult(Rnd.Str))).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<IMsg, string>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<IMsg, Task<string>>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<IMsg, Task<string>>>())).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_If_None_Runs_None_Func_With_Msg()
	{
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), none(new TestMsg()))).ConfigureAwait(false);
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestMsg()))).ConfigureAwait(false);
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), () => none(new TestMsg()))).ConfigureAwait(false);
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestMsg()))).ConfigureAwait(false);
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), x => none(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), none)).ConfigureAwait(false);
		await Test02((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_If_Some_Runs_Some_Func_With_Value()
	{
		await Test03((mbe, some) => mbe.SwitchAsync(some, Rnd.Str)).ConfigureAwait(false);
		await Test03((mbe, some) => mbe.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Task.FromResult(Rnd.Str))).ConfigureAwait(false);
		await Test03((mbe, some) => mbe.SwitchAsync(some, Task.FromResult(Rnd.Str))).ConfigureAwait(false);
		await Test03((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test03((mbe, some) => mbe.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test03((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test03((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<IMsg, string>>())).ConfigureAwait(false);
		await Test03((mbe, some) => mbe.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<IMsg, Task<string>>>())).ConfigureAwait(false);
		await Test03((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<IMsg, Task<string>>>())).ConfigureAwait(false);
	}

	#region Unused

	public override Task Test01_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input) =>
		Task.CompletedTask;

	#endregion Unused
}
