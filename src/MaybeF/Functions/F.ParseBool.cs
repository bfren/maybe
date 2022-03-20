// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<bool> ParseBool(string? input) =>
		Parse<bool>(input, bool.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<bool> ParseBool(ReadOnlySpan<char> input) =>
		Parse<bool>(input, bool.TryParse);
}
