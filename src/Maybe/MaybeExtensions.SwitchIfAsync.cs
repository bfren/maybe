// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="MaybeF.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Maybe{T}}?, Func{T, Maybe{T}}?)"/>
	public static Task<Maybe<T>> SwitchIfAsync<T>(
		this Task<Maybe<T>> @this,
		Func<T, bool> check,
		Func<T, Maybe<T>>? ifTrue = null,
		Func<T, Maybe<T>>? ifFalse = null
	) =>
		MaybeF.SwitchIfAsync(@this, check, ifTrue, ifFalse);

	/// <inheritdoc cref="MaybeF.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, IReason})"/>
	public static Task<Maybe<T>> SwitchIfAsync<T>(
		this Task<Maybe<T>> @this,
		Func<T, bool> check,
		Func<T, IReason> ifFalse
	) =>
		MaybeF.SwitchIfAsync(@this, check, ifFalse);
}
