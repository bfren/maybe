// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts.Enumerable;

public abstract class Map_Tests
{
	public abstract void Test00_Without_Map_Converts_Each_Item();

	protected static void Test00(Func<IEnumerable<int>, IEnumerable<Maybe<int>>> act)
	{
		// Arrange
		var i0 = Rnd.Int;
		var i1 = Rnd.Int;
		var i2 = Rnd.Int;
		var list = new[] { i0, i1, i2 };

		// Act
		var result = act(list);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0, x.AssertSome()),
			x => Assert.Equal(i1, x.AssertSome()),
			x => Assert.Equal(i2, x.AssertSome())
		);
	}

	public abstract void Test01_Without_Map_Removes_Nullable_Items();

	protected static void Test01(Func<IEnumerable<int?>, IEnumerable<Maybe<int?>>> act)
	{
		// Arrange
		var i0 = Rnd.Int;
		var i1 = Rnd.Int;
		var i2 = Rnd.Int;
		var list = new int?[] { i0, null, i1, null, i2 };

		// Act
		var result = act(list);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0, x.AssertSome()),
			x => Assert.Equal(i1, x.AssertSome()),
			x => Assert.Equal(i2, x.AssertSome())
		);
	}

	public abstract void Test02_With_Map_Converts_Each_Item();

	protected static void Test02(Func<IEnumerable<int>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var i0 = Rnd.Int;
		var i1 = Rnd.Int;
		var i2 = Rnd.Int;
		var list = new[] { i0, i1, i2 };
		var map = Substitute.For<Func<int, Maybe<string>>>();
		map.Invoke(default).ReturnsForAnyArgs(x => x[0].ToString());

		// Act
		var result = act(list, map);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.ToString(), x.AssertSome()),
			x => Assert.Equal(i1.ToString(), x.AssertSome()),
			x => Assert.Equal(i2.ToString(), x.AssertSome())
		);
	}

	public abstract void Test03_With_Map_Removes_Nullable_Items();

	protected static void Test03(Func<IEnumerable<int?>, Func<int?, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var i0 = Rnd.Int;
		var i1 = Rnd.Int;
		var i2 = Rnd.Int;
		var list = new int?[] { i0, null, i1, null, i2 };
		var map = Substitute.For<Func<int?, Maybe<string>>>();
		map.Invoke(default).ReturnsForAnyArgs(x => x[0].ToString());

		// Act
		var result = act(list, map);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.ToString(), x.AssertSome()),
			x => Assert.Equal(i1.ToString(), x.AssertSome()),
			x => Assert.Equal(i2.ToString(), x.AssertSome())
		);
	}

	public abstract void Test04_With_Map_Removes_None_Values();

	protected static void Test04(Func<IEnumerable<int>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var i0 = Rnd.Int;
		var i1 = Rnd.Int;
		var i2 = Rnd.Int;
		var list = new[] { i0, i1, i2 };
		var map = Substitute.For<Func<int, Maybe<string>>>();
		map.Invoke(default).ReturnsForAnyArgs(x => x.Arg<int>() switch
		{
			int y when y == i2 =>
				Create.None<string>(),

			int y =>
				y.ToString()
		});

		// Act
		var result = act(list, map);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.ToString(), x.AssertSome()),
			x => Assert.Equal(i1.ToString(), x.AssertSome())
		);
	}

	public abstract void Test05_List_Null_Returns_Empty_List();

	protected static void Test05(Func<IEnumerable<int>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var map = Substitute.For<Func<int, Maybe<string>>>();

		// Act
		var result = act(null!, map);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}

	public abstract void Test06_Map_Null_Returns_Empty_List();

	protected static void Test06(Func<IEnumerable<int>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var list = new[] { Rnd.Int, Rnd.Int, Rnd.Int };

		// Act
		var result = act(list, null!);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}
}
