// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace Maybe.Functions;

public static partial class MaybeF
{
	/// <summary>
	/// Use <paramref name="map"/> to convert the value of <paramref name="maybe"/> to type <typeparamref name="TReturn"/>,
	/// if it is a <see cref="Internals.Some{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TReturn">Next value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="map">Mapping function - will receive <see cref="Internals.Some{T}.Value"/> if this is a <see cref="Internals.Some{T}"/></param>
	/// <param name="handler">Exception handler</param>
	public static Maybe<TReturn> Map<T, TReturn>(Maybe<T> maybe, Func<T, TReturn> map, Handler handler) =>
		Catch(() =>
			Switch(
				maybe,
				some: v => Some(map(v)),
				none: r => None<TReturn>(r)
			),
			handler
		);
}
