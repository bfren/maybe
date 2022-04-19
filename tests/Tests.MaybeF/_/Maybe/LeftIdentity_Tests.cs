// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

/// <summary>
/// See https://blog.ploeh.dk/2022/04/11/monad-laws/
/// </summary>
public class LeftIdentity_Tests
{
	[Theory]
	[InlineData(-1)]
	[InlineData(42)]
	[InlineData(87)]
	public void Kleisli(int input)
	{
		// Arrange
		var f = Maybe<int> (int i) =>
			F.Some(i);
		var g = Maybe<string> (int i) =>
			F.Some(i.ToString());
		var composed = F.Compose(f, g);

		// Act
		var result = composed(input);

		// Assert
		Assert.Equal(g(input), result);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(42)]
	[InlineData(87)]
	public void Inlined(int input)
	{
		// Arrange
		var f = Maybe<int> (int i) =>
			F.Some(i);
		var g = Maybe<string> (int i) =>
			F.Some(i.ToString());
		var inlined = Maybe<string> (int i) =>
			f(i).Bind(g);

		// Act
		var result = inlined(input);

		// Assert
		Assert.Equal(g(input), result);
	}
}
