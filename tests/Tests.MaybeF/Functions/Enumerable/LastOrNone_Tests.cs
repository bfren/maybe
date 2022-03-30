﻿// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Enumerable;

public class LastOrNone_Tests : Abstracts.Enumerable.LastOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg()
	{
		Test00(list => F.EnumerableF.LastOrNone(list, null));
	}

	[Fact]
	public override void Test01_No_Matching_Items_Returns_None_With_LastItemIsNullMsg()
	{
		Test01((list, predicate) => F.EnumerableF.LastOrNone(list, predicate));
	}

	[Fact]
	public override void Test02_Returns_Last_Element()
	{
		Test02(list => F.EnumerableF.LastOrNone(list, null));
	}

	[Fact]
	public override void Test03_Returns_Last_Matching_Element()
	{
		Test03((list, predicate) => F.EnumerableF.LastOrNone(list, predicate));
	}
}
