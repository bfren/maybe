// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="Switch{T, TReturn}(Maybe{T}, Func{T, TReturn}, Func{IReason, TReturn})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(Maybe<T> maybe, Func<T, Task<TReturn>> some, Func<IReason, Task<TReturn>> none) =>
		Switch(
			maybe,
			some: v => some(v),
			none: r => none(r)
		);

	/// <inheritdoc cref="Switch{T, TReturn}(Maybe{T}, Func{T, TReturn}, Func{IReason, TReturn})"/>
	public static async Task<TReturn> SwitchAsync<T, TReturn>(Task<Maybe<T>> maybe, Func<T, Task<TReturn>> some, Func<IReason, Task<TReturn>> none) =>
		await SwitchAsync(await maybe.ConfigureAwait(false), some, none).ConfigureAwait(false);
}
