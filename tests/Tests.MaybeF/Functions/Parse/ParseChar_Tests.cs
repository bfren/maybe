// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using static MaybeF.F.M;

namespace MaybeF.Functions.Parse_Tests;

public class ParseChar_Tests : Abstracts.Parse_Tests<char>
{
	[Theory]
	[InlineData("a")]
	[InlineData("1")]
	public override void Test00_Valid_Input_Returns_Parsed_Result(string? input)
	{
		// Arrange
		var expected = char.Parse(input ?? string.Empty);

		// Act
		var result = F.ParseChar(input);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(expected, some);
	}

	[Theory]
	[InlineData("true")]
	public override void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		// Arrange

		// Act
		var result = F.ParseChar(input);

		// Assert
		var none = result.AssertNone();
		var message = Assert.IsType<UnableToParseValueAsMsg>(none);
		Assert.Equal(typeof(char), message.Type);
		Assert.Equal(input, message.Value);
	}

	[Theory]
	[InlineData(null)]
	public override void Test02_Null_Input_Returns_None_With_UnableToParseValueAsMsg(string? input)
	{
		// Arrange

		// Act
		var result = F.ParseChar(input);

		// Assert
		var none = result.AssertNone();
		var message = Assert.IsType<UnableToParseValueAsMsg>(none);
		Assert.Equal(typeof(char), message.Type);
		Assert.Empty(message.Value);
	}
}
