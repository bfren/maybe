// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseDecimal_Tests : Abstracts.Parse_Tests<decimal>
{
	public static IEnumerable<object[]> Extreme_Decimal_Input()
	{
		yield return new object[] { decimal.MinValue.ToString() };
		yield return new object[] { decimal.MaxValue.ToString() };
	}

	[Theory]
	[MemberData(nameof(ParseSingle_Tests.Valid_Float_Input), MemberType = typeof(ParseSingle_Tests))]
	[MemberData(nameof(Extreme_Decimal_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => decimal.Parse(s, F.DefaultCulture), F.ParseDecimal, F.ParseDecimal);
	}

	[Theory]
	[MemberData(nameof(ParseSingle_Tests.Invalid_Float_Input), MemberType = typeof(ParseSingle_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test01(input, F.ParseDecimal, F.ParseDecimal);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test02(input, F.ParseDecimal, F.ParseDecimal);
	}
}
