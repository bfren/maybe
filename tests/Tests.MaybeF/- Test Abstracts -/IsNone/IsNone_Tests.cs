// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts;

public abstract class IsNone_Tests
{
	public delegate bool IsNone<T>(Maybe<T> maybe, out IReason reason);

	public abstract void Test00_Is_None_Returns_True_Sets_Reason();

	protected static void Test00(IsNone<int> act)
	{
		// Arrange
		var inReason = Substitute.For<IReason>();
		var maybe = F.None<int>(inReason);

		// Act
		var result = act(maybe, out IReason outReason);

		// Assert
		Assert.True(result);
		Assert.Equal(inReason, outReason);
	}

	public abstract void Test01_Is_Not_None_Returns_False();

	protected static void Test01(IsNone<string> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Str);

		// Act
		var result = act(maybe, out IReason outReason);

		// Assert
		Assert.False(result);
		Assert.Null(outReason);
	}
}
