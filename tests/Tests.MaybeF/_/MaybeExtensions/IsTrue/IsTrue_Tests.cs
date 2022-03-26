// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class IsTrue_Tests : Abstracts.IsTrue_Tests
{
	[Fact]
	public override void Test00_Is_Some_Returns_Value()
	{
		Test00(mbe => mbe.IsTrue());
	}

	[Fact]
	public override void Test01_Is_None_Returns_False()
	{
		Test01(mbe => mbe.IsTrue());
	}
}
