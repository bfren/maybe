// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseIntPtr_Tests : Abstracts.Parse_Tests<nint>
{
	public static IEnumerable<object[]> Extreme_Long_Input()
	{
		yield return new object[] { nint.MinValue.ToString() };
		yield return new object[] { nint.MaxValue.ToString() };
	}

	[Theory]
	[MemberData(nameof(ParseInt16_Tests.Valid_Integer_Input), MemberType = typeof(ParseInt16_Tests))]
	[MemberData(nameof(Extreme_Long_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string input)
	{
		Test00(input, s => nint.Parse(s, F.DefaultCulture), F.ParseIntPtr, F.ParseIntPtr);
	}

	[Theory]
	[MemberData(nameof(ParseInt16_Tests.Invalid_Integer_Input), MemberType = typeof(ParseInt16_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string input)
	{
		Test01(input, F.ParseIntPtr, F.ParseIntPtr);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsReason(string input)
	{
		Test02(input, F.ParseIntPtr, F.ParseIntPtr);
	}
}
