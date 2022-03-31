// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Functions.Parse_Tests;

public class Parse_Tests
{
	[Fact]
	public void Returns_Result_If_True()
	{
		// Arrange
		var valueInt = Rnd.Int;
		var valueSpan = Rnd.Str.AsSpan();
		var valueString = Rnd.Str;

		var tryParseSpan = new F.TryParseSpan<int>((ReadOnlySpan<char> _, out int result) =>
		{
			result = valueInt;
			return true;
		});
		var tryParseString = new F.TryParseString<int>((string? _, out int result) =>
		{
			result = valueInt;
			return true;
		});

		// Act
		var r0 = F.Parse(valueSpan, tryParseSpan);
		var r1 = F.Parse(valueString, tryParseString);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(valueInt, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(valueInt, s1);
	}

	[Fact]
	public void Returns_None_With_UnableToParseValueAs_If_False()
	{
		// Arrange
		var valueSpan = Rnd.Str.AsSpan();
		var valueString = Rnd.Str;

		var tryParseSpan = new F.TryParseSpan<int>((ReadOnlySpan<char> _, out int result) =>
		{
			result = Rnd.Int;
			return false;
		});
		var tryParseString = new F.TryParseString<int>((string? _, out int result) =>
		{
			result = Rnd.Int;
			return false;
		});

		// Act
		var r0 = F.Parse(valueSpan, tryParseSpan);
		var r1 = F.Parse(valueString, tryParseString);

		// Assert
		var n0 = r0.AssertNone();
		var m0 = Assert.IsType<F.M.UnableToParseValueAsMsg>(n0);
		Assert.Equal(typeof(int), m0.Type);
		Assert.Equal(valueSpan.ToString(), m0.Value);
		var n1 = r1.AssertNone();
		var m1 = Assert.IsType<F.M.UnableToParseValueAsMsg>(n1);
		Assert.Equal(typeof(int), m1.Type);
		Assert.Equal(valueString, m1.Value);
	}
}
