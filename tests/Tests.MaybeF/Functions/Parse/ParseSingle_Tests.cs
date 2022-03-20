// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseSingle_Tests : Abstracts.Parse_Tests<float>
{
	public static IEnumerable<object[]> Valid_Float_Input()
	{
		yield return new object[] { "1" };
		yield return new object[] { "  1  " };
		yield return new object[] { "-1" };
		yield return new object[] { "1.01" };
		yield return new object[] { "-1.01" };
		yield return new object[] { "1,01" };
		yield return new object[] { "-1,01" };
		yield return new object[] { "1,000" };
		yield return new object[] { "-1,000" };
		yield return new object[] { "1,000.01" };
		yield return new object[] { "-1,000.01" };
	}
	public static IEnumerable<object[]> Valid_Float_Exponential_Input()
	{
		yield return new object[] { "1e10" };
		yield return new object[] { "-1e10" };
		yield return new object[] { "1e-10" };
		yield return new object[] { "-1e-10" };
	}

	public static IEnumerable<object[]> Invalid_Float_Input()
	{
		yield return new object[] { "" };
		yield return new object[] { "Invalid" };
		yield return new object[] { "1-" };
		yield return new object[] { "(1)" };
		yield return new object[] { "1.00.1" };
		yield return new object[] { "£1" };
		yield return new object[] { "£1.10" };
	}

	public static IEnumerable<object[]> Extreme_Single_Input()
	{
		yield return new object[] { float.MinValue.ToString() };
		yield return new object[] { float.MaxValue.ToString() };
	}

	[Theory]
	[MemberData(nameof(Valid_Float_Input))]
	[MemberData(nameof(Valid_Float_Exponential_Input))]
	[MemberData(nameof(Extreme_Single_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string input)
	{
		Test00(input, s => float.Parse(s, F.DefaultCulture), F.ParseSingle, F.ParseSingle);
	}

	[Theory]
	[MemberData(nameof(Invalid_Float_Input))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string input)
	{
		Test01(input, F.ParseSingle, F.ParseSingle);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsReason(string input)
	{
		Test02(input, F.ParseSingle, F.ParseSingle);
	}
}
