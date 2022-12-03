// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

public class AuditAsync_Tests : Abstracts.AuditAsync_Tests
{
	#region General

	[Fact]
	public override async Task Test01_If_Unknown_Maybe_Throws_UnknownMaybeException()
	{
		var anyTask = Substitute.For<Func<Maybe<int>, Task>>();
		var anyValueTask = Substitute.For<Func<Maybe<int>, ValueTask>>();
		var someTask = Substitute.For<Func<int, Task>>();
		var someValueTask = Substitute.For<Func<int, ValueTask>>();
		var noneTask = Substitute.For<Func<IMsg, Task>>();
		var noneValueTask = Substitute.For<Func<IMsg, ValueTask>>();

		await Test01(
			mbe => mbe.AuditAsync(anyTask),
			mbe => mbe.AuditAsync(anyValueTask)
		);
		await Test01(
			mbe => mbe.AuditAsync(someTask),
			mbe => mbe.AuditAsync(someValueTask)
		);
		await Test01(
			mbe => mbe.AuditAsync(noneTask),
			mbe => mbe.AuditAsync(noneValueTask)
		);
		await Test01(
			mbe => mbe.AuditAsync(someTask, noneTask),
			mbe => mbe.AuditAsync(someValueTask, noneValueTask)
		);
	}

	#endregion General

	#region Any

	[Fact]
	public override async Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Maybe()
	{
		await Test04(
			(mbe, any) => mbe.AuditAsync(any),
			(mbe, any) => mbe.AuditAsync(any)
		);
	}

	[Fact]
	public override async Task Test05_None_Runs_Audit_Func_And_Returns_Original_Maybe()
	{
		await Test05(
			(mbe, any) => mbe.AuditAsync(any),
			(mbe, any) => mbe.AuditAsync(any)
		);
	}

	[Fact]
	public override async Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		await Test08(
			(mbe, any) => mbe.AuditAsync(any),
			(mbe, any) => mbe.AuditAsync(any)
		);
	}

	[Fact]
	public override async Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		await Test09(
			(mbe, any) => mbe.AuditAsync(any),
			(mbe, any) => mbe.AuditAsync(any)
		);
	}

	#endregion Any

	#region Some / None

	[Fact]
	public override async Task Test11_Some_Runs_Some_Func_And_Returns_Original_Maybe()
	{
		var noneTask = Substitute.For<Func<IMsg, Task>>();
		var noneValueTask = Substitute.For<Func<IMsg, ValueTask>>();

		await Test11(
			(mbe, some) => mbe.AuditAsync(some),
			(mbe, some) => mbe.AuditAsync(some)
		);
		await Test11(
			(mbe, some) => mbe.AuditAsync(some, noneTask),
			(mbe, some) => mbe.AuditAsync(some, noneValueTask)
		);
	}

	[Fact]
	public override async Task Test13_None_Runs_None_Func_And_Returns_Original_Maybe()
	{
		var someTask = Substitute.For<Func<int, Task>>();
		var someValueTask = Substitute.For<Func<int, ValueTask>>();

		await Test13(
			(mbe, none) => mbe.AuditAsync(none),
			(mbe, none) => mbe.AuditAsync(none)
		);
		await Test13(
			(mbe, none) => mbe.AuditAsync(someTask, none),
			(mbe, none) => mbe.AuditAsync(someValueTask, none)
		);
	}

	[Fact]
	public override async Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		var noneTask = Substitute.For<Func<IMsg, Task>>();
		var noneValueTask = Substitute.For<Func<IMsg, ValueTask>>();

		await Test15(
			(mbe, some) => mbe.AuditAsync(some),
			(mbe, some) => mbe.AuditAsync(some)
		);
		await Test15(
			(mbe, some) => mbe.AuditAsync(some, noneTask),
			(mbe, some) => mbe.AuditAsync(some, noneValueTask)
		);
	}

	[Fact]
	public override async Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Maybe()
	{
		var someTask = Substitute.For<Func<int, Task>>();
		var someValueTask = Substitute.For<Func<int, ValueTask>>();

		await Test17(
			(mbe, none) => mbe.AuditAsync(none),
			(mbe, none) => mbe.AuditAsync(none)
		);
		await Test17(
			(mbe, none) => mbe.AuditAsync(someTask, none),
			(mbe, none) => mbe.AuditAsync(someValueTask, none)
		);
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
