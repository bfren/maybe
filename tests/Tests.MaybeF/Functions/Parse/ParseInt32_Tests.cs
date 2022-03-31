// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseInt32_Tests : Abstracts.Parse_Tests<int>
{
	public static IEnumerable<object[]> Extreme_Int_Input()
	{
		yield return new object[] { int.MinValue.ToString() };
		yield return new object[] { int.MaxValue.ToString() };
	}

	[Theory]
	[MemberData(nameof(ParseInt16_Tests.Valid_Integer_Input), MemberType = typeof(ParseInt16_Tests))]
	[MemberData(nameof(Extreme_Int_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => int.Parse(s, F.DefaultCulture), F.ParseInt32, F.ParseInt32);
	}

	[Theory]
	[MemberData(nameof(ParseInt16_Tests.Invalid_Integer_Input), MemberType = typeof(ParseInt16_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test01(input, F.ParseInt32, F.ParseInt32);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test02(input, F.ParseInt32, F.ParseInt32);
	}
}
