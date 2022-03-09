// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.IO;

namespace Maybe.Functions;

/// <summary>
/// Create <see cref="Maybe{T}"/> types and begin chains
/// </summary>
public static partial class MaybeF
{
	/// <summary>
	/// Exception handler delegate - takes exception and must return a Reason of type <see cref="IExceptionReason"/>
	/// </summary>
	/// <param name="e">Exception</param>
	public delegate IExceptionReason Handler(Exception e);

	/// <summary>
	/// Default exception handler,
	/// it returns <see cref="R.UnhandledExceptionReason"/>
	/// </summary>
	public static Handler DefaultHandler =>
		e => new R.UnhandledExceptionReason(e);

	/// <summary>
	/// Set to log audit exceptions - otherwise they are sent to the Console
	/// </summary>
	public static Action<Exception>? LogAuditExceptions { get; set; }

	internal static void HandleAuditException(Exception e) =>
		HandleAuditException(e, LogAuditExceptions, Console.Out);

	internal static void HandleAuditException(Exception e, Action<Exception>? log, TextWriter writer)
	{
		if (log is not null)
		{
			log(e);
		}
		else
		{
			writer.WriteLine("Audit Error: {0}", e);
		}
	}

	/// <summary>Reasons</summary>
	public static partial class R
	{
		/// <summary>Exception while creating a new object</summary>
		/// <typeparam name="T">The type of the object being created</typeparam>
		/// <param name="Value">Exception object</param>
		public sealed record class CreateNewExceptionReason<T>(Exception Value) : IExceptionReason;

		/// <summary>Unhandled exception</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class UnhandledExceptionReason(Exception Value) : IExceptionReason;
	}
}
