// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF.Exceptions;

/// <summary>
/// Thrown when a none argument is null
/// </summary>
public sealed class NoneCannotBeNullException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public NoneCannotBeNullException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public NoneCannotBeNullException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public NoneCannotBeNullException(string message, Exception inner) : base(message, inner) { }
}
