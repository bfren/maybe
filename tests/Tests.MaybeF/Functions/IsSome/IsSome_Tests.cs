// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class IsSome_Tests : Abstracts.IsSome_Tests
{
	[Fact]
	public override void Test00_Is_Some_Returns_True_Sets_Value()
	{
		Test00((Maybe<int> mbe, out int val) => F.IsSome(mbe, out val));
	}

	[Fact]
	public override void Test01_Is_Not_Some_Returns_False()
	{
		Test01((Maybe<string> mbe, out string val) => F.IsSome(mbe, out val));
	}

	[Fact]
	public override void Test02_Is_Null_Returns_False()
	{
		Test02((Maybe<Guid> mbe, out Guid val) => F.IsSome(mbe, out val));
	}
}
