// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class IsTrueAsync_Tests : Abstracts.IsTrueAsync_Tests
{
	[Fact]
	public override async Task Test00_Is_Some_Returns_Value()
	{
		await Test00(mbe => F.IsTrueAsync(mbe));
	}

	[Fact]
	public override async Task Test01_Is_None_Returns_False()
	{
		await Test01(mbe => F.IsTrueAsync(mbe));
	}
}
