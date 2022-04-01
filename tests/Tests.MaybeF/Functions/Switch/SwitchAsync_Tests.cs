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
		await Test00(mbe => F.SwitchAsync(mbe.AsTask, some, none));
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
		await Test02((mbe, none) => F.SwitchAsync(mbe.AsTask, some, none));
	}

	[Fact]
	public override async Task Test03_If_Some_Runs_Some_Func_With_Value()
	{
		var none = Substitute.For<Func<IMsg, Task<string>>>();
		await Test03((mbe, some) => F.SwitchAsync(mbe, some, none));
		await Test03((mbe, some) => F.SwitchAsync(mbe.AsTask, some, none));
	}
}
