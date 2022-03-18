// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<short> ParseInt16(string input) =>
		Parse<short>(input, short.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<short> ParseInt16(ReadOnlySpan<char> input) =>
		Parse<short>(input, short.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<int> ParseInt32(string input) =>
		Parse<int>(input, int.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<int> ParseInt32(ReadOnlySpan<char> input) =>
		Parse<int>(input, int.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<long> ParseInt64(string input) =>
		Parse<long>(input, long.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<long> ParseInt64(ReadOnlySpan<char> input) =>
		Parse<long>(input, long.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<nint> ParseIntPtr(string input) =>
		Parse<nint>(input, nint.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<nint> ParseIntPtr(ReadOnlySpan<char> input) =>
		Parse<nint>(input, nint.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<uint> ParseUInt16(string input) =>
		Parse<uint>(input, uint.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<ushort> ParseUInt16(ReadOnlySpan<char> input) =>
		Parse<ushort>(input, ushort.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<uint> ParseUInt32(string input) =>
		Parse<uint>(input, uint.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<uint> ParseUInt32(ReadOnlySpan<char> input) =>
		Parse<uint>(input, uint.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<ulong> ParseUInt64(string input) =>
		Parse<ulong>(input, ulong.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<ulong> ParseUInt64(ReadOnlySpan<char> input) =>
		Parse<ulong>(input, ulong.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<nuint> ParseUIntPtr(string input) =>
		Parse<nuint>(input, nuint.TryParse);

	/// <inheritdoc cref="TryParseSpan{T}"/>
	public static Maybe<nuint> ParseUIntPtr(ReadOnlySpan<char> input) =>
		Parse<nuint>(input, nuint.TryParse);
}
