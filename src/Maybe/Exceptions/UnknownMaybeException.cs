// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace Maybe.Exceptions;

/// <summary>
/// Thrown when an unknown <see cref="Maybe{T}"/> type is matched -
/// as <see cref="Maybe{T}"/> only allows internal implementation this should never happen...
/// </summary>
public class UnknownMaybeException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public UnknownMaybeException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public UnknownMaybeException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnknownMaybeException(string message, Exception inner) : base(message, inner) { }
}
