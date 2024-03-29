﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class Unwrap_Tests : Abstracts.Unwrap_Tests
{
	[Fact]
	public override void Test00_None_Runs_IfNone_Func_Returns_Value()
	{
		Test00((mbe, ifNone) => mbe.Unwrap(ifNone));
	}

	[Fact]
	public override void Test01_None_With_Msg_Runs_IfNone_Func_Passes_Msg_Returns_Value()
	{
		Test01((mbe, ifNone) => mbe.Unwrap(ifNone));
	}

	[Fact]
	public override void Test02_Some_Returns_Value()
	{
		Test02(mbe => mbe.Unwrap(Substitute.For<Func<int>>()));
		Test02(mbe => mbe.Unwrap(Substitute.For<Func<IMsg, int>>()));
	}
}
