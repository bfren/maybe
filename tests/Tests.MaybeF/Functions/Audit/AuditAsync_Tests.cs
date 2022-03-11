﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class AuditAsync_Tests : Abstracts.AuditAsync_Tests
{
	#region General

	[Fact]
	public override async Task Test00_Null_Args_Returns_Original_Option()
	{
		await Test00(mbe => F.AuditAsync(mbe, null, null, null)).ConfigureAwait(false);
		await Test00(mbe => F.AuditAsync(mbe.AsTask, null, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var any = Substitute.For<Func<Maybe<int>, Task>>();
		var some = Substitute.For<Func<int, Task>>();
		var none = Substitute.For<Func<IReason, Task>>();

		await Test01(mbe => F.AuditAsync(mbe, any, null, null)).ConfigureAwait(false);
		await Test01(mbe => F.AuditAsync(mbe, null, some, null)).ConfigureAwait(false);
		await Test01(mbe => F.AuditAsync(mbe, null, null, none)).ConfigureAwait(false);
		await Test01(mbe => F.AuditAsync(mbe, any, some, none)).ConfigureAwait(false);
		await Test01(mbe => F.AuditAsync(mbe.AsTask, any, null, null)).ConfigureAwait(false);
		await Test01(mbe => F.AuditAsync(mbe.AsTask, null, some, null)).ConfigureAwait(false);
		await Test01(mbe => F.AuditAsync(mbe.AsTask, null, null, none)).ConfigureAwait(false);
		await Test01(mbe => F.AuditAsync(mbe.AsTask, any, some, none)).ConfigureAwait(false);
	}

	#endregion General

	#region Any

	[Fact]
	public override async Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Option()
	{
		await Test02((some, any) => F.AuditAsync(some.AsTask, any, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_None_Runs_Audit_Action_And_Returns_Original_Option()
	{
		await Test03((none, any) => F.AuditAsync(none.AsTask, any, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Option()
	{
		await Test04((some, any) => F.AuditAsync(some, any, null, null)).ConfigureAwait(false);
		await Test04((some, any) => F.AuditAsync(some.AsTask, any, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test05_None_Runs_Audit_Func_And_Returns_Original_Option()
	{
		await Test05((none, any) => F.AuditAsync(none, any, null, null)).ConfigureAwait(false);
		await Test05((none, any) => F.AuditAsync(none.AsTask, any, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option()
	{
		await Test06((some, any) => F.AuditAsync(some.AsTask, any, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option()
	{
		await Test07((none, any) => F.AuditAsync(none.AsTask, any, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
	{
		await Test08((some, any) => F.AuditAsync(some, any, null, null)).ConfigureAwait(false);
		await Test08((some, any) => F.AuditAsync(some.AsTask, any, null, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
	{
		await Test09((none, any) => F.AuditAsync(none, any, null, null)).ConfigureAwait(false);
		await Test05((none, any) => F.AuditAsync(none.AsTask, any, null, null)).ConfigureAwait(false);
	}

	#endregion Any

	#region Some / None
	[Fact]
	public override async Task Test10_Some_Runs_Some_Action_And_Returns_Original_Option()
	{
		await Test10((mbe, some) => F.AuditAsync(mbe.AsTask, null, some, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test11_Some_Runs_Some_Func_And_Returns_Original_Option()
	{
		await Test11((mbe, some) => F.AuditAsync(mbe, null, some, null)).ConfigureAwait(false);
		await Test11((mbe, some) => F.AuditAsync(mbe.AsTask, null, some, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test12_None_Runs_None_Action_And_Returns_Original_Option()
	{
		await Test12((mbe, none) => F.AuditAsync(mbe.AsTask, null, null, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test13_None_Runs_None_Func_And_Returns_Original_Option()
	{
		await Test13((mbe, none) => F.AuditAsync(mbe, null, null, none)).ConfigureAwait(false);
		await Test13((mbe, none) => F.AuditAsync(mbe.AsTask, null, null, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option()
	{
		await Test14((mbe, some) => F.AuditAsync(mbe.AsTask, null, some, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option()
	{
		await Test15((mbe, some) => F.AuditAsync(mbe, null, some, null)).ConfigureAwait(false);
		await Test15((mbe, some) => F.AuditAsync(mbe.AsTask, null, some, null)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option()
	{
		await Test16((mbe, none) => F.AuditAsync(mbe.AsTask, null, null, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option()
	{
		await Test17((mbe, none) => F.AuditAsync(mbe, null, null, none)).ConfigureAwait(false);
		await Test17((mbe, none) => F.AuditAsync(mbe.AsTask, null, null, none)).ConfigureAwait(false);
	}

	#endregion Some / None
}