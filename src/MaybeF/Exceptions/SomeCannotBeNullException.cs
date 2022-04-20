// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF.Exceptions;

/// <summary>
/// Thrown when a some argument is null
/// </summary>
public sealed class SomeCannotBeNullException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public SomeCannotBeNullException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public SomeCannotBeNullException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public SomeCannotBeNullException(string message, Exception inner) : base(message, inner) { }
}
