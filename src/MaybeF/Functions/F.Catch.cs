// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using MaybeF.Exceptions;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Catch any unhandled exceptions in the chain
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="f">The chain to execute</param>
	/// <param name="handler">Caught exception handler</param>
	internal static Maybe<T> Catch<T>(Func<Maybe<T>> f, Handler handler)
	{
		if (f is null)
		{
			return None<T, M.MaybeCannotBeNullMsg>();
		}

		try
		{
			return f();
		}
		catch (UnknownMaybeException e)
		{
			return None<T>(new M.UnknownMaybeTypeMsg(e.MaybeType));
		}
		catch (Exception e) when (handler is not null)
		{
			return None<T>(handler(e));
		}
		catch (Exception e)
		{
			return None<T>(DefaultHandler(e));
		}
	}

	public static partial class M
	{
		/// <summary>Maybe input cannot be null</summary>
		public sealed record class MaybeCannotBeNullMsg : IMsg;

		/// <summary>Custom Maybe types are not permitted</summary>
		/// <param name="MaybeType">Unknown Maybe type</param>
		public sealed record class UnknownMaybeTypeMsg(Type MaybeType) : IMsg;
	}
}
