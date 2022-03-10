// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using MaybeF.Internals;

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

				None<T> x when x.Reason is R.NullValueReason =>
					ifNull(),

				_ =>
					maybe.AsTask
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public static async Task<Maybe<T>> IfNullAsync<T>(Task<Maybe<T>> maybe, Func<Task<Maybe<T>>> ifNull) =>
		await IfNullAsync(await maybe.ConfigureAwait(false), ifNull).ConfigureAwait(false);

	/// <inheritdoc cref="IfNull{T, TReason}(Maybe{T}, Func{TReason})"/>
	public static async Task<Maybe<T>> IfNullAsync<T, TReason>(Task<Maybe<T>> maybe, Func<TReason> ifNull)
		where TReason : IReason =>
		await IfNullAsync(await maybe.ConfigureAwait(false), () => None<T>(ifNull()).AsTask).ConfigureAwait(false);
}
