// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts.Enumerable;

public abstract class BindAsync_Tests
{
	public abstract Task Test00_Removes_None_Input_Items();

	protected static async Task Test00(Func<IEnumerable<Maybe<int>>, Func<int, Task<Maybe<string>>>, IAsyncEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var i0 = F.Some(Rnd.Int);
		var i1 = F.Some(Rnd.Int);
		var i2 = Create.None<int>();
		var list = new[] { i0, i1, i2 };
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();
		bind.Invoke(default).ReturnsForAnyArgs(x => x.Arg<int>().ToString());

		// Act
		List<Maybe<string>> result = new();
		await foreach (var item in act(list, bind))
		{
			result.Add(item);
		}

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.ToString(), x.AssertSome()),
			x => Assert.Equal(i1.ToString(), x.AssertSome())
		);
	}

	public abstract Task Test01_Binds_Each_Item();

	protected static async Task Test01(Func<IEnumerable<Maybe<int>>, Func<int, Task<Maybe<string>>>, IAsyncEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var i0 = F.Some(Rnd.Int);
		var i1 = F.Some(Rnd.Int);
		var i2 = F.Some(Rnd.Int);
		var list = new[] { i0, i1, i2 };
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();
		bind.Invoke(default).ReturnsForAnyArgs(x => x.Arg<int>().ToString());

		// Act
		List<Maybe<string>> result = new();
		await foreach (var item in act(list, bind))
		{
			result.Add(item);
		}

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.ToString(), x.AssertSome()),
			x => Assert.Equal(i1.ToString(), x.AssertSome()),
			x => Assert.Equal(i2.ToString(), x.AssertSome())
		);
	}

	public abstract Task Test02_List_Null_Returns_Empty_List();

	protected static async Task Test02(Func<IEnumerable<Maybe<int>>, Func<int, Task<Maybe<string>>>, IAsyncEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var bind = Substitute.For<Func<int, Task<Maybe<string>>>>();

		// Act
		List<Maybe<string>> result = new();
		await foreach (var item in act(null!, bind))
		{
			result.Add(item);
		}

		// Assert
		Assert.Empty(result);
	}

	public abstract Task Test03_Map_Null_Returns_Empty_List();

	protected static async Task Test03(Func<IEnumerable<Maybe<int>>, Func<int, Task<Maybe<string>>>, IAsyncEnumerable<Maybe<string>>> act)
	{
		// Arrange
		var list = new[] { F.Some(Rnd.Int), F.Some(Rnd.Int), F.Some(Rnd.Int) };

		// Act
		List<Maybe<string>> result = new();
		await foreach (var item in act(list, null!))
		{
			result.Add(item);
		}

		// Assert
		Assert.Empty(result);
	}
}
