// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseBool_Tests : Abstracts.Parse_Tests<bool>
{
	[Theory]
	[InlineData("false", false)]
	[InlineData("true", true)]
	[InlineData("False", false)]
	[InlineData("True", true)]
	[InlineData("FALSE", false)]
	[InlineData("TRUE", true)]
	[InlineData("fAlSe", false)]
	[InlineData("tRuE", true)]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string input, bool expected)
	{
		Test00(input, expected, F.ParseBool, F.ParseBool);
	}

	[Theory]
	[InlineData("0")]
	[InlineData("1")]
	[InlineData("no")]
	[InlineData("yes")]
	[InlineData("Invalid")]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string input)
	{
		Test01(input, F.ParseBool, F.ParseBool);
	}
}
