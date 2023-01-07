// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using MaybeF.Exceptions;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Run an action depending on whether <paramref name="maybe"/> is a <see cref="MaybeF.Some{T}"/> or <see cref="MaybeF.None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="some">Action to run if <see cref="MaybeF.Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
	/// <param name="none">Action to run if <see cref="MaybeF.None{T}"/></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="MaybeCannotBeNullException"></exception>
	/// <exception cref="UnknownMaybeException"></exception>
	public static void Switch<T>(Maybe<T> maybe, Action<T> some, Action<IMsg> none)
	{
		ArgumentNullException.ThrowIfNull(some);
		ArgumentNullException.ThrowIfNull(none);

		// No return value so unable to use switch statement

		if (maybe is Some<T> x)
		{
			some(x.Value);
		}
		else if (maybe is None<T> y)
		{
			none(y.Reason);
		}
		else if (maybe is not null)
		{
			throw new UnknownMaybeException(); // as Maybe<T> is internal implementation only this should never happen...
		}
		else
		{
			throw new MaybeCannotBeNullException();
		}
	}

	/// <summary>
	/// Run a function depending on whether <paramref name="maybe"/> is a <see cref="MaybeF.Some{T}"/> or <see cref="MaybeF.None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TReturn">Next value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="some">Function to run if <see cref="MaybeF.Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
	/// <param name="none">Function to run if <see cref="MaybeF.None{T}"/></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="MaybeCannotBeNullException"></exception>
	/// <exception cref="UnknownMaybeException"></exception>
	public static TReturn Switch<T, TReturn>(Maybe<T> maybe, Func<T, TReturn> some, Func<IMsg, TReturn> none)
	{
		ArgumentNullException.ThrowIfNull(some);
		ArgumentNullException.ThrowIfNull(none);

		if (maybe is Some<T> x)
		{
			return some(x.Value);
		}
		else if (maybe is None<T> y)
		{
			return none(y.Reason);
		}
		else if (maybe is not null)
		{
			throw new UnknownMaybeException(); // as Maybe<T> is internal implementation only this should never happen...
		}
		else
		{
			throw new MaybeCannotBeNullException();
		}
	}

	/// <summary>
	/// Run a function depending on whether <paramref name="maybe"/> is a <see cref="MaybeF.Some{T}"/> or <see cref="MaybeF.None{T}"/>
	/// </summary>
	/// <remarks>
	/// Be VERY careful using this function method because you will lose the original reason <see cref="IMsg"/> - sometimes this is
	/// desired, but most of the time you want to be using <see cref="Map{T, TReturn}(Maybe{T}, Func{T, TReturn}, Handler)"/> which
	/// preserves the reason while changing the return value type.
	/// </remarks>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TReturn">Next value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="some">Function to run if <see cref="MaybeF.Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
	/// <param name="none">Function to run if <see cref="MaybeF.None{T}"/></param>
	public static Maybe<TReturn> Switch<T, TReturn>(Maybe<T> maybe, Func<T, Maybe<TReturn>> some, Func<Maybe<TReturn>> none) =>
		maybe switch
		{
			Some<T> x when some is not null =>
				Catch(() => some(x.Value), DefaultHandler),

			None<T> y when none is not null =>
				Catch(() => none(), DefaultHandler),

			{ } z =>
				None<TReturn>(new M.UnknownMaybeTypeMsg(maybe.GetType())),

			_ =>
				None<TReturn, M.MaybeCannotBeNullMsg>()
		};

	public static partial class M
	{
		/// <summary>Unknown Maybe type</summary>
		public sealed record class UnknownMaybeTypeMsg(Type Type) : IMsg;
	}
}
