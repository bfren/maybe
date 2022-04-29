// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Maybe{T}}?, Func{T, Maybe{T}}?)"/>
	public static async Task<Maybe<T>> SwitchIfAsync<T>(Task<Maybe<T>> maybe, Func<T, bool> check, Func<T, Maybe<T>>? ifTrue, Func<T, Maybe<T>>? ifFalse) =>
		SwitchIf(await maybe.ConfigureAwait(false), check, ifTrue, ifFalse);

	/// <inheritdoc cref="SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, IMsg})"/>
	public static async Task<Maybe<T>> SwitchIfAsync<T>(Task<Maybe<T>> maybe, Func<T, bool> check, Func<T, IMsg> ifFalse) =>
		SwitchIf(await maybe.ConfigureAwait(false), check, ifFalse);

	/// <inheritdoc cref="SwitchIf{T}(Maybe{T}, Func{T, bool})"/>
	public static async Task<bool> SwitchIfAsync<T>(Task<Maybe<T>> maybe, Func<T, bool> check) =>
		SwitchIf(await maybe.ConfigureAwait(false), check);
}
