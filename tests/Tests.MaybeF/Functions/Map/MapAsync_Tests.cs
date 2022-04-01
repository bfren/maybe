// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class MapAsync_Tests : Abstracts.MapAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((mbe, map, handler) => F.MapAsync(mbe, map, handler));
		await Test00((mbe, map, handler) => F.MapAsync(mbe.AsTask, map, handler));
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test01((mbe, map, handler) => F.MapAsync(mbe, map, handler));
		await Test01((mbe, map, handler) => F.MapAsync(mbe.AsTask, map, handler));
	}

	[Fact]
	public override async Task Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None()
	{
		await Test02((mbe, map, handler) => F.MapAsync(mbe, map, handler));
		await Test02((mbe, map, handler) => F.MapAsync(mbe.AsTask, map, handler));
	}

	[Fact]
	public override async Task Test03_If_None_Returns_None()
	{
		await Test03((mbe, map, handler) => F.MapAsync(mbe, map, handler));
		await Test03((mbe, map, handler) => F.MapAsync(mbe.AsTask, map, handler));
	}

	[Fact]
	public override async Task Test04_If_None_With_Msg_Returns_None_With_Same_Msg()
	{
		await Test04((mbe, map, handler) => F.MapAsync(mbe, map, handler));
		await Test04((mbe, map, handler) => F.MapAsync(mbe.AsTask, map, handler));
	}

	[Fact]
	public override async Task Test05_If_Some_Runs_Map_Function()
	{
		await Test05((mbe, map, handler) => F.MapAsync(mbe, map, handler));
		await Test05((mbe, map, handler) => F.MapAsync(mbe.AsTask, map, handler));
	}
}
