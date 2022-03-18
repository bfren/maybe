// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<byte> ParseByte(string input) =>
		Parse<byte>(input, byte.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<byte> ParseByte(ReadOnlySpan<char> input) =>
		Parse<byte>(input, byte.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<sbyte> ParseSByte(string input) =>
		Parse<sbyte>(input, sbyte.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<sbyte> ParseSByte(ReadOnlySpan<char> input) =>
		Parse<sbyte>(input, sbyte.TryParse);
}
