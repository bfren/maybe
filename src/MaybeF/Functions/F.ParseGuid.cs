// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<Guid> ParseGuid(string input) =>
		Parse<Guid>(input, Guid.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<Guid> ParseGuid(ReadOnlySpan<char> input) =>
		Parse<Guid>(input, Guid.TryParse);
}
