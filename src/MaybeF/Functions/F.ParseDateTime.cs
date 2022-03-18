// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateOnly> ParseDateOnly(string input) =>
		Parse<DateOnly>(input, DateOnly.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateOnly> ParseDateOnly(ReadOnlySpan<char> input) =>
		Parse<DateOnly>(input, DateOnly.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateTime> ParseDateTime(string input) =>
		Parse<DateTime>(input, DateTime.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateTime> ParseDateTime(ReadOnlySpan<char> input) =>
		Parse<DateTime>(input, DateTime.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateTimeOffset> ParseDateTimeOffset(string input) =>
		Parse<DateTimeOffset>(input, DateTimeOffset.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<DateTimeOffset> ParseDateTimeOffset(ReadOnlySpan<char> input) =>
		Parse<DateTimeOffset>(input, DateTimeOffset.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<TimeOnly> ParseTimeOnly(string input) =>
		Parse<TimeOnly>(input, TimeOnly.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<TimeOnly> ParseTimeOnly(ReadOnlySpan<char> input) =>
		Parse<TimeOnly>(input, TimeOnly.TryParse);
}
