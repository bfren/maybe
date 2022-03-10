// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class Audit_Tests : Abstracts.Audit_Tests
{
	#region General

	[Fact]
	public override void Test00_Null_Args_Returns_Original_Option()
	{
		Test00(mbe => F.Audit(mbe, null, null, null));
	}

	[Fact]
	public override void Test01_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		Test01(mbe => F.Audit(mbe, Substitute.For<Action<Maybe<int>>>(), null, null));
		Test01(mbe => F.Audit(mbe, null, Substitute.For<Action<int>>(), null));
		Test01(mbe => F.Audit(mbe, null, null, Substitute.For<Action<IReason>>()));
	}

	#endregion General

	#region Any

	[Fact]
	public override void Test02_Some_Runs_Audit_And_Returns_Original_Option()
	{
		Test02((mbe, any) => F.Audit(mbe, any, null, null));
	}

	[Fact]
	public override void Test03_None_Runs_Audit_And_Returns_Original_Option()
	{
		Test03((mbe, any) => F.Audit(mbe, any, null, null));
	}

	[Fact]
	public override void Test04_Some_Catches_Exception_And_Returns_Original_Option()
	{
		Test04((mbe, any) => F.Audit(mbe, any, null, null));
	}

	[Fact]
	public override void Test05_None_Catches_Exception_And_Returns_Original_Option()
	{
		Test05((mbe, any) => F.Audit(mbe, any, null, null));
	}

	#endregion Any

	#region Some / None

	[Fact]
	public override void Test06_Some_Runs_Some_And_Returns_Original_Option()
	{
		Test06((mbe, some) => F.Audit(mbe, null, some, null));
	}

	[Fact]
	public override void Test07_None_Runs_None_And_Returns_Original_Option()
	{
		Test07((mbe, none) => F.Audit(mbe, null, null, none));
	}

	[Fact]
	public override void Test08_Some_Catches_Exception_And_Returns_Original_Option()
	{
		Test08((mbe, some) => F.Audit(mbe, null, some, null));
	}

	[Fact]
	public override void Test09_None_Catches_Exception_And_Returns_Original_Option()
	{
		Test09((mbe, none) => F.Audit(mbe, null, null, none));
	}

	#endregion Some / None
}
