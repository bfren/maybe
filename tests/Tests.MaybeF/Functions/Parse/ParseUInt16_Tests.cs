// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseUInt16_Tests : Abstracts.Parse_Tests<ushort>
{
	public static IEnumerable<object[]> Valid_Unsigned_Integer_Input()
	{
		yield return new object[] { "1" };
		yield return new object[] { "  1  " };
		yield return new object[] { "1000" };
	}

	public static IEnumerable<object[]> Invalid_Unsigned_Integer_Input()
	{
		yield return new object[] { "" };
		yield return new object[] { "Invalid" };
		yield return new object[] { "1-" };
		yield return new object[] { "(1)" };
		yield return new object[] { "1.01" };
		yield return new object[] { "£1" };
		yield return new object[] { "£1.10" };
		yield return new object[] { "1e4" };
		yield return new object[] { "-1e4" };
		yield return new object[] { "1e-4" };
		yield return new object[] { "-1e-4" };
		yield return new object[] { "1,000" };
		yield return new object[] { "-1,000" };
		yield return new object[] { "-1" };
		yield return new object[] { "-1000" };
	}

	public static IEnumerable<object[]> Extreme_UShort_Input()
	{
		yield return new object[] { ushort.MinValue.ToString() };
		yield return new object[] { ushort.MaxValue.ToString() };
	}

	[Theory]
	[MemberData(nameof(Valid_Unsigned_Integer_Input))]
	[MemberData(nameof(Extreme_UShort_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => ushort.Parse(s, F.DefaultCulture), F.ParseUInt16, F.ParseUInt16);
	}

	[Theory]
	[MemberData(nameof(Invalid_Unsigned_Integer_Input))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test01(input, F.ParseUInt16, F.ParseUInt16);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test02(input, F.ParseUInt16, F.ParseUInt16);
	}
}
