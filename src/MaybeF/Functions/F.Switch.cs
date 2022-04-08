// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using MaybeF.Exceptions;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Run an action depending on whether <paramref name="maybe"/> is a <see cref="Some{T}"/> or <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="some">Action to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
	/// <param name="none">Action to run if <see cref="None{T}"/></param>
	/// <exception cref="MaybeCannotBeNullException"></exception>
	/// <exception cref="UnknownMaybeException"></exception>
	public static void Switch<T>(Maybe<T> maybe, Action<T> some, Action<IMsg> none)
	{
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
	/// Run a function depending on whether <paramref name="maybe"/> is a <see cref="Some{T}"/> or <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TReturn">Next value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
	/// <param name="none">Function to run if <see cref="None{T}"/></param>
	/// <exception cref="UnknownMaybeException"></exception>
	/// <exception cref="MaybeCannotBeNullException"></exception>
	public static TReturn Switch<T, TReturn>(Maybe<T> maybe, Func<T, TReturn> some, Func<IMsg, TReturn> none) =>
		maybe switch
		{
			Some<T> x =>
				some(x.Value),

			None<T> x =>
				none(x.Reason),

			{ } =>
				throw new UnknownMaybeException(), // as Maybe<T> is internal implementation only this should never happen...

			_ =>
				throw new MaybeCannotBeNullException()
		};
}
