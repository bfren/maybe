// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using static MaybeF.F.DictionaryF.M;

namespace Abstracts.Dictionary;

public abstract class GetValueOrNone_Tests
{
	public abstract void Test00_Empty_Dictionary_Returns_None_With_DictionaryIsEmptyMsg();

	protected static void Test00(Func<IDictionary<string, int>, string, Maybe<int>> act)
	{
		// Arrange
		var dictionary = new Dictionary<string, int>();

		// Act
		var result = act(dictionary, Rnd.Str);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<DictionaryIsEmptyMsg>(none);
	}

	public abstract void Test01_Null_Key_Returns_None_With_KeyCannotBeNullMsg(string input);

	protected static void Test01(Func<IDictionary<string, int>, Maybe<int>> act)
	{
		// Arrange
		var dictionary = new Dictionary<string, int>
		{
			{ Rnd.Str, Rnd.Int }
		};

		// Act
		var result = act(dictionary);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<KeyCannotBeNullMsg>(none);
	}

	public abstract void Test02_Key_Does_Not_Exists_Returns_None_With_KeyDoesNotExistMsg();

	protected static void Test02(Func<IDictionary<string, int>, string, Maybe<int>> act)
	{
		// Arrange
		var dictionary = new Dictionary<string, int>
		{
			{ Rnd.Str, Rnd.Int }
		};
		var key = Rnd.Str;

		// Act
		var result = act(dictionary, key);

		// Assert
		var none = result.AssertNone();
		var message = Assert.IsType<KeyDoesNotExistMsg<string>>(none);
		Assert.Equal(key, message.Key);
	}

	public abstract void Test03_Key_Exists_Null_Item_Returns_None_With_NullValueMsg();

	protected static void Test03(Func<IDictionary<int, string>, int, Maybe<string>> act)
	{
		// Arrange
		var key = Rnd.Int;
		var dictionary = new Dictionary<int, string>
		{
			{ key, null! }
		};

		// Act
		var result = act(dictionary, key);

		// Assert
		var none = result.AssertNone();
		var message = Assert.IsType<NullValueMsg<int>>(none);
		Assert.Equal(key, message.Key);
	}

	public abstract void Test04_Key_Exists_Valid_Item_Returns_Some_With_Value();

	protected static void Test04(Func<IDictionary<int, string>, int, Maybe<string>> act)
	{
		// Arrange
		var key = Rnd.Int;
		var value = Rnd.Str;
		var dictionary = new Dictionary<int, string>
		{
			{ key, value }
		};

		// Act
		var result = act(dictionary, key);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract void Test05_Null_Dictionary_Returns_None_With_DictionaryIsEmptyMsg();

	protected static void Test05(Func<IDictionary<string, int>, string, Maybe<int>> act)
	{
		// Arrange

		// Act
		var result = act(null!, Rnd.Str);

		// Assert
		result.AssertNone().AssertType<DictionaryIsEmptyMsg>();
	}
}
