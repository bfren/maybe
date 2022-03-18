// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<decimal> ParseDecimal(string input) =>
		Parse<decimal>(input, decimal.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<decimal> ParseDecimal(ReadOnlySpan<char> input) =>
		Parse<decimal>(input, decimal.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<double> ParseDouble(string input) =>
		Parse<double>(input, double.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<double> ParseDouble(ReadOnlySpan<char> input) =>
		Parse<double>(input, double.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<float> ParseSingle(string input) =>
		Parse<float>(input, float.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<float> ParseSingle(ReadOnlySpan<char> input) =>
		Parse<float>(input, float.TryParse);
}
