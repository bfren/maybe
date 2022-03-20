// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseByte_Tests : Abstracts.Parse_Tests<byte>
{
	public static IEnumerable<object[]> Valid_Byte_Input()
	{
		yield return new object[] { "101" };
		yield return new object[] { "  101  " };
		yield return new object[] { "+101" };
		yield return new object[] { "00000000101" };
	}

	public static IEnumerable<object[]> Negative_Byte_Input()
	{
		yield return new object[] { "-101" };
		yield return new object[] { "-00000000101" };

	}

	public static IEnumerable<object[]> Invalid_Byte_Input()
	{
		yield return new object[] { "" };
		yield return new object[] { "1024" };
		yield return new object[] { "100.1" };
		yield return new object[] { "FF" };
		yield return new object[] { "0x1F" };
	}

	[Theory]
	[MemberData(nameof(Valid_Byte_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => byte.Parse(s, F.DefaultCulture), F.ParseByte, F.ParseByte);
	}

	[Theory]
	[MemberData(nameof(Invalid_Byte_Input))]
	[MemberData(nameof(Negative_Byte_Input))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test01(input, F.ParseByte, F.ParseByte);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test02(input, F.ParseByte, F.ParseByte);
	}
}
