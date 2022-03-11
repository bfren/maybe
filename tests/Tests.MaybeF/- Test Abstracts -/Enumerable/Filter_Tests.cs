﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Testing;

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
		_ = predicate.Invoke(v1).Returns(true);

		// Act
		var result = act(list, predicate);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(v1, x)
		);
	}
}