// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class IfNull_Tests : Abstracts.IfNull_Tests
{
	[Fact]
	public override void Test00_Exception_In_IfNull_Func_Returns_None_With_UnhandledExceptionMsg()
	{
		Test00((mbe, ifNull) => F.IfNull(mbe, ifNull));
		Test00((mbe, ifNull) => F.IfNull(mbe, () => { ifNull(); return new TestMsg(); }));
	}

	[Fact]
	public override void Test01_Some_With_Null_Value_Runs_IfNull_Func()
	{
		Test01((mbe, ifNull) => F.IfNull(mbe, ifNull));
	}

	[Fact]
	public override void Test02_None_With_NullValueMsg_Runs_IfNull_Func()
	{
		Test02((mbe, ifNull) => F.IfNull(mbe, ifNull));
	}

	[Fact]
	public override void Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Msg()
	{
		Test03((mbe, ifNull) => F.IfNull(mbe, ifNull));
	}

	[Fact]
	public override void Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Msg()
	{
		Test04((mbe, ifNull) => F.IfNull(mbe, ifNull));
	}

	[Theory]
	[InlineData(null)]
	public override void Test05_Null_Maybe_Runs_IfNull_Func(Maybe<int> input)
	{
		Test05(ifNull => F.IfNull(input, ifNull));
	}

	[Fact]
	public override void Test06_Some_With_Null__Runs_IfNull()
	{
		Test06((mbe, ifNull, ifSome) => F.IfNull(mbe, ifNull, ifSome, F.DefaultHandler));
		Test06((mbe, ifNull, ifSome) => F.IfNull(mbe, () => F.Some(ifNull()), x => F.Some(ifSome(x))));
	}

	[Fact]
	public override void Test07_Some_With_Value__Runs_IfSome()
	{
		Test07((mbe, ifNull, ifSome) => F.IfNull(mbe, ifNull, ifSome, F.DefaultHandler));
		Test07((mbe, ifNull, ifSome) => F.IfNull(mbe, () => F.Some(ifNull()), x => F.Some(ifSome(x))));
	}

	[Fact]
	public override void Test08_None_With_NullValueMsg__Runs_IfNull()
	{
		Test08((mbe, ifNull, ifSome) => F.IfNull(mbe, ifNull, ifSome, F.DefaultHandler));
		Test08((mbe, ifNull, ifSome) => F.IfNull(mbe, () => F.Some(ifNull()), x => F.Some(ifSome(x))));
	}

	[Fact]
	public override void Test09_None_With_Msg__Returns_None()
	{
		Test09((mbe, ifNull, ifSome) => F.IfNull(mbe, ifNull, ifSome, F.DefaultHandler));
		Test09((mbe, ifNull, ifSome) => F.IfNull(mbe, () => F.Some(ifNull()), x => F.Some(ifSome(x))));
	}

	[Fact]
	public override void Test10_Exception_In_IfNull__Uses_Handler()
	{
		Test10((mbe, ifNull, ifSome, handler) => F.IfNull(mbe, ifNull, ifSome, handler));
	}

	[Fact]
	public override void Test11_Exception_In_IfSome__Uses_Handler()
	{
		Test11((mbe, ifNull, ifSome, handler) => F.IfNull(mbe, ifNull, ifSome, handler));
	}

	[Fact]
	public override void Test12_Exception_In_IfNull__Uses_DefaultHandler()
	{
		Test12((mbe, ifNull, ifSome) => F.IfNull(mbe, ifNull, ifSome));
	}

	[Fact]
	public override void Test13_Exception_In_IfSome__Uses_DefaultHandler()
	{
		Test13((mbe, ifNull, ifSome) => F.IfNull(mbe, ifNull, ifSome));
	}
}
