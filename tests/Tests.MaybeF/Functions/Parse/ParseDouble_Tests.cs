// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseDouble_Tests : Abstracts.Parse_Tests<double>
{
	public static IEnumerable<object[]> Extreme_Double_Input()
	{
		yield return new object[] { double.MinValue.ToString() };
		yield return new object[] { double.MaxValue.ToString() };
	}

	[Theory]
	[MemberData(nameof(ParseSingle_Tests.Valid_Float_Input), MemberType = typeof(ParseSingle_Tests))]
	[MemberData(nameof(ParseSingle_Tests.Valid_Float_Exponential_Input), MemberType = typeof(ParseSingle_Tests))]
	[MemberData(nameof(Extreme_Double_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => double.Parse(s, F.DefaultCulture), F.ParseDouble, F.ParseDouble);
	}

	[Theory]
	[MemberData(nameof(ParseSingle_Tests.Invalid_Float_Input), MemberType = typeof(ParseSingle_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test01(input, F.ParseDouble, F.ParseDouble);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test02(input, F.ParseDouble, F.ParseDouble);
	}
}
