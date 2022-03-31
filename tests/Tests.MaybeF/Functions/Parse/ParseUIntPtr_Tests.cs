// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseUIntPtr_Tests : Abstracts.Parse_Tests<nuint>
{
	public static IEnumerable<object[]> Extreme_Int_Input()
	{
		yield return new object[] { nuint.MinValue.ToString() };
		yield return new object[] { nuint.MaxValue.ToString() };
	}

	[Theory]
	[MemberData(nameof(ParseUInt16_Tests.Valid_Unsigned_Integer_Input), MemberType = typeof(ParseUInt16_Tests))]
	[MemberData(nameof(Extreme_Int_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => nuint.Parse(s, F.DefaultCulture), F.ParseUIntPtr, F.ParseUIntPtr);
	}

	[Theory]
	[MemberData(nameof(ParseUInt16_Tests.Invalid_Unsigned_Integer_Input), MemberType = typeof(ParseUInt16_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test01(input, F.ParseUIntPtr, F.ParseUIntPtr);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test02(input, F.ParseUIntPtr, F.ParseUIntPtr);
	}
}
