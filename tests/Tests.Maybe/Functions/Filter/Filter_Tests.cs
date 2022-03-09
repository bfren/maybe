﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using NSubstitute;
using Xunit;

namespace Maybe.Functions.MaybeF_Tests;

public class Filter_Tests : Tests.Maybe.Abstracts.Filter_Tests
{
	[Fact]
	public override void Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason()
	{
		var predicate = Substitute.For<Func<int, bool>>();
		Test00(mbe => MaybeF.Filter(mbe, predicate));
	}

	[Fact]
	public override void Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionReason()
	{
		Test01((mbe, predicate) => MaybeF.Filter(mbe, predicate));
	}

	[Fact]
	public override void Test02_When_Some_And_Predicate_True_Returns_Value()
	{
		Test02((mbe, predicate) => MaybeF.Filter(mbe, predicate));
	}

	[Fact]
	public override void Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseReason()
	{
		Test03((mbe, predicate) => MaybeF.Filter(mbe, predicate));
	}

	[Fact]
	public override void Test04_When_None_Returns_None_With_Original_Reason()
	{
		Test04((mbe, predicate) => MaybeF.Filter(mbe, predicate));
	}
}
