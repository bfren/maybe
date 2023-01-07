// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using MaybeF.Exceptions;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// If the input <paramref name="maybe"/> is <see cref="MaybeF.Some{T}"/>, runs <paramref name="predicate"/> function -<br/>
	/// if it returns true and <paramref name="ifTrue"/> is not null, <paramref name="ifTrue"/> is returned,<br/>
	/// if it returns false and <paramref name="ifFalse"/> is not null, <paramref name="ifFalse"/> is returned,<br/>
	/// otherwise, the original <paramref name="maybe"/> is returned.
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="predicate">Function to run if <paramref name="maybe"/> is <see cref="MaybeF.Some{T}"/></param>
	/// <param name="ifTrue">Function to run if <paramref name="predicate"/> returns true</param>
	/// <param name="ifFalse">Function to run if <paramref name="predicate"/> returns false</param>
	/// <exception cref="UnknownMaybeException"></exception>
	/// <exception cref="MaybeCannotBeNullException"></exception>
	public static Maybe<T> SwitchIf<T>(
		Maybe<T> maybe,
		Func<T, bool> predicate,
		Func<T, Maybe<T>>? ifTrue,
		Func<T, Maybe<T>>? ifFalse
	)
	{
		if (predicate is null)
		{
			return None<T, M.SwitchIfPredicateCannotBeNullMsg>();
		}

		if (maybe is Some<T> x)
		{
			try
			{
				return predicate(x.Value) switch
				{
					true =>
						ifTrue?.Invoke(x.Value) ?? x,

					false =>
						ifFalse?.Invoke(x.Value) ?? x
				};
			}
			catch (Exception e)
			{
				return None<T>(new M.SwitchIfFuncExceptionMsg(e));
			}
		}
		else if (maybe is None<T> y)
		{
			return y;
		}
		else if (maybe is not null)
		{
			throw new UnknownMaybeException(maybe.GetType()); // as Maybe<T> is internal implementation only this should never happen...
		}
		else
		{
			throw new MaybeCannotBeNullException();
		}
	}

	/// <summary>
	/// If the input <paramref name="maybe"/> is <see cref="MaybeF.Some{T}"/>, runs <paramref name="check"/> function -<br/>
	/// if it returns false, returns <see cref="MaybeF.None{T}"/> with the Reason from <paramref name="ifFalse"/>,<br/>
	/// otherwise, the original <paramref name="maybe"/> is returned.
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="check">Function to run if <paramref name="maybe"/> is <see cref="MaybeF.Some{T}"/></param>
	/// <param name="ifFalse">Function to run if <paramref name="check"/> returns false</param>
	public static Maybe<T> SwitchIf<T>(Maybe<T> maybe, Func<T, bool> check, Func<T, IMsg> ifFalse) =>
		SwitchIf(maybe, check, null, x => None<T>(ifFalse(x)));

	/// <summary>
	/// If the input <paramref name="maybe"/> is <see cref="MaybeF.Some{T}"/>, runs <paramref name="check"/> function
	/// and returns its result - otherwise returns false.
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="check">Function to run if <paramref name="maybe"/> is <see cref="MaybeF.Some{T}"/></param>
	public static bool SwitchIf<T>(Maybe<T> maybe, Func<T, bool> check) =>
		Switch(maybe, some: x => check(x), none: _ => false);

	public static partial class M
	{
		/// <summary>Predicate in SwitchIf cannot be null</summary>
		public sealed record class SwitchIfPredicateCannotBeNullMsg : IMsg;

		/// <summary>An exception was caught while executing one of the SwitchIf functions</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class SwitchIfFuncExceptionMsg(Exception Value) : IExceptionMsg;
	}
}
