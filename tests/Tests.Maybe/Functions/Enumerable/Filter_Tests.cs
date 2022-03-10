﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Xunit;

namespace MaybeF.MaybeF_Tests.Enumerable;

public class Filter_Tests : Abstracts.Enumerable.Filter_Tests
{
	[Fact]
	public override void Test00_Maps_And_Returns_Only_Some_From_List()
	{
		Test00(list => F.EnumerableF.Filter(list, null));
	}

	[Fact]
	public override void Test01_Maps_And_Returns_Matching_Some_From_List()
	{
		Test01((list, predicate) => F.EnumerableF.Filter(list, predicate));
	}
}
