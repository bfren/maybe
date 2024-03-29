﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class SomeAsync_Tests : Abstracts.SomeAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((val, handler) => F.SomeAsync(val, handler));
	}

	[Fact]
	public override async Task Test01_Nullable_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test01((val, nullable, handler) => F.SomeAsync(val, nullable, handler));
	}

	[Fact]
	public override async Task Test02_Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
	{
		await Test02((val, handler) => F.SomeAsync(val, handler));
	}

	[Fact]
	public override async Task Test03_Nullable_Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
	{
		await Test03((val, nullable, handler) => F.SomeAsync(val, nullable, handler));
	}

	[Fact]
	public override async Task Test04_Null_Input_Returns_None()
	{
		await Test04((val, handler) => F.SomeAsync(val, handler));
	}

	[Fact]
	public override async Task Test05_Nullable_Allow_Null_False_Null_Input_Returns_None_With_AllowNullWasFalseMsg()
	{
		await Test05((val, nullable, handler) => F.SomeAsync(val, nullable, handler));
	}

	[Fact]
	public override async Task Test06_Nullable_Allow_Null_True_Null_Input_Returns_Some_With_Null_Value()
	{
		await Test06((val, nullable, handler) => F.SomeAsync(val, nullable, handler));
	}

	[Fact]
	public override async Task Test07_Not_Null_Returns_Some()
	{
		await Test07((val, handler) => F.SomeAsync(val, handler));
	}

	[Fact]
	public override async Task Test08_Nullable_Not_Null_Returns_Some()
	{
		await Test08((val, nullable, handler) => F.SomeAsync(val, nullable, handler));
	}
}
