// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class AuditAsync_Tests : Abstracts.AuditAsync_Tests
{
	#region General

	[Fact]
	public override async Task Test01_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var any = Substitute.For<Func<Maybe<int>, Task>>();
		var some = Substitute.For<Func<int, Task>>();
		var none = Substitute.For<Func<IMsg, Task>>();

		await Test01(mbe => mbe.AuditAsync(any));
		await Test01(mbe => mbe.AuditAsync(some));
		await Test01(mbe => mbe.AuditAsync(none));
		await Test01(mbe => mbe.AuditAsync(some, none));
	}

	#endregion General

	#region Any

	[Fact]
	public override async Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Maybe()
	{
		await Test04((mbe, any) => mbe.AuditAsync(any));
	}

	[Fact]
	public override async Task Test05_None_Runs_Audit_Func_And_Returns_Original_Maybe()
	{
		await Test05((mbe, any) => mbe.AuditAsync(any));
	}

	[Fact]
	public override async Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		await Test08((mbe, any) => mbe.AuditAsync(any));
	}

	[Fact]
	public override async Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		await Test09((mbe, any) => mbe.AuditAsync(any));
	}

	#endregion Any

	#region Some / None

	[Fact]
	public override async Task Test11_Some_Runs_Some_Func_And_Returns_Original_Maybe()
	{
		var none = Substitute.For<Func<IMsg, Task>>();

		await Test11((mbe, some) => mbe.AuditAsync(some));
		await Test11((mbe, some) => mbe.AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test13_None_Runs_None_Func_And_Returns_Original_Maybe()
	{
		var some = Substitute.For<Func<int, Task>>();

		await Test13((mbe, none) => mbe.AuditAsync(none));
		await Test13((mbe, none) => mbe.AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		var none = Substitute.For<Func<IMsg, Task>>();

		await Test15((mbe, some) => mbe.AuditAsync(some));
		await Test15((mbe, some) => mbe.AuditAsync(some, none));
	}

	[Fact]
	public override async Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		var some = Substitute.For<Func<int, Task>>();

		await Test17((mbe, none) => mbe.AuditAsync(none));
		await Test17((mbe, none) => mbe.AuditAsync(some, none));
	}

	#endregion Some / None

	#region Unused

	[Fact]
	public override Task Test00_Null_Args_Returns_Original_Maybe() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Maybe() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test03_None_Runs_Audit_Action_And_Returns_Original_Maybe() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Maybe() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Maybe() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test10_Some_Runs_Some_Action_And_Returns_Original_Maybe() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test12_None_Runs_None_Action_And_Returns_Original_Maybe() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Maybe() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Maybe() =>
		Task.CompletedTask;

	#endregion Unused
}
