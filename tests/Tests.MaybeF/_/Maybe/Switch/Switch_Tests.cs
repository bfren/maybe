// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class Switch_Tests : Abstracts.Switch_Tests
{
	[Fact]
	public override void Test00_Return_Void_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var some = Substitute.For<Action<int>>();
		var none = Substitute.For<Action<IReason>>();
		Test00(mbe => mbe.Switch(some, () => none(new TestReason())));
		Test00(mbe => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test01_Return_Value_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var some = Substitute.For<Func<int, string>>();
		var none = Substitute.For<Func<IReason, string>>();
		Test01(mbe => mbe.Switch(some, none(new TestReason())));
		Test01(mbe => mbe.Switch(some, () => none(new TestReason())));
		Test01(mbe => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test02_Return_Void_If_None_Runs_None_Action_With_Reason()
	{
		var some = Substitute.For<Action<int>>();
		Test02((mbe, none) => mbe.Switch(some, () => none(new TestReason())));
		Test02((mbe, none) => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test03_Return_Value_If_None_Runs_None_Func_With_Reason()
	{
		var some = Substitute.For<Func<int, string>>();
		Test03((mbe, none) => mbe.Switch(some, none(new TestReason())));
		Test03((mbe, none) => mbe.Switch(some, () => none(new TestReason())));
		Test03((mbe, none) => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test04_Return_Void_If_Some_Runs_Some_Action_With_Value()
	{
		Test04((mbe, some) => mbe.Switch(some, Substitute.For<Action>()));
		Test04((mbe, some) => mbe.Switch(some, Substitute.For<Action<IReason>>()));
	}

	[Fact]
	public override void Test05_Return_Value_If_Some_Runs_Some_Func_With_Value()
	{
		Test05((mbe, some) => mbe.Switch(some, Rnd.Str));
		Test05((mbe, some) => mbe.Switch(some, Substitute.For<Func<string>>()));
		Test05((mbe, some) => mbe.Switch(some, Substitute.For<Func<IReason, string>>()));
	}
}
