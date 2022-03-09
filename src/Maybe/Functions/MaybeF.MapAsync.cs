// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace Maybe.Functions;

public static partial class MaybeF
{
	/// <inheritdoc cref="Map{T, TReturn}(Maybe{T}, Func{T, TReturn}, Handler)"/>
	public static Task<Maybe<TReturn>> MapAsync<T, TReturn>(Maybe<T> maybe, Func<T, Task<TReturn>> map, Handler handler) =>
		CatchAsync(() =>
			Switch(
				maybe,
				some: async v => { var x = await map(v).ConfigureAwait(false); return Some(x); },
				none: r => None<TReturn>(r).AsTask
			),
			handler
		);

	/// <inheritdoc cref="Map{T, TReturn}(Maybe{T}, Func{T, TReturn}, Handler)"/>
	public static async Task<Maybe<TReturn>> MapAsync<T, TReturn>(Task<Maybe<T>> maybe, Func<T, Task<TReturn>> map, Handler handler) =>
		await MapAsync(await maybe.ConfigureAwait(false), map, handler).ConfigureAwait(false);
}
