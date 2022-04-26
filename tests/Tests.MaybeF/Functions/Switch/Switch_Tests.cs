// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class Switch_Tests : Abstracts.Switch_Tests
{
	[Fact]
	public override void Test00_Return_Void_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var some = Substitute.For<Action<int>>();
		var none = Substitute.For<Action<IMsg>>();
		Test00(mbe => F.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test01_Return_Value_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var some = Substitute.For<Func<int, string>>();
		var none = Substitute.For<Func<IMsg, string>>();
		Test01(mbe => F.Switch(mbe, some, none));
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_If_Null_Throws_MaybeCannotBeNullException(Maybe<int> input)
	{
		var some = Substitute.For<Func<int, string>>();
		var none = Substitute.For<Func<IMsg, string>>();
		Test02(() => F.Switch(input, some, none));
	}

	[Fact]
	public override void Test03_Return_Void_If_None_And_None_Func_Is_Null_Throws_ArgumentNullException()
	{
		var some = Substitute.For<Action<int>>();
		Test03((mbe, _, n1) => F.Switch(mbe, some, n1));
	}

	[Fact]
	public override void Test04_Return_Void_If_None_Runs_None_Action_With_Msg()
	{
		var some = Substitute.For<Action<int>>();
		Test04((mbe, none) => F.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test05_Return_Value_If_None_And_None_Func_Is_Null_Throws_ArgumentNullException()
	{
		var some = Substitute.For<Func<int, string>>();
		Test05((mbe, _, _, n2) => F.Switch(mbe, some, n2));
	}

	[Fact]
	public override void Test06_Return_Value_If_None_Runs_None_Func_With_Msg()
	{
		var some = Substitute.For<Func<int, string>>();
		Test06((mbe, none) => F.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test07_Return_Void_If_Some_And_Some_Func_Is_Null_Throws_ArgumentNullException()
	{
		var none = Substitute.For<Action<IMsg>>();
		Test07((mbe, some) => F.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test08_Return_Void_If_Some_Runs_Some_Action_With_Value()
	{
		var none = Substitute.For<Action<IMsg>>();
		Test08((mbe, some) => F.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test09_Return_Value_If_Some_And_Some_Func_Is_Null_Throws_ArgumentNullException()
	{
		var none = Substitute.For<Func<IMsg, string>>();
		Test09((mbe, some) => F.Switch(mbe, some, none));
	}

	[Fact]
	public override void Test10_Return_Value_If_Some_Runs_Some_Func_With_Value()
	{
		var none = Substitute.For<Func<IMsg, string>>();
		Test10((mbe, some) => F.Switch(mbe, some, none));
	}
}
