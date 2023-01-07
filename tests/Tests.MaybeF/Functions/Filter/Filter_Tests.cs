// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class Filter_Tests : Abstracts.Filter_Tests
{
	[Fact]
	public override void Test00_If_Unknown_Maybe_Returns_None_With_UnknownMaybeTypeMsg()
	{
		var predicate = Substitute.For<Func<int, bool>>();
		Test00(mbe => F.Filter(mbe, predicate));
	}

	[Fact]
	public override void Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
	{
		Test01((mbe, predicate) => F.Filter(mbe, predicate));
	}

	[Fact]
	public override void Test02_When_Some_And_Predicate_True_Returns_Value()
	{
		Test02((mbe, predicate) => F.Filter(mbe, predicate));
	}

	[Fact]
	public override void Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
	{
		Test03((mbe, predicate) => F.Filter(mbe, predicate));
	}

	[Fact]
	public override void Test04_When_None_Returns_None_With_Original_Msg()
	{
		Test04((mbe, predicate) => F.Filter(mbe, predicate));
	}
}
