// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts.Enumerable;

public abstract class Bind_Tests
{
	public abstract void Test00_Removes_None_Input_Items();

	protected static void Test00(Func<IEnumerable<Maybe<int>>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var i0 = F.Some(Rnd.Int);
		var i1 = F.Some(Rnd.Int);
		var i2 = Create.None<int>();
		var list = new[] { i0, i1, i2 };
		var bind = Substitute.For<Func<int, Maybe<string>>>();
		bind.Invoke(default).ReturnsForAnyArgs(x => x.Arg<int>().ToString());

		// Act
		var result = act(list, bind);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.ToString(), x.AssertSome()),
			x => Assert.Equal(i1.ToString(), x.AssertSome())
		);
	}

	public abstract void Test01_Removes_None_Bound_Items();

	protected static void Test01(Func<IEnumerable<Maybe<int>>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var i0 = F.Some(Rnd.Int);
		var i1 = F.Some(Rnd.Int);
		var i2 = F.Some(Rnd.Int);
		var list = new[] { i0, i1, i2 };
		var bind = Substitute.For<Func<int, Maybe<string>>>();
		bind.Invoke(default).ReturnsForAnyArgs(x => x.Arg<int>() switch
		{
			int y when i2.IsSome(out var z) && y == z =>
				Create.None<string>(),

			int y =>
				y.ToString()
		});

		// Act
		var result = act(list, bind);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.ToString(), x.AssertSome()),
			x => Assert.Equal(i1.ToString(), x.AssertSome())
		);
	}

	public abstract void Test02_Binds_Each_Item();

	protected static void Test02(Func<IEnumerable<Maybe<int>>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var i0 = F.Some(Rnd.Int);
		var i1 = F.Some(Rnd.Int);
		var i2 = F.Some(Rnd.Int);
		var list = new[] { i0, i1, i2 };
		var bind = Substitute.For<Func<int, Maybe<string>>>();
		bind.Invoke(default).ReturnsForAnyArgs(x => x.Arg<int>().ToString());

		// Act
		var result = act(list, bind);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.ToString(), x.AssertSome()),
			x => Assert.Equal(i1.ToString(), x.AssertSome()),
			x => Assert.Equal(i2.ToString(), x.AssertSome())
		);
	}

	public abstract void Test03_List_Null_Returns_Empty_List();

	protected static void Test03(Func<IEnumerable<Maybe<int>>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var bind = Substitute.For<Func<int, Maybe<string>>>();

		// Act
		var result = act(null!, bind);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}

	public abstract void Test04_Map_Null_Returns_Empty_List();

	protected static void Test04(Func<IEnumerable<Maybe<int>>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var list = new[] { F.Some(Rnd.Int), F.Some(Rnd.Int), F.Some(Rnd.Int) };

		// Act
		var result = act(list, null!);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}
}
