// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseTimeOnly_Tests : Abstracts.Parse_Tests<TimeOnly>
{
	public static IEnumerable<object[]> Valid_TimeOnly_Input()
	{
		yield return new object[] { "13:59" };
		yield return new object[] { "1:59 PM" };
		yield return new object[] { "13:59 PM" };
		yield return new object[] { "13:59:59" };
		yield return new object[] { "1:59:59 PM" };
		yield return new object[] { "1.59.59 PM" };
	}

	public static IEnumerable<object[]> Invalid_TimeOnly_Input()
	{
		yield return new object[] { "Invalid" };
		yield return new object[] { "1:59.59 PM" };
		yield return new object[] { "24:59" };
		yield return new object[] { "13:79" };
		yield return new object[] { "1:79 PM" };
	}

	[Theory]
	[MemberData(nameof(Valid_TimeOnly_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => TimeOnly.Parse(s, F.DefaultCulture), F.ParseTimeOnly, F.ParseTimeOnly);
	}

	[Theory]
	[MemberData(nameof(Invalid_TimeOnly_Input))]
	[MemberData(nameof(ParseDateTime_Tests.Invalid_DateTime_Input), MemberType = typeof(ParseDateTime_Tests))]
	[MemberData(nameof(ParseDateTime_Tests.Valid_DateTime_Input), MemberType = typeof(ParseDateTime_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test01(input, F.ParseTimeOnly, F.ParseTimeOnly);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test02(input, F.ParseTimeOnly, F.ParseTimeOnly);
	}
}
