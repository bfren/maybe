// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using MaybeF.Internals;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Create a <see cref="Internals.None{T}"/> Maybe with a Reason
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="reason">Reason</param>
	public static None<T> None<T>(IReason reason) =>
		new(reason);

	/// <summary>
	/// Create a <see cref="Internals.None{T}"/> Maybe with a Reason by type
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TReason">Reason type</typeparam>
	public static None<T> None<T, TReason>()
		where TReason : IReason, new() =>
		new(new TReason());

	/// <summary>
	/// Create a <see cref="Internals.None{T}"/> Maybe with an exception Reason by type<br/>
	/// NB: <typeparamref name="TExceptionReason"/> must have a constructor with precisely one argument to
	/// receive <paramref name="ex"/> as the value, or creation will fail
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TExceptionReason">Exception Reason type</typeparam>
	/// <param name="ex">Exception object</param>
	public static None<T> None<T, TExceptionReason>(Exception ex)
		where TExceptionReason : IExceptionReason
	{
		var none = () => None<T>(new R.GeneralExceptionReason<TExceptionReason>(ex));

		try
		{
			return Activator.CreateInstance(typeof(TExceptionReason), ex) switch
			{
				TExceptionReason reason =>
					None<T>(reason),

				_ =>
					none()
			};
		}
		catch (Exception)
		{
			return none();
		}
	}

	public static partial class R
	{
		/// <summary>Unable to create exception Reason</summary>
		/// <typeparam name="TExceptionReason">IExceptionReason type</typeparam>
		/// <param name="Value">Exception value</param>
		public sealed record class GeneralExceptionReason<TExceptionReason>(Exception Value) : IExceptionReason
			where TExceptionReason : IExceptionReason;
	}
}
