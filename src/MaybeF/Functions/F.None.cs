// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Create a <see cref="MaybeF.None{T}"/> Maybe with a Reason
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="reason"><see cref="IMsg"/> reason</param>
	public static None<T> None<T>(IMsg reason) =>
		new(reason);

	/// <summary>
	/// Create a <see cref="MaybeF.None{T}"/> Maybe with a Reason by type
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TMsg"><see cref="IMsg"/> type</typeparam>
	public static None<T> None<T, TMsg>()
		where TMsg : IMsg, new() =>
		new(new TMsg());

	/// <summary>
	/// Create a <see cref="MaybeF.None{T}"/> Maybe with an exception Reason by type<br/>
	/// NB: <typeparamref name="TExceptionMsg"/> must have a constructor with precisely one argument to
	/// receive <paramref name="ex"/> as the value, or creation will fail
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TExceptionMsg"><see cref="IExceptionMsg"/> type</typeparam>
	/// <param name="ex">Exception object</param>
	public static None<T> None<T, TExceptionMsg>(Exception ex)
		where TExceptionMsg : IExceptionMsg
	{
		var none = () => None<T>(new M.GeneralExceptionMsg<TExceptionMsg>(ex));

		try
		{
			return Activator.CreateInstance(typeof(TExceptionMsg), ex) switch
			{
				TExceptionMsg message =>
					None<T>(message),

				_ =>
					none()
			};
		}
		catch (Exception)
		{
			return none();
		}
	}

	public static partial class M
	{
		/// <summary>Unable to create exception reason</summary>
		/// <typeparam name="TExceptionMsg"><see cref="IExceptionMsg"/> type</typeparam>
		/// <param name="Value">Exception value</param>
		public sealed record class GeneralExceptionMsg<TExceptionMsg>(Exception Value) : IExceptionMsg
			where TExceptionMsg : IExceptionMsg;
	}
}
