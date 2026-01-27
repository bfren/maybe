// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseDateTime_Tests : Abstracts.Parse_Tests<DateTime>
{
	public static TheoryData<string> Valid_DateTime_Input() =>
		[
			"16/5/2009 14:57:32.8",
			"2009-05-16 14:57:32.8",
			"2009-05-16T14:57:32.8375298-04:00",
			"16/5/2008 14:57:32.80 -07:00",
			"16 May 2008 2:57:32.8 PM",
			"16-05-2009 1:00:32 PM",
			"Sat, 16 May 2009 20:10:57 GMT"
		];

	public static TheoryData<string> Invalid_DateTime_Input() =>
		[
			"Invalid",
			"5/16/2009 14:57:32.8",
			"2009-16-05 14:57:32.8",
			"2009-16-05T14:57:32.8375298-04:00",
			"5/16/2008 14:57:32.80 -07:00",
			"05-16-2009 1:00:32 PM",
			"Fri, 16 May 2009 20:10:57 GMT"
		];

	[Theory]
	[MemberData(nameof(ParseDateOnly_Tests.Valid_DateOnly_Input), MemberType = typeof(ParseDateOnly_Tests))]
	[MemberData(nameof(Valid_DateTime_Input))]
	[MemberData(nameof(ParseTimeOnly_Tests.Valid_TimeOnly_Input), MemberType = typeof(ParseTimeOnly_Tests))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => DateTime.Parse(s, F.DefaultCulture), F.ParseDateTime, F.ParseDateTime);
	}

	[Theory]
	[MemberData(nameof(ParseDateOnly_Tests.Invalid_DateOnly_Input), MemberType = typeof(ParseDateOnly_Tests))]
	[MemberData(nameof(Invalid_DateTime_Input))]
	[MemberData(nameof(ParseTimeOnly_Tests.Invalid_TimeOnly_Input), MemberType = typeof(ParseTimeOnly_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test01(input, F.ParseDateTime, F.ParseDateTime);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		Test02(input, F.ParseDateTime, F.ParseDateTime);
	}
}
