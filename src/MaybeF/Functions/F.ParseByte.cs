// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Globalization;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<byte> ParseByte(string input) =>
		Parse(input, (string s, out byte result) => byte.TryParse(s, NumberStyles.Integer, DefaultCulture, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<byte> ParseByte(ReadOnlySpan<char> input) =>
		Parse(input, (ReadOnlySpan<char> s, out byte result) => byte.TryParse(s, NumberStyles.Integer, DefaultCulture, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<sbyte> ParseSByte(string input) =>
		Parse(input, (string s, out sbyte result) => sbyte.TryParse(s, NumberStyles.Integer, DefaultCulture, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<sbyte> ParseSByte(ReadOnlySpan<char> input) =>
		Parse(input, (ReadOnlySpan<char> s, out sbyte result) => sbyte.TryParse(s, NumberStyles.Integer, DefaultCulture, out result));
}
