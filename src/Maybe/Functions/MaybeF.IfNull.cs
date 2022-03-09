// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using Maybe.Internals;

namespace Maybe.Functions;

public static partial class MaybeF
{
	/// <summary>
	/// If <paramref name="maybe"/> is <see cref="Internals.None{T}"/> and the reason is <see cref="R.NullValueReason"/>,
	/// or <paramref name="maybe"/> is <see cref="Internals.Some{T}"/> and <see cref="Some{T}.Value"/> is null,
	/// runs <paramref name="ifNull"/> - which gives you the opportunity to return a more useful 'Not Found' Reason
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

				None<T> x when x.Reason is R.NullValueReason =>
					ifNull(),

				_ =>
					maybe
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TReason">Reason type</typeparam>
	public static Maybe<T> IfNull<T, TReason>(Maybe<T> maybe, Func<TReason> ifNull)
		where TReason : IReason =>
		IfNull(maybe, () => None<T>(ifNull()));
}
