// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF.Exceptions;

/// <summary>
/// Thrown when a <see cref="Maybe{T}"/> argument is null
/// </summary>
public class MaybeCannotBeNullException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public MaybeCannotBeNullException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public MaybeCannotBeNullException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public MaybeCannotBeNullException(string message, Exception inner) : base(message, inner) { }
}
