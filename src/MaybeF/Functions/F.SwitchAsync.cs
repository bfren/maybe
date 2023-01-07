// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="Switch{T, TReturn}(Maybe{T}, Func{T, TReturn}, Func{IMsg, TReturn})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(Maybe<T> maybe, Func<T, Task<TReturn>> some, Func<IMsg, Task<TReturn>> none) =>
		Switch(maybe, some, none);

	/// <inheritdoc cref="Switch{T, TReturn}(Maybe{T}, Func{T, TReturn}, Func{IMsg, TReturn})"/>
	public static async Task<TReturn> SwitchAsync<T, TReturn>(Task<Maybe<T>> maybe, Func<T, Task<TReturn>> some, Func<IMsg, Task<TReturn>> none) =>
		await SwitchAsync(await maybe.ConfigureAwait(false), some, none).ConfigureAwait(false);

	/// <inheritdoc cref="Switch{T, TReturn}(Maybe{T}, Func{T, Maybe{TReturn}}, Func{Maybe{TReturn}})"/>
	public static Task<Maybe<TReturn>> SwitchAsync<T, TReturn>(Maybe<T> maybe, Func<T, Task<Maybe<TReturn>>> some, Func<Task<Maybe<TReturn>>> none) =>
		maybe switch
		{
			Some<T> x when some is not null =>
				CatchAsync(() => some(x.Value), DefaultHandler),

			None<T> y when none is not null =>
				CatchAsync(() => none(), DefaultHandler),

			Some<T> =>
				None<TReturn, M.SomeFunctionCannotBeNullMsg>().AsTask(),

			None<T> =>
				None<TReturn, M.NoneFunctionCannotBeNullMsg>().AsTask(),

			{ } z =>
				None<TReturn>(new M.UnknownMaybeTypeMsg(maybe.GetType())).AsTask(),

			_ =>
				None<TReturn, M.MaybeCannotBeNullMsg>().AsTask()
		};

	/// <inheritdoc cref="Switch{T, TReturn}(Maybe{T}, Func{T, Maybe{TReturn}}, Func{Maybe{TReturn}})"/>
	public static async Task<Maybe<TReturn>> SwitchAsync<T, TReturn>(Task<Maybe<T>> maybe, Func<T, Task<Maybe<TReturn>>> some, Func<Task<Maybe<TReturn>>> none) =>
		await SwitchAsync(await maybe.ConfigureAwait(false), some, none).ConfigureAwait(false);
}
