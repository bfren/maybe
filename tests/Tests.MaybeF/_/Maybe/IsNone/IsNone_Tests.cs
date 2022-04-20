// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class IsNone_Tests : Abstracts.IsNone_Tests
{
	[Fact]
	public override void Test00_Is_None_Returns_True_Sets_Msg()
	{
		Test00((Maybe<int> mbe, out IMsg rsn) => mbe.IsNone(out rsn));
	}

	[Fact]
	public override void Test01_Is_Not_None_Returns_False()
	{
		Test01((Maybe<string> mbe, out IMsg rsn) => mbe.IsNone(out rsn));
	}

	#region Unused

	[Fact]
	public override void Test02_Is_Null_Returns_False() { }

	#endregion Unused
}
