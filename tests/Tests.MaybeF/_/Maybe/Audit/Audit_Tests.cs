﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class Audit_Tests : Abstracts.Audit_Tests
{
	#region General

	[Fact]
	public override void Test01_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		Test01(mbe => mbe.Audit(Substitute.For<Action<Maybe<int>>>()));
		Test01(mbe => mbe.Audit(Substitute.For<Action<int>>()));
		Test01(mbe => mbe.Audit(Substitute.For<Action<IMsg>>()));
		Test01(mbe => mbe.Audit(Substitute.For<Action<int>>(), Substitute.For<Action<IMsg>>()));
	}

	#endregion General

	#region Any

	[Fact]
	public override void Test02_Some_Runs_Audit_And_Returns_Original_Maybe()
	{
		Test02((mbe, any) => mbe.Audit(any));
	}

	[Fact]
	public override void Test03_None_Runs_Audit_And_Returns_Original_Maybe()
	{
		Test03((mbe, any) => mbe.Audit(any));
	}

	[Fact]
	public override void Test04_Some_Catches_Exception_And_Returns_Original_Maybe()
	{
		Test04((mbe, any) => mbe.Audit(any));
	}

	[Fact]
	public override void Test05_None_Catches_Exception_And_Returns_Original_Maybe()
	{
		Test05((mbe, any) => mbe.Audit(any));
	}

	#endregion Any

	#region Some / None

	[Fact]
	public override void Test06_Some_Runs_Some_And_Returns_Original_Maybe()
	{
		Test06((mbe, some) => mbe.Audit(some));
		Test06((mbe, some) => mbe.Audit(some, Substitute.For<Action<IMsg>>()));
	}

	[Fact]

	public override void Test07_None_Runs_None_And_Returns_Original_Maybe()
	{
		Test07((mbe, none) => mbe.Audit(none));
		Test07((mbe, none) => mbe.Audit(Substitute.For<Action<int>>(), none));
	}

	[Fact]
	public override void Test08_Some_Catches_Exception_And_Returns_Original_Maybe()
	{
		Test08((mbe, some) => mbe.Audit(some));
		Test08((mbe, some) => mbe.Audit(some, Substitute.For<Action<IMsg>>()));
	}

	[Fact]
	public override void Test09_None_Catches_Exception_And_Returns_Original_Maybe()
	{
		Test09((mbe, none) => mbe.Audit(none));
		Test09((mbe, none) => mbe.Audit(Substitute.For<Action<int>>(), none));
	}

	#endregion Some / None

	#region Unused

	[Fact]
	public override void Test00_Null_Args_Returns_Original_Maybe()
	{
		// Unused
	}

	#endregion Unused
}
