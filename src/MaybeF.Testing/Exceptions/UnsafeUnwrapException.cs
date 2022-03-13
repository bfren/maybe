// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF.Testing.Exceptions;

/// <summary>
/// Thrown when <see cref="MaybeExtensions.UnsafeUnwrap{T}(Maybe{T})"/> fails
/// </summary>
public class UnsafeUnwrapException : Exception
{
	/// <summary>Create exception</summary>
	public UnsafeUnwrapException() { }

	/// <summary>Create exception</summary>
	/// <param name="message">Message</param>
	public UnsafeUnwrapException(string message) : base(message) { }

	/// <summary>Create exception</summary>
	/// <param name="message">Message</param>
	/// <param name="inner">Inner Exception</param>
	public UnsafeUnwrapException(string message, Exception inner) : base(message, inner) { }
}
