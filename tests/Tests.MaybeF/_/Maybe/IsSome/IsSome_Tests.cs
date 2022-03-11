// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class IsSome_Tests : Abstracts.IsSome_Tests
{
	[Fact]
	public override void Test00_Is_Some_Returns_True_Sets_Value()
	{
		Test00((Maybe<int> mbe, out int val) => mbe.IsSome(out val));
	}

	[Fact]
	public override void Test01_Is_Not_Some_Returns_False()
	{
		Test01((Maybe<string> mbe, out string val) => mbe.IsSome(out val));
	}
}
