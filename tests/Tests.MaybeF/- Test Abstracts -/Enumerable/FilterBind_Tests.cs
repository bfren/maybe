// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts.Enumerable;

public abstract class FilterBind_Tests
{
	public abstract void Test00_Binds_And_Returns_Only_Some_From_List();

	protected static void Test00(Func<IEnumerable<Maybe<int>>, Func<int, Maybe<string>>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0);
		var o1 = F.Some(v1);
		var o2 = Create.None<int>();
		var o3 = Create.None<int>();
		var list = new[] { o0, o1, o2, o3 };
		var bind = Substitute.For<Func<int, Maybe<string>>>();
		bind.Invoke(Arg.Any<int>()).Returns(x => F.Some(x.ArgAt<int>(0).ToString()));

		// Act
		var result = act(list, bind);

		// Assert
		Assert.Collection(result,
			x =>
			{
				var s0 = x.AssertSome();
				Assert.Equal(v0.ToString(), s0);
			},
			x =>
			{
				var s1 = x.AssertSome();
				Assert.Equal(v1.ToString(), s1);
			}
		);
		bind.ReceivedWithAnyArgs(2).Invoke(Arg.Any<int>());
	}

	public abstract void Test01_Returns_Matching_Some_From_List();

	protected static void Test01(Func<IEnumerable<Maybe<int>>, Func<int, Maybe<string>>, Func<int, bool>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Int;
		var o0 = F.Some(v0);
		var o1 = F.Some(v1);
		var o2 = Create.None<int>();
		var o3 = Create.None<int>();
		var list = new[] { o0, o1, o2, o3 };

		var bind = Substitute.For<Func<int, Maybe<string>>>();
		bind.Invoke(Arg.Any<int>()).Returns(x => F.Some(x.ArgAt<int>(0).ToString()));

		var predicate = Substitute.For<Func<int, bool>>();
		predicate.Invoke(v1).Returns(true);

		// Act
		var r0 = act(list, bind, predicate);

		// Assert
		Assert.Collection(r0,
			x => Assert.Equal(v1.ToString(), x)
		);
		bind.ReceivedWithAnyArgs(1).Invoke(Arg.Any<int>());
	}

	public abstract void Test02_List_Null_Returns_Empty_List();

	protected static void Test02(Func<IEnumerable<Maybe<int>>, Func<int, Maybe<string>>, Func<int, bool>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var bind = Substitute.For<Func<int, Maybe<string>>>();
		var predicate = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(null!, bind, predicate);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}

	public abstract void Test03_Bind_Null_Returns_Empty_List();

	protected static void Test03(Func<IEnumerable<Maybe<int>>, Func<int, Maybe<string>>, Func<int, bool>, IEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var list = new[] { F.Some(Rnd.Int), F.Some(Rnd.Int) };
		var predicate = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(list, null!, predicate);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}
}
