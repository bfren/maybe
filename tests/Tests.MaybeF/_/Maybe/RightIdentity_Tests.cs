// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

/// <summary>
/// See https://blog.ploeh.dk/2022/04/11/monad-laws/
/// </summary>
public class RightIdentity_Tests
{
	[Theory]
	[InlineData("foo")]
	[InlineData("ploeh")]
	[InlineData("lawful")]
	public void Kleisli(string input)
	{
		// Arrange
		var f = Maybe<int> (string i) =>
			F.Some(i.Length);
		var g = Maybe<int> (int i) =>
			F.Some(i);
		var composed = F.Compose(f, g);

		// Act
		var result = composed(input);

		// Assert
		Assert.Equal(f(input), result);
	}

	[Theory]
	[InlineData("foo")]
	[InlineData("ploeh")]
	[InlineData("lawful")]
	public void Inlined(string input)
	{
		// Arrange
		var f = Maybe<int> (string i) =>
			F.Some(i.Length);
		var g = Maybe<int> (int i) =>
			F.Some(i);
		var inlined = Maybe<int> (string i) =>
			f(i).Bind(g);

		// Act
		var result = inlined(input);

		// Assert
		Assert.Equal(f(input), result);
	}

	[Theory]
	[InlineData("foo")]
	[InlineData("ploeh")]
	[InlineData("lawful")]
	public void Inlined_Into_Assertion(string input)
	{
		// Arrange
		var f = Maybe<int> (string i) =>
			F.Some(i.Length);
		var g = Maybe<int> (int i) =>
			F.Some(i);

		// Act
		var result = f(input);

		// Assert
		Assert.Equal(result.Bind(g), result);
	}
}
