// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace Maybe;

/// <summary>
/// Contains information about the reason why a <see cref="Maybe{T}"/> is <see cref="Internals.None{T}"/>,
/// usually used when an exception has been caught
/// </summary>
public interface IExceptionReason : IReason
{
	/// <summary>
	/// Exception value
	/// </summary>
	Exception Value { get; init; }
}
