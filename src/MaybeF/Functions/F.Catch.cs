// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

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
		try
		{
			return f();
		}
		catch (NullReferenceException)
		{
			return None<T, R.MaybeCannotBeNullReason>();
		}
		catch (Exception e)
		{
			return None<T>(handler(e));
		}
	}

	public static partial class R
	{
		/// <summary>Maybe input cannot be null</summary>
		public sealed record class MaybeCannotBeNullReason : IReason;
	}
}
