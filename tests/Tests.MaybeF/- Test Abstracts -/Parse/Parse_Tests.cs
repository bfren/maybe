// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using static MaybeF.F.R;

namespace Abstracts;

public abstract class Parse_Tests<T>
{
	public delegate Maybe<T> ParseSpan(ReadOnlySpan<char> input);

	public delegate Maybe<T> ParseString(string input);

	public abstract void Test00_Valid_Input_Returns_Parsed_Result(string input);

	protected static void Test00(string input, T expected, ParseSpan parseSpan, ParseString parseString)
	{
		// Arrange

		// Act
		var r0 = parseSpan(input.AsSpan());
		var r1 = parseString(input);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(expected, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(expected, s1);
	}

	public abstract void Test01_Invalid_Input_Returns_None_With_UnableToParseValueAsReason(string input);

	protected static void Test01(string input, ParseSpan parseSpan, ParseString parseString)
	{
		// Arrange

		// Act
		var r0 = parseSpan(input.AsSpan());
		var r1 = parseString(input);

		// Assert
		var n0 = r0.AssertNone();
		var m0 = Assert.IsType<UnableToParseValueAsReason>(n0);
		Assert.Equal(typeof(T), m0.Type);
		Assert.Equal(input, m0.Value);
		var n1 = r1.AssertNone();
		var m1 = Assert.IsType<UnableToParseValueAsReason>(n1);
		Assert.Equal(typeof(T), m1.Type);
		Assert.Equal(input, m1.Value);
	}
}
