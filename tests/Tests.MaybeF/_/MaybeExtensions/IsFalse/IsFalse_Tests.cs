// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class IsFalse_Tests : Abstracts.IsFalse_Tests
{
	[Fact]
	public override void Test00_Is_Some_Returns_Opposite_Of_Value()
	{
		Test00(mbe => mbe.IsFalse());
	}

	[Fact]
	public override void Test01_Is_None_Returns_False()
	{
		Test01(mbe => mbe.IsFalse());
	}
}
