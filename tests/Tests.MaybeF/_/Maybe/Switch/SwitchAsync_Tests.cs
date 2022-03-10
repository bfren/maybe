// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class SwitchAsync_Tests : Abstracts.SwitchAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Rnd.Str)).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), Task.FromResult(Rnd.Str))).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Task.FromResult(Rnd.Str))).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<IReason, string>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<IReason, Task<string>>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<IReason, Task<string>>>())).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_If_None_Runs_None_Func_With_Reason()
	{
		await Test01((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestReason()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), none(new TestReason()))).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestReason()))).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestReason()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), () => none(new TestReason()))).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestReason()))).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), x => none(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, string>>(), none)).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_If_Some_Runs_Some_Func_With_Value()
	{
		await Test02((mbe, some) => mbe.SwitchAsync(some, Rnd.Str)).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Task.FromResult(Rnd.Str))).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.SwitchAsync(some, Task.FromResult(Rnd.Str))).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<IReason, string>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<IReason, Task<string>>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.SwitchAsync(some, Substitute.For<Func<IReason, Task<string>>>())).ConfigureAwait(false);
	}
}
