// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

/// <summary>
/// Contains information about the reason why a <see cref="Maybe{T}"/> is <see cref="None{T}"/>,
/// usually used when an exception has been caught
/// </summary>
public interface IExceptionMsg : IMsg
{
	/// <summary>
	/// Exception value
	/// </summary>
	Exception Value { get; init; }
}
