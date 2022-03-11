// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.IfNullAsync{T, TReason}(Task{Maybe{T}}, Func{TReason})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(this Task<Maybe<T>> @this, Func<Maybe<T>> ifNull) =>
		F.IfNullAsync(@this, () => Task.FromResult(ifNull()));

	/// <inheritdoc cref="F.IfNullAsync{T, TReason}(Task{Maybe{T}}, Func{TReason})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(this Task<Maybe<T>> @this, Func<Task<Maybe<T>>> ifNull) =>
		F.IfNullAsync(@this, ifNull);

	/// <inheritdoc cref="F.IfNullAsync{T, TReason}(Task{Maybe{T}}, Func{TReason})"/>
	public static Task<Maybe<T>> IfNullAsync<T, TReason>(this Task<Maybe<T>> @this, Func<TReason> ifNull)
		where TReason : IReason =>
		F.IfNullAsync(@this, ifNull);
}
