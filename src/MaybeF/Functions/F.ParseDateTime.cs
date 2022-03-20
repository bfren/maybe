// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Globalization;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateOnly> ParseDateOnly(string input) =>
		Parse(input, (string s, out DateOnly result) => DateOnly.TryParse(s, DefaultCulture, DateTimeStyles.None, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateOnly> ParseDateOnly(ReadOnlySpan<char> input) =>
		Parse(input, (ReadOnlySpan<char> s, out DateOnly result) => DateOnly.TryParse(s, DefaultCulture, DateTimeStyles.None, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateTime> ParseDateTime(string input) =>
		Parse(input, (string s, out DateTime result) => DateTime.TryParse(s, DefaultCulture, DateTimeStyles.None, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateTime> ParseDateTime(ReadOnlySpan<char> input) =>
		Parse(input, (ReadOnlySpan<char> s, out DateTime result) => DateTime.TryParse(s, DefaultCulture, DateTimeStyles.None, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateTimeOffset> ParseDateTimeOffset(string input) =>
		Parse(input, (string s, out DateTimeOffset result) => DateTimeOffset.TryParse(s, DefaultCulture, DateTimeStyles.None, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateTimeOffset> ParseDateTimeOffset(ReadOnlySpan<char> input) =>
		Parse(input, (ReadOnlySpan<char> s, out DateTimeOffset result) => DateTimeOffset.TryParse(s, DefaultCulture, DateTimeStyles.None, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<TimeOnly> ParseTimeOnly(string input) =>
		Parse(input, (string s, out TimeOnly result) => TimeOnly.TryParse(s, DefaultCulture, DateTimeStyles.None, out result));

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<TimeOnly> ParseTimeOnly(ReadOnlySpan<char> input) =>
		Parse(input, (ReadOnlySpan<char> s, out TimeOnly result) => TimeOnly.TryParse(s, DefaultCulture, DateTimeStyles.None, out result));
}
