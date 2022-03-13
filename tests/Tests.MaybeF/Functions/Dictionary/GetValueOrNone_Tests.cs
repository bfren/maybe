// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Dictionary;

public class GetValueOrNone_Tests : Abstracts.Dictionary.GetValueOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_Dictionary_Returns_None_With_ListIsEmptyReason()
	{
		Test00((dict, key) => F.DictionaryF.GetValueOrNone(dict, key));
	}

	[Theory]
	[InlineData(null)]
	public override void Test01_Null_Key_Returns_None_With_KeyCannotBeNullReason(string input)
	{
		Test01(dict => F.DictionaryF.GetValueOrNone(dict, input));
	}

	[Fact]
	public override void Test02_Key_Does_Not_Exists_Returns_None_With_KeyDoesNotExistReason()
	{
		Test02((dict, key) => F.DictionaryF.GetValueOrNone(dict, key));
	}

	[Fact]
	public override void Test03_Key_Exists_Null_Item_Returns_None_With_NullValueReason()
	{
		Test03((dict, key) => F.DictionaryF.GetValueOrNone(dict, key));
	}

	[Fact]
	public override void Test04_Key_Exists_Valid_Item_Returns_Some_With_Value()
	{
		Test04((dict, key) => F.DictionaryF.GetValueOrNone(dict, key));
	}
}
