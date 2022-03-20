// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class ParseDateOnly_Tests : Abstracts.Parse_Tests<DateOnly>
{
	public static IEnumerable<object[]> Valid_DateOnly_Input()
	{
		yield return new object[] { "16/5/2009" };
		yield return new object[] { "2009-05-16" };
		yield return new object[] { "16 May 2009" };
		yield return new object[] { "Sat, 16 May 2009" };
	}

	public static IEnumerable<object[]> Invalid_DateOnly_Input()
	{
		yield return new object[] { "Invalid" };
		yield return new object[] { "5/16/2009" };
		yield return new object[] { "2009-16-5" };
		yield return new object[] { "32/5/2009" };
		yield return new object[] { "2009-5-32" };
		yield return new object[] { "16/13/2009" };
		yield return new object[] { "2009-13-16" };
		yield return new object[] { "16/5/10000" };
		yield return new object[] { "10000-5-16" };
		yield return new object[] { "32 May 2009" };
		yield return new object[] { "Fri, 16 May 2009" };
	}

	[Theory]
	[MemberData(nameof(Valid_DateOnly_Input))]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		Test00(input, s => DateOnly.Parse(s, F.DefaultCulture), F.ParseDateOnly, F.ParseDateOnly);
	}

	[Theory]
	[MemberData(nameof(Invalid_DateOnly_Input))]
	[MemberData(nameof(ParseDateTime_Tests.Invalid_DateTime_Input), MemberType = typeof(ParseDateTime_Tests))]
	[MemberData(nameof(ParseDateTime_Tests.Valid_DateTime_Input), MemberType = typeof(ParseDateTime_Tests))]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test01(input, F.ParseDateOnly, F.ParseDateOnly);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsReason(string? input)
	{
		Test02(input, F.ParseDateOnly, F.ParseDateOnly);
	}
}
