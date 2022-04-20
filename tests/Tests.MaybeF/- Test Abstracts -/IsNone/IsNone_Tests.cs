// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts;

public abstract class IsNone_Tests
{
	public delegate bool IsNone<T>(Maybe<T> maybe, out IMsg reason);

	public abstract void Test00_Is_None_Returns_True_Sets_Msg();

	protected static void Test00(IsNone<int> act)
	{
		// Arrange
		var inMsg = Substitute.For<IMsg>();
		var maybe = F.None<int>(inMsg);

		// Act
		var result = act(maybe, out var reason);

		// Assert
		Assert.True(result);
		Assert.Equal(inMsg, reason);
	}

	public abstract void Test01_Is_Not_None_Returns_False();

	protected static void Test01(IsNone<string> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Str);

		// Act
		var result = act(maybe, out var reason);

		// Assert
		Assert.False(result);
		Assert.Null(reason);
	}

	public abstract void Test02_Is_Null_Returns_False();

	protected static void Test02(IsNone<Guid> act)
	{
		// Arrange

		// Act
		var result = act(null!, out var reason);

		// Assert
		Assert.False(result);
		Assert.Null(reason);
	}
}
