// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Maybe{T}}?, Func{T, Maybe{T}}?)"/>
	public static Task<Maybe<T>> SwitchIfAsync<T>(
		this Task<Maybe<T>> @this,
		Func<T, bool> check,
		Func<T, Maybe<T>>? ifTrue = null,
		Func<T, Maybe<T>>? ifFalse = null
	) =>
		F.SwitchIfAsync(@this, check, ifTrue, ifFalse);

	/// <inheritdoc cref="F.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, IMsg})"/>
	public static Task<Maybe<T>> SwitchIfAsync<T>(
		this Task<Maybe<T>> @this,
		Func<T, bool> check,
		Func<T, IMsg> ifFalse
	) =>
		F.SwitchIfAsync(@this, check, ifFalse);
}
