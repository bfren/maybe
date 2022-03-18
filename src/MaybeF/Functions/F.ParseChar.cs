// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<char> ParseChar(string input) =>
		Parse<char>(input, char.TryParse);
}
