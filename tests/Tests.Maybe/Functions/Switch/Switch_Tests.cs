// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using NSubstitute;
using Xunit;

namespace Maybe.Functions.MaybeF_Tests;

public class Switch_Tests : Tests.Maybe.Abstracts.Switch_Tests
{
	[Fact]
	public override void Test00_Return_Void_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var some = Substitute.For<Action<int>>();
		var none = Substitute.For<Action<IReason>>();
		Test00(mbe => MaybeF.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test01_Return_Value_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var some = Substitute.For<Func<int, string>>();
		var none = Substitute.For<Func<IReason, string>>();
		Test01(mbe => MaybeF.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test02_Return_Void_If_None_Runs_None_Action_With_Reason()
	{
		var some = Substitute.For<Action<int>>();
		Test02((mbe, none) => MaybeF.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test03_Return_Value_If_None_Runs_None_Func_With_Reason()
	{
		var some = Substitute.For<Func<int, string>>();
		Test03((mbe, none) => MaybeF.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test04_Return_Void_If_Some_Runs_Some_Action_With_Value()
	{
		var none = Substitute.For<Action<IReason>>();
		Test04((mbe, some) => MaybeF.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test05_Return_Value_If_Some_Runs_Some_Func_With_Value()
	{
		var none = Substitute.For<Func<IReason, string>>();
		Test05((mbe, some) => MaybeF.Switch(mbe, some, none));
	}
}
