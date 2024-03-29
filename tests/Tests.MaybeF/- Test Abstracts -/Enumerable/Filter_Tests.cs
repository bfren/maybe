// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts.Enumerable;

public abstract class Filter_Tests
{
	public abstract void Test00_Maps_And_Returns_Only_Some_From_List();

	protected static void Test00(Func<IEnumerable<Maybe<int>>, IEnumerable<int>> act)
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0);
		var o1 = F.Some(v1);
		var o2 = Create.None<int>();
		var o3 = Create.None<int>();
		var list = new[] { o0, o1, o2, o3 };

		// Act
		var result = act(list);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(v0, x),
			x => Assert.Equal(v1, x)
		);
	}

	public abstract void Test01_Maps_And_Returns_Matching_Some_From_List();

	protected static void Test01(Func<IEnumerable<Maybe<int>>, Func<int, bool>, IEnumerable<int>> act)
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0);
		var o1 = F.Some(v1);
		var o2 = Create.None<int>();
		var o3 = Create.None<int>();
		var list = new[] { o0, o1, o2, o3 };
		var predicate = Substitute.For<Func<int, bool>>();
		predicate.Invoke(v1).Returns(true);

		// Act
		var result = act(list, predicate);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(v1, x)
		);
	}

	public abstract void Test02_Null_Input_Returns_Empty_List();

	protected static void Test02(Func<IEnumerable<Maybe<int>>, Func<int, bool>, IEnumerable<int>> act)
	{
		// Arrange
		var predicate = Substitute.For<Func<int, bool>>();

		// Act
		var r0 = act(null!, null!);
		var r1 = act(null!, predicate);

		// Assert
		Assert.NotNull(r0);
		Assert.Empty(r0);
		Assert.NotNull(r1);
		Assert.Empty(r1);
	}
}
