// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using MaybeF.Internals;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="IfSome{T}(Maybe{T}, Action{T})"/>
	public static Task<Maybe<T>> IfSomeAsync<T>(Maybe<T> maybe, Func<T, Task> ifSome) =>
		CatchAsync(async () =>
			{
				if (maybe is Some<T> some)
				{
					await ifSome(some.Value).ConfigureAwait(false);
				}

				return maybe;
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfSome{T}(Maybe{T}, Action{T})"/>
	public static async Task<Maybe<T>> IfSomeAsync<T>(Task<Maybe<T>> maybe, Func<T, Task> ifSome) =>
		await IfSomeAsync(await maybe.ConfigureAwait(false), ifSome).ConfigureAwait(false);
}
