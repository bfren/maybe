// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF.Testing.Exceptions;

/// <summary>
/// Used during test so System Exceptions don't need to be thrown
/// </summary>
public class MaybeTestException : Exception
{
	/// <summary>
	/// Create object
	/// </summary>
	public MaybeTestException() { }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="message">Message</param>
	public MaybeTestException(string message) : base(message) { }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="message">Message</param>
	/// <param name="inner">Inner Exception</param>
	public MaybeTestException(string message, Exception inner) : base(message, inner) { }
}
