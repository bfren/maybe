﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class FilterAsync_Tests : Abstracts.FilterAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg()
	{
		var syncPredicate = Substitute.For<Func<int, bool>>();
		var asyncPredicate = Substitute.For<Func<int, Task<bool>>>();

		await Test00(mbe => mbe.AsTask().FilterAsync(syncPredicate));
		await Test00(mbe => mbe.AsTask().FilterAsync(asyncPredicate));
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test01((mbe, predicate) => mbe.AsTask().FilterAsync(x => predicate(x).GetAwaiter().GetResult()));
		await Test01((mbe, predicate) => mbe.AsTask().FilterAsync(predicate));
	}

	[Fact]
	public override async Task Test02_When_Some_And_Predicate_True_Returns_Value()
	{
		await Test02((mbe, predicate) => mbe.AsTask().FilterAsync(x => predicate(x).GetAwaiter().GetResult()));
		await Test02((mbe, predicate) => mbe.AsTask().FilterAsync(predicate));
	}

	[Fact]
	public override async Task Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
	{
		await Test03((mbe, predicate) => mbe.AsTask().FilterAsync(x => predicate(x).GetAwaiter().GetResult()));
		await Test03((mbe, predicate) => mbe.AsTask().FilterAsync(predicate));
	}

	[Fact]
	public override async Task Test04_When_None_Returns_None_With_Original_Msg()
	{
		await Test04((mbe, predicate) => mbe.AsTask().FilterAsync(x => predicate(x).GetAwaiter().GetResult()));
		await Test04((mbe, predicate) => mbe.AsTask().FilterAsync(predicate));
	}
}
