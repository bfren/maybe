// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Globalization;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Default number style for formatting floating-point numbers
	/// </summary>
	internal static NumberStyles FloatNumberStyles { get; } = NumberStyles.Float | NumberStyles.AllowThousands;

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<decimal> ParseDecimal(string? input) =>
		Parse(input, (string? s, out decimal r) => decimal.TryParse(s, FloatNumberStyles, DefaultCulture, out r));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<decimal> ParseDecimal(ReadOnlySpan<char> input) =>
		Parse(input, (ReadOnlySpan<char> s, out decimal r) => decimal.TryParse(s, FloatNumberStyles, DefaultCulture, out r));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<double> ParseDouble(string? input) =>
		Parse(input, (string? s, out double r) => double.TryParse(s, FloatNumberStyles, DefaultCulture, out r));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<double> ParseDouble(ReadOnlySpan<char> input) =>
		Parse(input, (ReadOnlySpan<char> s, out double r) => double.TryParse(s, FloatNumberStyles, DefaultCulture, out r));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<float> ParseSingle(string? input) =>
		Parse(input, (string? s, out float r) => float.TryParse(s, FloatNumberStyles, DefaultCulture, out r));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<float> ParseSingle(ReadOnlySpan<char> input) =>
		Parse(input, (ReadOnlySpan<char> s, out float r) => float.TryParse(s, FloatNumberStyles, DefaultCulture, out r));
}
