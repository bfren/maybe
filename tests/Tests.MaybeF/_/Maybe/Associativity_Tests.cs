// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Maybe_Tests;

/// <summary>
/// See https://blog.ploeh.dk/2022/04/11/monad-laws/
/// </summary>
public class Associativity_Tests
{
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	public void Kleisli(int input)
	{
		// Arrange
		var f = Maybe<bool> (double i) =>
			F.Some(i % 2 == 0);
		var g = Maybe<string> (bool b) =>
			F.Some(b.ToString());
		var h = Maybe<int> (string s) =>
			F.Some(s.Length);
		var left = F.Compose(F.Compose(f, g), h);
		var right = F.Compose(f, F.Compose(g, h));

		// Act
		var r0 = left(input);
		var r1 = right(input);

		// Assert
		Assert.Equal(r0, r1);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	public void Inlined(int input)
	{
		// Arrange
		var f = Maybe<bool> (double i) =>
			F.Some(i % 2 == 0);
		var g = Maybe<string> (bool b) =>
			F.Some(b.ToString());
		var h = Maybe<int> (string s) =>
			F.Some(s.Length);

		var fg = Maybe<string> (double i) =>
			f(i).Bind(g);
		var left = Maybe<int> (double i) =>
			fg(i).Bind(h);

		var gh = Maybe<int> (bool b) =>
			g(b).Bind(h);
		var right = Maybe<int> (double i) =>
			f(i).Bind(gh);

		// Act
		var r0 = left(input);
		var r1 = right(input);

		// Assert
		Assert.Equal(r0, r1);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	public void Inlined_Into_Assertion(int input)
	{
		// Arrange
		var f = Maybe<bool> (double i) =>
			F.Some(i % 2 == 0);
		var g = Maybe<string> (bool b) =>
			F.Some(b.ToString());
		var h = Maybe<int> (string s) =>
			F.Some(s.Length);
		var maybe = f(input);

		// Act
		var r0 = maybe.Bind(g).Bind(h);
		var r1 = maybe.Bind(x => g(x).Bind(h));

		// Assert
		Assert.Equal(r0, r1);
	}
}
