// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(Maybe<T> maybe, Func<Task<Maybe<T>>> ifNull) =>
		CatchAsync(() =>
			maybe switch
			{
				Some<T> x when x.Value is null =>
					ifNull(),

				None<T> x when x.Reason is M.NullValueMsg =>
					ifNull(),

				{ } =>
					maybe.AsTask(),

				_ =>
					ifNull()
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public static async Task<Maybe<T>> IfNullAsync<T>(Task<Maybe<T>> maybe, Func<Task<Maybe<T>>> ifNull) =>
		await IfNullAsync(await maybe.ConfigureAwait(false), ifNull).ConfigureAwait(false);

	/// <inheritdoc cref="IfNull{T, TMsg}(Maybe{T}, Func{TMsg})"/>
	public static async Task<Maybe<T>> IfNullAsync<T, TMsg>(Task<Maybe<T>> maybe, Func<TMsg> ifNull)
		where TMsg : IMsg =>
		await IfNullAsync(await maybe.ConfigureAwait(false), () => None<T>(ifNull()).AsTask()).ConfigureAwait(false);

	/// <inheritdoc cref="IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, Handler)"/>
	public static Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		Maybe<T> maybe,
		Func<Task<TReturn>> ifNull,
		Func<T, Task<TReturn>> ifSome,
		Handler handler
	) =>
		CatchAsync(() =>
			SwitchAsync(maybe,
				some: async x => Some(x is null ? await ifNull() : await ifSome(x)),
				none: async r => r is M.NullValueMsg ? await ifNull() : None<TReturn>(r)
			),
			handler
		);

	/// <inheritdoc cref="IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, Handler)"/>
	public static async Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		Task<Maybe<T>> maybe,
		Func<TReturn> ifNull,
		Func<T, TReturn> ifSome,
		Handler handler
	) =>
		await IfNullAsync(
			await maybe.ConfigureAwait(false),
			() => Task.FromResult(ifNull()),
			x => Task.FromResult(ifSome(x)),
			handler
		);

	/// <inheritdoc cref="IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, Handler)"/>
	public static async Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		Task<Maybe<T>> maybe,
		Func<Task<TReturn>> ifNull,
		Func<T, Task<TReturn>> ifSome,
		Handler handler
	) =>
		await IfNullAsync(
			await maybe.ConfigureAwait(false),
			ifNull,
			ifSome,
			handler
		);

	/// <inheritdoc cref="IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, Handler)"/>
	public static Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		Maybe<T> maybe,
		Func<Task<Maybe<TReturn>>> ifNull,
		Func<T, Task<Maybe<TReturn>>> ifSome
	) =>
		CatchAsync(() =>
			SwitchAsync(maybe,
				some: async x => x is null ? await ifNull() : await ifSome(x),
				none: async r => r is M.NullValueMsg ? await ifNull() : None<TReturn>(r)
			),
			DefaultHandler
		);

	/// <inheritdoc cref="IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, Handler)"/>
	public static async Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		Task<Maybe<T>> maybe,
		Func<Maybe<TReturn>> ifNull,
		Func<T, Maybe<TReturn>> ifSome
	) =>
		await IfNullAsync(
			await maybe.ConfigureAwait(false),
			() => Task.FromResult(ifNull()),
			x => Task.FromResult(ifSome(x))
		);

	/// <inheritdoc cref="IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, Handler)"/>
	public static async Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		Task<Maybe<T>> maybe,
		Func<Task<Maybe<TReturn>>> ifNull,
		Func<T, Task<Maybe<TReturn>>> ifSome
	) =>
		await IfNullAsync(
			await maybe.ConfigureAwait(false),
			ifNull,
			ifSome
		);
}
