// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class AuditAsync_Tests : Abstracts.AuditAsync_Tests
{
	#region General

	[Fact]
	public override async Task Test01_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var anyA = Substitute.For<Action<Maybe<int>>>();
		var anyF = Substitute.For<Func<Maybe<int>, Task>>();
		var someA = Substitute.For<Action<int>>();
		var someF = Substitute.For<Func<int, Task>>();
		var noneA = Substitute.For<Action<IMsg>>();
		var noneF = Substitute.For<Func<IMsg, Task>>();

		await Test01(mbe => mbe.AsTask().AuditAsync(anyA));
		await Test01(mbe => mbe.AsTask().AuditAsync(anyF));
		await Test01(mbe => mbe.AsTask().AuditAsync(someA));
		await Test01(mbe => mbe.AsTask().AuditAsync(someF));
		await Test01(mbe => mbe.AsTask().AuditAsync(noneA));
		await Test01(mbe => mbe.AsTask().AuditAsync(noneF));
		await Test01(mbe => mbe.AsTask().AuditAsync(someA, noneA));
		await Test01(mbe => mbe.AsTask().AuditAsync(someF, noneF));
	}

	#endregion General

	#region Any

	[Fact]
	public override async Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Maybe()
	{
		await Test02((some, any) => some.AsTask().AuditAsync(any));
	}

	[Fact]
	public override async Task Test03_None_Runs_Audit_Action_And_Returns_Original_Maybe()
	{
		await Test03((none, any) => none.AsTask().AuditAsync(any));
	}

	[Fact]
	public override async Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Maybe()
	{
		await Test04((some, any) => some.AsTask().AuditAsync(any));
	}

	[Fact]
	public override async Task Test05_None_Runs_Audit_Func_And_Returns_Original_Maybe()
	{
		await Test05((none, any) => none.AsTask().AuditAsync(any));
	}

	[Fact]
	public override async Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Maybe()
	{
		await Test06((some, any) => some.AsTask().AuditAsync(any));
	}

	[Fact]
	public override async Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Maybe()
	{
		await Test07((none, any) => none.AsTask().AuditAsync(any));
	}

	[Fact]
	public override async Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		await Test08((some, any) => some.AsTask().AuditAsync(any));
	}

	[Fact]
	public override async Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		await Test09((none, any) => none.AsTask().AuditAsync(any));
	}

	#endregion Any

	#region Some / None
	[Fact]
	public override async Task Test10_Some_Runs_Some_Action_And_Returns_Original_Maybe()
	{
		var none = Substitute.For<Action<IMsg>>();

		await Test10((mbe, some) => mbe.AsTask().AuditAsync(some));
		await Test10((mbe, some) => mbe.AsTask().AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test11_Some_Runs_Some_Func_And_Returns_Original_Maybe()
	{
		var none = Substitute.For<Func<IMsg, Task>>();

		await Test11((mbe, some) => mbe.AsTask().AuditAsync(some));
		await Test11((mbe, some) => mbe.AsTask().AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test12_None_Runs_None_Action_And_Returns_Original_Maybe()
	{
		var some = Substitute.For<Action<int>>();

		await Test12((mbe, none) => mbe.AsTask().AuditAsync(none));
		await Test12((mbe, none) => mbe.AsTask().AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test13_None_Runs_None_Func_And_Returns_Original_Maybe()
	{
		var some = Substitute.For<Func<int, Task>>();

		await Test13((mbe, none) => mbe.AsTask().AuditAsync(none));
		await Test13((mbe, none) => mbe.AsTask().AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Maybe()
	{
		var none = Substitute.For<Action<IMsg>>();

		await Test14((mbe, some) => mbe.AsTask().AuditAsync(some));
		await Test14((mbe, some) => mbe.AsTask().AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		var none = Substitute.For<Func<IMsg, Task>>();

		await Test15((mbe, some) => mbe.AsTask().AuditAsync(some));
		await Test15((mbe, some) => mbe.AsTask().AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Maybe()
	{
		var some = Substitute.For<Action<int>>();

		await Test16((mbe, none) => mbe.AsTask().AuditAsync(none));
		await Test16((mbe, none) => mbe.AsTask().AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		var some = Substitute.For<Func<int, Task>>();

		await Test17((mbe, none) => mbe.AsTask().AuditAsync(none));
		await Test17((mbe, none) => mbe.AsTask().AuditAsync(some, none));
	}

	#endregion Some / None

	#region Unused

	[Fact]
	public override Task Test00_Null_Args_Returns_Original_Maybe() =>
		Task.CompletedTask;

	#endregion Unused
}
