// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests.Dictionary;

public class GetValueOrNone_Tests : Abstracts.Dictionary.GetValueOrNone_Tests
{
	[Fact]
	public override void Test00_Empty_Dictionary_Returns_None_With_DictionaryIsEmptyMsg()
	{
		Test00(F.DictionaryF.GetValueOrNone);
	}

	[Theory]
	[InlineData(null)]
	public override void Test01_Null_Key_Returns_None_With_KeyCannotBeNullMsg(string input)
	{
		Test01(dict => F.DictionaryF.GetValueOrNone(dict, input));
	}

	[Fact]
	public override void Test02_Key_Does_Not_Exists_Returns_None_With_KeyDoesNotExistMsg()
	{
		Test02(F.DictionaryF.GetValueOrNone);
	}

	[Fact]
	public override void Test03_Key_Exists_Null_Item_Returns_None_With_NullValueMsg()
	{
		Test03(F.DictionaryF.GetValueOrNone);
	}

	[Fact]
	public override void Test04_Key_Exists_Valid_Item_Returns_Some_With_Value()
	{
		Test04(F.DictionaryF.GetValueOrNone);
	}

	[Fact]
	public override void Test05_Null_Dictionary_Returns_None_With_DictionaryIsEmptyMsg()
	{
		Test05(F.DictionaryF.GetValueOrNone);
	}
}
