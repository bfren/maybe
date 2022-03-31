// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseUInt64_Tests : Abstracts.Parse_Tests<ulong>
{
	public static IEnumerable<object[]> Extreme_Int_Input()
	{
		yield return new object[] { ulong.MinValue.ToString() };
		yield return new object[] { ulong.MaxValue.ToString() };
	}

	[Theory]
	[MemberData(nameof(ParseUInt16_Tests.Valid_Unsigned_Integer_Input), MemberType = typeof(ParseUInt16_Tests))]
	[MemberData(nameof(Extreme_Int_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => ulong.Parse(s, F.DefaultCulture), F.ParseUInt64, F.ParseUInt64);
	}

	[Theory]
	[MemberData(nameof(ParseUInt16_Tests.Invalid_Unsigned_Integer_Input), MemberType = typeof(ParseUInt16_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test01(input, F.ParseUInt64, F.ParseUInt64);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test02(input, F.ParseUInt64, F.ParseUInt64);
	}
}
