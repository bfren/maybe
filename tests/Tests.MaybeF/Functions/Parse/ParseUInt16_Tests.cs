// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseUInt16_Tests : Abstracts.Parse_Tests<ushort>
{
	public static TheoryData<string> Valid_Unsigned_Integer_Input() =>
		[
			"1",
			"  1  ",
			"1000"
		];

	public static TheoryData<string> Invalid_Unsigned_Integer_Input() =>
		[
			"",
			"Invalid",
			"1-",
			"(1)",
			"1.01",
			"£1",
			"£1.10",
			"1e4",
			"-1e4",
			"1e-4",
			"-1e-4",
			"1,000",
			"-1,000",
			"-1",
			"-1000"
		];

	public static TheoryData<string> Extreme_UShort_Input() =>
		[
			ushort.MinValue.ToString(),
			ushort.MaxValue.ToString()
		];

	[Theory]
	[MemberData(nameof(Valid_Unsigned_Integer_Input))]
	[MemberData(nameof(Extreme_UShort_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => ushort.Parse(s, F.DefaultCulture), F.ParseUInt16, F.ParseUInt16);
	}

	[Theory]
	[MemberData(nameof(Invalid_Unsigned_Integer_Input))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test01(input, F.ParseUInt16, F.ParseUInt16);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test02(input, F.ParseUInt16, F.ParseUInt16);
	}
}
