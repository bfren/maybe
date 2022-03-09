// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="MaybeF.IfNullAsync{T, TReason}(Task{Maybe{T}}, Func{TReason})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(this Task<Maybe<T>> @this, Func<Maybe<T>> ifNull) =>
		MaybeF.IfNullAsync(@this, () => Task.FromResult(ifNull()));

	/// <inheritdoc cref="MaybeF.IfNullAsync{T, TReason}(Task{Maybe{T}}, Func{TReason})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(this Task<Maybe<T>> @this, Func<Task<Maybe<T>>> ifNull) =>
		MaybeF.IfNullAsync(@this, ifNull);

	/// <inheritdoc cref="MaybeF.IfNullAsync{T, TReason}(Task{Maybe{T}}, Func{TReason})"/>
	public static Task<Maybe<T>> IfNullAsync<T, TReason>(this Task<Maybe<T>> @this, Func<TReason> ifNull)
		where TReason : IReason =>
		MaybeF.IfNullAsync(@this, ifNull);
}
