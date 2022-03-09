// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace Maybe.Functions;

public static partial class MaybeF
{
	/// <inheritdoc cref="Bind{T, U}(Maybe{T}, Func{T, Maybe{U}})"/>
	public static Task<Maybe<TReturn>> BindAsync<T, TReturn>(Maybe<T> maybe, Func<T, Task<Maybe<TReturn>>> bind) =>
		CatchAsync(() =>
			Switch(
				maybe,
				some: v => bind(v),
				none: r => None<TReturn>(r).AsTask
			),
			DefaultHandler
		);

	/// <inheritdoc cref="Bind{T, U}(Maybe{T}, Func{T, Maybe{U}})"/>
	public static async Task<Maybe<TReturn>> BindAsync<T, TReturn>(Task<Maybe<T>> maybe, Func<T, Task<Maybe<TReturn>>> bind) =>
		await BindAsync(await maybe.ConfigureAwait(false), bind).ConfigureAwait(false);
}
