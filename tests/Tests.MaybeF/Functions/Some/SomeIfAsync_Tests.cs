﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class SomeIfAsync_Tests : Abstracts.SomeIfAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_Thrown_By_Predicate_With_Value_Calls_Handler_Returns_None()
	{
		await Test00((predicate, value, handler) => F.SomeIfAsync(predicate, value, handler));
		await Test00((predicate, value, handler) => F.SomeIfAsync(_ => predicate(), value, handler));
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_By_Predicate_With_Value_Func_Calls_Handler_Returns_None()
	{
		await Test01((predicate, value, handler) => F.SomeIfAsync(predicate, value, handler));
		await Test01((predicate, value, handler) => F.SomeIfAsync(_ => predicate(), value, handler));
	}

	[Fact]
	public override async Task Test02_Exception_Thrown_By_Value_Func_Calls_Handler_Returns_None()
	{
		await Test02((predicate, value, handler) => F.SomeIfAsync(predicate, value, handler));
		await Test02((predicate, value, handler) => F.SomeIfAsync(_ => predicate(), value, handler));
	}

	[Fact]
	public override async Task Test03_Predicate_True_With_Value_Returns_Some()
	{
		await Test03((predicate, value, handler) => F.SomeIfAsync(predicate, value, handler));
		await Test03((predicate, value, handler) => F.SomeIfAsync(_ => predicate(), value, handler));
	}

	[Fact]
	public override async Task Test04_Predicate_True_With_Value_Func_Returns_Some()
	{
		await Test04((predicate, value, handler) => F.SomeIfAsync(predicate, value, handler));
		await Test04((predicate, value, handler) => F.SomeIfAsync(_ => predicate(), value, handler));
	}

	[Fact]
	public override async Task Test05_Predicate_False_With_Value_Returns_None_With_PredicateWasFalseMsg()
	{
		await Test05((predicate, value, handler) => F.SomeIfAsync(predicate, value, handler));
		await Test05((predicate, value, handler) => F.SomeIfAsync(_ => predicate(), value, handler));
	}

	[Fact]
	public override async Task Test06_Predicate_False_With_Value_Func_Returns_None_With_PredicateWasFalseMsg()
	{
		await Test06((predicate, value, handler) => F.SomeIfAsync(predicate, value, handler));
		await Test06((predicate, value, handler) => F.SomeIfAsync(_ => predicate(), value, handler));
	}

	[Fact]
	public override async Task Test07_Predicate_False_Bypasses_Value_Func()
	{
		await Test07((predicate, value, handler) => F.SomeIfAsync(predicate, value, handler));
	}
}
