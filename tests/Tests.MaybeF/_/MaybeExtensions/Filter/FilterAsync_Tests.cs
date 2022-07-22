// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.MaybeExtensions_Tests;

public class FilterAsync_Tests : Abstracts.FilterAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg()
	{
		var syncPredicate = Substitute.For<Func<int, bool>>();
		var taskPredicate = Substitute.For<Func<int, Task<bool>>>();
		var valueTaskPredicate = Substitute.For<Func<int, ValueTask<bool>>>();

		await Test00(
			mbe => mbe.AsTask().FilterAsync(syncPredicate),
			mbe => mbe.AsValueTask().FilterAsync(syncPredicate)
		);
		await Test00(
			mbe => mbe.AsTask().FilterAsync(taskPredicate),
			mbe => mbe.AsValueTask().FilterAsync(valueTaskPredicate)
		);
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test01(
			(mbe, predicate) => mbe.AsTask().FilterAsync(x => H.GetResult(predicate(x))),
			(mbe, predicate) => mbe.AsValueTask().FilterAsync(x => H.GetResult(predicate(x)))
		);
		await Test01(
			(mbe, predicate) => mbe.AsTask().FilterAsync(predicate),
			(mbe, predicate) => mbe.AsValueTask().FilterAsync(predicate)
		);
	}

	[Fact]
	public override async Task Test02_When_Some_And_Predicate_True_Returns_Value()
	{
		await Test02(
			(mbe, predicate) => mbe.AsTask().FilterAsync(x => H.GetResult(predicate(x))),
			(mbe, predicate) => mbe.AsValueTask().FilterAsync(x => H.GetResult(predicate(x)))
		);
		await Test02(
			(mbe, predicate) => mbe.AsTask().FilterAsync(predicate),
			(mbe, predicate) => mbe.AsValueTask().FilterAsync(predicate)
		);
	}

	[Fact]
	public override async Task Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
	{
		await Test03(
			(mbe, predicate) => mbe.AsTask().FilterAsync(x => H.GetResult(predicate(x))),
			(mbe, predicate) => mbe.AsValueTask().FilterAsync(x => H.GetResult(predicate(x)))
		);
		await Test03(
			(mbe, predicate) => mbe.AsTask().FilterAsync(predicate),
			(mbe, predicate) => mbe.AsValueTask().FilterAsync(predicate)
		);
	}

	[Fact]
	public override async Task Test04_When_None_Returns_None_With_Original_Msg()
	{
		await Test04(
			(mbe, predicate) => mbe.AsTask().FilterAsync(x => H.GetResult(predicate(x))),
			(mbe, predicate) => mbe.AsValueTask().FilterAsync(x => H.GetResult(predicate(x)))
		);
		await Test04(
			(mbe, predicate) => mbe.AsTask().FilterAsync(predicate),
			(mbe, predicate) => mbe.AsValueTask().FilterAsync(predicate)
		);
	}
}
