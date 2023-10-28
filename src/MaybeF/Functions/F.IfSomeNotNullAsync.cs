// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Delegate async function to inform the compiler that a value is not null
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="value">Value object (not null)</param>
	public delegate Task SomeNotNullAsync<T>([NotNull] T value);

	/// <inheritdoc cref="IfSomeNotNull{T}(Maybe{T}, SomeNotNull{T})"/>
	public static Task<Maybe<T>> IfSomeNotNullAsync<T>(Maybe<T> maybe, SomeNotNullAsync<T> ifSomeNotNull) =>
		CatchAsync(async () =>
			{
				if (maybe is Some<T> some && some.Value is T value)
				{
					await ifSomeNotNull(value).ConfigureAwait(false);
				}

				return maybe;
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfSomeNotNull{T}(Maybe{T}, SomeNotNull{T})"/>
	public static async Task<Maybe<T>> IfSomeNotNullAsync<T>(Task<Maybe<T>> maybe, SomeNotNullAsync<T> ifSomeNotNull) =>
		await IfSomeNotNullAsync(await maybe.ConfigureAwait(false), ifSomeNotNull).ConfigureAwait(false);
}
