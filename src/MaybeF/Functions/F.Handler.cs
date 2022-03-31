// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.IO;

namespace MaybeF;

/// <summary>
/// Create <see cref="Maybe{T}"/> types and begin chains
/// </summary>
public static partial class F
{
	/// <summary>
	/// Exception handler delegate - takes exception and must return a message of type <see cref="IExceptionMsg"/>
	/// </summary>
	/// <param name="e">Exception</param>
	public delegate IExceptionMsg Handler(Exception e);

	/// <summary>
	/// Default exception handler,
	/// it returns <see cref="M.UnhandledExceptionMsg"/>
	/// </summary>
	public static Handler DefaultHandler =>
		e => new M.UnhandledExceptionMsg(e);

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

	public static partial class M
	{
		/// <summary>Exception while creating a new object</summary>
		/// <typeparam name="T">The type of the object being created</typeparam>
		/// <param name="Value">Exception object</param>
		public sealed record class CreateNewExceptionMsg<T>(Exception Value) : IExceptionMsg;

		/// <summary>Unhandled exception</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class UnhandledExceptionMsg(Exception Value) : IExceptionMsg;
	}
}
