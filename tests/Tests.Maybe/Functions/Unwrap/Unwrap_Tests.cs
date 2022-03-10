// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using NSubstitute;
using Xunit;

namespace MaybeF.MaybeF_Tests;

public class Unwrap_Tests : Abstracts.Unwrap_Tests
{
	[Fact]
	public override void Test00_None_Runs_IfNone_Func_Returns_Value()
	{
		Test00((mbe, ifNone) => F.Unwrap(mbe, ifNone));
	}

	[Fact]
	public override void Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value()
	{
		Test01((mbe, ifNone) => F.Unwrap(mbe, ifNone));
	}

	[Fact]
	public override void Test02_Some_Returns_Value()
	{
		Test02(mbe => F.Unwrap(mbe, Substitute.For<Func<int>>()));
		Test02(mbe => F.Unwrap(mbe, Substitute.For<Func<IReason, int>>()));
	}
}
