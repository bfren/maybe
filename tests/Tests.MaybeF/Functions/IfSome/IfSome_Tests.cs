// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Xunit;

namespace MaybeF.MaybeF_Tests;

public class IfSome_Tests : Abstracts.IfSome_Tests
{
	[Fact]
	public override void Test00_Exception_In_IfSome_Action_Returns_None_With_UnhandledExceptionReason()
	{
		Test00((mbe, ifSome) => F.IfSome(mbe, ifSome));
	}

	[Fact]
	public override void Test01_None_Returns_Original_Option()
	{
		Test01((mbe, ifSome) => F.IfSome(mbe, ifSome));
	}

	[Fact]
	public override void Test02_Some_Runs_IfSome_Action_And_Returns_Original_Option()
	{
		Test02((mbe, ifSome) => F.IfSome(mbe, ifSome));
	}
}
