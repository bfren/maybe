// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="Filter{T}(Maybe{T}, Func{T, bool})"/>
	public static Task<Maybe<T>> FilterAsync<T>(Maybe<T> maybe, Func<T, Task<bool>> predicate) =>
		BindAsync(
			maybe,
			async Task<Maybe<T>> (x) =>
				await predicate(x).ConfigureAwait(false) switch
				{
					true =>
						Some(x),

					false =>
						None<T, M.FilterPredicateWasFalseMsg>()
				}
		);

	/// <inheritdoc cref="Filter{T}(Maybe{T}, Func{T, bool})"/>
	public static async Task<Maybe<T>> FilterAsync<T>(Task<Maybe<T>> maybe, Func<T, Task<bool>> predicate) =>
		await FilterAsync(await maybe.ConfigureAwait(false), predicate).ConfigureAwait(false);

	/// <inheritdoc cref="Filter{T}(Maybe{T}, Func{T, bool})"/>
	public static ValueTask<Maybe<T>> FilterAsync<T>(Maybe<T> maybe, Func<T, ValueTask<bool>> predicate) =>
		BindAsync(
			maybe,
			async ValueTask<Maybe<T>> (x) =>
				await predicate(x).ConfigureAwait(false) switch
				{
					true =>
						Some(x),

					false =>
						None<T, M.FilterPredicateWasFalseMsg>()
				}
		);

	/// <inheritdoc cref="Filter{T}(Maybe{T}, Func{T, bool})"/>
	public static async ValueTask<Maybe<T>> FilterAsync<T>(ValueTask<Maybe<T>> maybe, Func<T, ValueTask<bool>> predicate) =>
		await FilterAsync(await maybe.ConfigureAwait(false), predicate).ConfigureAwait(false);
}
