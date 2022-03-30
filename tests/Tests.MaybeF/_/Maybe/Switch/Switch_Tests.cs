// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class Switch_Tests : Abstracts.Switch_Tests
{
	[Fact]
	public override void Test00_Return_Void_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var some = Substitute.For<Action<int>>();
		var none = Substitute.For<Action<IMsg>>();
		Test00(mbe => mbe.Switch(some, () => none(new TestMsg())));
		Test00(mbe => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test01_Return_Value_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var some = Substitute.For<Func<int, string>>();
		var none = Substitute.For<Func<IMsg, string>>();
		Test01(mbe => mbe.Switch(some, none(new TestMsg())));
		Test01(mbe => mbe.Switch(some, () => none(new TestMsg())));
		Test01(mbe => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test03_Return_Void_If_None_Runs_None_Action_With_Msg()
	{
		var some = Substitute.For<Action<int>>();
		Test03((mbe, none) => mbe.Switch(some, () => none(new TestMsg())));
		Test03((mbe, none) => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test04_Return_Value_If_None_Runs_None_Func_With_Msg()
	{
		var some = Substitute.For<Func<int, string>>();
		Test04((mbe, none) => mbe.Switch(some, none(new TestMsg())));
		Test04((mbe, none) => mbe.Switch(some, () => none(new TestMsg())));
		Test04((mbe, none) => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test05_Return_Void_If_Some_Runs_Some_Action_With_Value()
	{
		Test05((mbe, some) => mbe.Switch(some, Substitute.For<Action>()));
		Test05((mbe, some) => mbe.Switch(some, Substitute.For<Action<IMsg>>()));
	}

	[Fact]
	public override void Test06_Return_Value_If_Some_Runs_Some_Func_With_Value()
	{
		Test06((mbe, some) => mbe.Switch(some, Rnd.Str));
		Test06((mbe, some) => mbe.Switch(some, Substitute.For<Func<string>>()));
		Test06((mbe, some) => mbe.Switch(some, Substitute.For<Func<IMsg, string>>()));
	}

	#region Unused

	[Theory]
	[InlineData(null)]
	public override void Test02_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input) =>
		Assert.Null(input);

	#endregion Unused
}
