// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF.Exceptions;

/// <summary>
/// Thrown when an unknown <see cref="Maybe{T}"/> type is matched -
/// as <see cref="Maybe{T}"/> only allows internal implementation this should never happen...
/// </summary>
public sealed class UnknownMaybeException : Exception
{
	/// <summary>
	/// The type of the unknown Maybe object
	/// </summary>
	public Type MaybeType { get; private init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="maybeType">Unknown Maybe type</param>
	public UnknownMaybeException(Type maybeType) =>
		MaybeType = maybeType;
}
