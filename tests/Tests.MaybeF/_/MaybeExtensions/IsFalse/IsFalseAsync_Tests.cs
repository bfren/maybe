// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class IsFalseAsync_Tests : Abstracts.IsFalseAsync_Tests
{
	[Fact]
	public override async Task Test00_Is_Some_Returns_Opposite_Of_Value()
	{
		await Test00(mbe => mbe.IsFalseAsync());
	}

	[Fact]
	public override async Task Test01_Is_None_Returns_False()
	{
		await Test01(mbe => mbe.IsFalseAsync());
	}
}
