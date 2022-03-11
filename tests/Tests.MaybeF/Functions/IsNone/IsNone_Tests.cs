// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class IsNone_Tests : Abstracts.IsNone_Tests
{
	[Fact]
	public override void Test00_Is_None_Returns_True_Sets_Reason()
	{
		Test00((Maybe<int> mbe, out IReason rsn) => F.IsNone(mbe, out rsn));
	}

	[Fact]
	public override void Test01_Is_Not_None_Returns_False()
	{
		Test01((Maybe<string> mbe, out IReason rsn) => F.IsNone(mbe, out rsn));
	}
}
