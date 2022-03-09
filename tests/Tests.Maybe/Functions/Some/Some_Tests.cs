// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Xunit;

namespace Maybe.Functions.MaybeF_Tests;

public class Some_Tests : Tests.Maybe.Abstracts.Some_Tests
{
	[Fact]
	public override void Test00_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionReason()
	{
		Test00((val, handler) => MaybeF.Some(val, handler));
	}

	[Fact]
	public override void Test01_Nullable_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionReason()
	{
		Test01((val, nullable, handler) => MaybeF.Some(val, nullable, handler));
	}

	[Fact]
	public override void Test02_Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
	{
		Test02((val, handler) => MaybeF.Some(val, handler));
	}

	[Fact]
	public override void Test03_Nullable_Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
	{
		Test03((val, nullable, handler) => MaybeF.Some(val, nullable, handler));
	}

	[Fact]
	public override void Test04_Null_Input_Value_Returns_None()
	{
		Test04(val => MaybeF.Some(val));
	}

	[Fact]
	public override void Test05_Null_Input_Func_Returns_None()
	{
		Test05((val, handler) => MaybeF.Some(val, handler));
	}

	[Fact]
	public override void Test06_Nullable_Allow_Null_False_Null_Input_Value_Returns_None_With_AllowNullWasFalseReason()
	{
		Test06((val, nullable) => MaybeF.Some(val, nullable));
	}

	[Fact]
	public override void Test07_Nullable_Allow_Null_False_Null_Input_Func_Returns_None_With_AllowNullWasFalseReason()
	{
		Test07((val, nullable, handler) => MaybeF.Some(val, nullable, handler));
	}

	[Fact]
	public override void Test08_Nullable_Allow_Null_True_Null_Input_Value_Returns_Some_With_Null_Value()
	{
		Test08((val, nullable) => MaybeF.Some(val, nullable));
	}

	[Fact]
	public override void Test09_Nullable_Allow_Null_True_Null_Input_Func_Returns_Some_With_Null_Value()
	{
		Test09((val, nullable, handler) => MaybeF.Some(val, nullable, handler));
	}

	[Fact]
	public override void Test10_Not_Null_Value_Returns_Some()
	{
		Test10(val => MaybeF.Some(val));
	}

	[Fact]
	public override void Test11_Not_Null_Func_Returns_Some()
	{
		Test11((val, handler) => MaybeF.Some(val, handler));
	}

	[Fact]
	public override void Test12_Nullable_Not_Null_Value_Returns_Some()
	{
		Test12((val, nullable) => MaybeF.Some(val, nullable));
	}

	[Fact]
	public override void Test13_Nullable_Not_Null_Func_Returns_Some()
	{
		Test13((val, nullable, handler) => MaybeF.Some(val, nullable, handler));
	}
}
