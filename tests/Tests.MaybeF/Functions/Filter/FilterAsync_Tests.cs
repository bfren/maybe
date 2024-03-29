﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class FilterAsync_Tests : Abstracts.FilterAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg()
	{
		var predicate = Substitute.For<Func<int, Task<bool>>>();

		await Test00(mbe => F.FilterAsync(mbe, predicate));
		await Test00(mbe => F.FilterAsync(mbe.AsTask(), predicate));
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test01((mbe, predicate) => F.FilterAsync(mbe, predicate));
		await Test01((mbe, predicate) => F.FilterAsync(mbe.AsTask(), predicate));
	}

	[Fact]
	public override async Task Test02_When_Some_And_Predicate_True_Returns_Value()
	{
		await Test02((mbe, predicate) => F.FilterAsync(mbe, predicate));
		await Test02((mbe, predicate) => F.FilterAsync(mbe.AsTask(), predicate));
	}

	[Fact]
	public override async Task Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
	{
		await Test03((mbe, predicate) => F.FilterAsync(mbe, predicate));
		await Test03((mbe, predicate) => F.FilterAsync(mbe.AsTask(), predicate));
	}

	[Fact]
	public override async Task Test04_When_None_Returns_None_With_Original_Msg()
	{
		await Test04((mbe, predicate) => F.FilterAsync(mbe, predicate));
		await Test04((mbe, predicate) => F.FilterAsync(mbe.AsTask(), predicate));
	}
}
