// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using MaybeF.Internals;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// If <paramref name="maybe"/> is <see cref="Internals.None{T}"/> and the message is <see cref="M.NullValueMsg"/>,
	/// or <paramref name="maybe"/> is <see cref="Internals.Some{T}"/> and <see cref="Some{T}.Value"/> is null,
	/// runs <paramref name="ifNull"/> - which gives you the opportunity to return a more useful 'Not Found' message
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="ifNull">Runs if a null value was found</param>
	public static Maybe<T> IfNull<T>(Maybe<T> maybe, Func<Maybe<T>> ifNull) =>
		Catch(() =>
			maybe switch
			{
				Some<T> x when x.Value is null =>
					ifNull(),

				None<T> x when x.Reason is M.NullValueMsg =>
					ifNull(),

				{ } =>
					maybe,

				_ =>
					ifNull()
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TMsg">Message type</typeparam>
	public static Maybe<T> IfNull<T, TMsg>(Maybe<T> maybe, Func<TMsg> ifNull)
		where TMsg : IMsg =>
		IfNull(maybe, () => None<T>(ifNull()));

	/// <summary>
	/// If <paramref name="maybe"/> is <see cref="Internals.None{T}"/>:<br/>
	///  .. if the message is <see cref="M.NullValueMsg"/>, runs <paramref name="ifNull"/>,<br/>
	///  .. otherwise returns <see cref="Internals.None{TReturn}"/><br/>
	/// If <paramref name="maybe"/> is <see cref="Internals.Some{T}"/>:<br/>
	///  .. if <see cref="Some{T}.Value"/> is null, runs <paramref name="ifNull"/>,<br/>
	///  .. otherwise runs <paramref name="ifSome"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TReturn">Next value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="ifNull">Runs if a null value was found</param>
	/// <param name="ifSome">Runs if a value was found</param>
	/// <param name="handler">Exception handler</param>
	public static Maybe<TReturn> IfNull<T, TReturn>(
		Maybe<T> maybe,
		Func<TReturn> ifNull,
		Func<T, TReturn> ifSome,
		Handler handler
	) =>
		Catch(() =>
			Switch(maybe,
				some: x => Some(x is null ? ifNull() : ifSome(x)),
				none: r => r is M.NullValueMsg ? ifNull() : None<TReturn>(r)
			),
			handler
		);

	/// <inheritdoc cref="IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, Handler)"/>
	public static Maybe<TReturn> IfNull<T, TReturn>(
		Maybe<T> maybe,
		Func<Maybe<TReturn>> ifNull,
		Func<T, Maybe<TReturn>> ifSome
	) =>
		Catch(() =>
			Switch(maybe,
				some: x => x is null ? ifNull() : ifSome(x),
				none: r => r is M.NullValueMsg ? ifNull() : None<TReturn>(r)
			),
			DefaultHandler
		);
}
