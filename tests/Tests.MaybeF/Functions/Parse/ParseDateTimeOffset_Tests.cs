// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseDateTimeOffset_Tests : Abstracts.Parse_Tests<DateTimeOffset>
{
	[Theory]
	[MemberData(nameof(ParseDateOnly_Tests.Valid_DateOnly_Input), MemberType = typeof(ParseDateOnly_Tests))]
	[MemberData(nameof(ParseDateTime_Tests.Valid_DateTime_Input), MemberType = typeof(ParseDateTime_Tests))]
	[MemberData(nameof(ParseTimeOnly_Tests.Valid_TimeOnly_Input), MemberType = typeof(ParseTimeOnly_Tests))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => DateTimeOffset.Parse(s, F.DefaultCulture), F.ParseDateTimeOffset, F.ParseDateTimeOffset);
	}

	[Theory]
	[MemberData(nameof(ParseDateOnly_Tests.Invalid_DateOnly_Input), MemberType = typeof(ParseDateOnly_Tests))]
	[MemberData(nameof(ParseDateTime_Tests.Invalid_DateTime_Input), MemberType = typeof(ParseDateTime_Tests))]
	[MemberData(nameof(ParseTimeOnly_Tests.Invalid_TimeOnly_Input), MemberType = typeof(ParseTimeOnly_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test01(input, F.ParseDateTimeOffset, F.ParseDateTimeOffset);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test02(input, F.ParseDateTimeOffset, F.ParseDateTimeOffset);
	}
}
