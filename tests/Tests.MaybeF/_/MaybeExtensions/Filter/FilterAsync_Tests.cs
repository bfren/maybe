﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class FilterAsync_Tests : Abstracts.FilterAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason()
	{
		var syncPredicate = Substitute.For<Func<int, bool>>();
		var asyncPredicate = Substitute.For<Func<int, Task<bool>>>();

		await Test00(mbe => mbe.AsTask.FilterAsync(syncPredicate)).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.FilterAsync(asyncPredicate)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionReason()
	{
		await Test01((mbe, predicate) => mbe.AsTask.FilterAsync(x => predicate(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, predicate) => mbe.AsTask.FilterAsync(predicate)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_When_Some_And_Predicate_True_Returns_Value()
	{
		await Test02((mbe, predicate) => mbe.AsTask.FilterAsync(x => predicate(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test02((mbe, predicate) => mbe.AsTask.FilterAsync(predicate)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseReason()
	{
		await Test03((mbe, predicate) => mbe.AsTask.FilterAsync(x => predicate(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test03((mbe, predicate) => mbe.AsTask.FilterAsync(predicate)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_When_None_Returns_None_With_Original_Reason()
	{
		await Test04((mbe, predicate) => mbe.AsTask.FilterAsync(x => predicate(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test04((mbe, predicate) => mbe.AsTask.FilterAsync(predicate)).ConfigureAwait(false);
	}
}