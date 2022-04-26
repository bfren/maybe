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
	#region Handler

	/// <summary>
	/// Exception handler delegate - takes exception and must return a message of type <see cref="IExceptionMsg"/>
	/// </summary>
	/// <param name="e">Exception to handle</param>
	public delegate IExceptionMsg Handler(Exception e);

	/// <summary>
	/// Default exception handler to wrap an exception object in an <see cref="IExceptionMsg"/>
	/// </summary>
	public static Handler DefaultHandler =>
		e => e switch
		{
			NullReferenceException =>
				new M.NullReferenceExceptionMsg(e),

			_ =>
				new M.UnhandledExceptionMsg(e)
		};

	#endregion Handler

	#region Logger

	/// <summary>
	/// Exception logger delegate - so errors can be logged in functions that don't return a <see cref="Maybe{T}"/>,
	/// for example <see cref="Audit{T}(Maybe{T}, Action{Maybe{T}}?, Action{T}?, Action{IMsg}?)"/>
	/// </summary>
	/// <param name="e">Exception to log</param>
	public delegate void Logger(Exception e);

	/// <summary>
	/// Set to log exceptions - otherwise they are sent to the Console
	/// </summary>
	public static Logger? DefaultLogger { get; set; }

	internal static void LogException(Exception e) =>
		HandleException(e, DefaultLogger, Console.Out);

	internal static void HandleException(Exception e, Logger? log, TextWriter writer)
	{
		if (log is not null)
		{
			log(e);
		}
		else
		{
			writer.WriteLine("Error: {0}", e);
		}
	}

	#endregion Logger

	public static partial class M
	{
		/// <summary>Exception while creating a new object</summary>
		/// <typeparam name="T">The type of the object being created</typeparam>
		/// <param name="Value">Exception object</param>
		public sealed record class CreateNewExceptionMsg<T>(Exception Value) : IExceptionMsg;

		/// <summary>Null reference exception</summary>
		/// <param name="Value">NullReferenceException object</param>
		public sealed record class NullReferenceExceptionMsg(Exception Value) : IExceptionMsg;

		/// <summary>Unhandled exception</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class UnhandledExceptionMsg(Exception Value) : IExceptionMsg;
	}
}
