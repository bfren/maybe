﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class Map_Tests : Abstracts.Map_Tests
{
	[Fact]
	public override void Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason()
	{
		Test00((mbe, map, handler) => F.Map(mbe, map, handler));
	}

	[Fact]
	public override void Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionReason()
	{
		Test01((mbe, map, handler) => F.Map(mbe, map, handler));
	}

	[Fact]
	public override void Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None()
	{
		Test02((mbe, map, handler) => F.Map(mbe, map, handler));
	}

	[Fact]
	public override void Test03_If_None_Returns_None()
	{
		Test03((mbe, map, handler) => F.Map(mbe, map, handler));
	}

	[Fact]
	public override void Test04_If_None_With_Reason_Returns_None_With_Same_Reason()
	{
		Test04((mbe, map, handler) => F.Map(mbe, map, handler));
	}

	[Fact]
	public override void Test05_If_Some_Runs_Map_Function()
	{
		Test05((mbe, map, handler) => F.Map(mbe, map, handler));
	}
}