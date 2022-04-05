// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.IfNullAsync{T, TMsg}(Task{Maybe{T}}, Func{TMsg})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(this Task<Maybe<T>> @this, Func<Maybe<T>> ifNull) =>
		F.IfNullAsync(@this, () => Task.FromResult(ifNull()));

	/// <inheritdoc cref="F.IfNullAsync{T, TMsg}(Task{Maybe{T}}, Func{TMsg})"/>
	public static Task<Maybe<T>> IfNullAsync<T>(this Task<Maybe<T>> @this, Func<Task<Maybe<T>>> ifNull) =>
		F.IfNullAsync(@this, ifNull);

	/// <inheritdoc cref="F.IfNullAsync{T, TMsg}(Task{Maybe{T}}, Func{TMsg})"/>
	public static Task<Maybe<T>> IfNullAsync<T, TMsg>(this Task<Maybe<T>> @this, Func<TMsg> ifNull)
		where TMsg : IMsg =>
		F.IfNullAsync(@this, ifNull);

	/// <inheritdoc cref="F.IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, F.Handler)"/>
	public static Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		this Task<Maybe<T>> maybe,
		Func<TReturn> ifNull,
		Func<T, TReturn> ifSome,
		F.Handler handler
	) =>
		F.IfNullAsync(maybe, ifNull, ifSome, handler);

	/// <inheritdoc cref="F.IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, F.Handler)"/>
	public static Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		this Task<Maybe<T>> maybe,
		Func<Task<TReturn>> ifNull,
		Func<T, Task<TReturn>> ifSome,
		F.Handler handler
	) =>
		F.IfNullAsync(maybe, ifNull, ifSome, handler);

	/// <inheritdoc cref="F.IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, F.Handler)"/>
	public static Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		this Task<Maybe<T>> maybe,
		Func<Maybe<TReturn>> ifNull,
		Func<T, Maybe<TReturn>> ifSome
	) =>
		F.IfNullAsync(maybe, ifNull, ifSome);

	/// <inheritdoc cref="F.IfNull{T, TReturn}(Maybe{T}, Func{TReturn}, Func{T, TReturn}, F.Handler)"/>
	public static Task<Maybe<TReturn>> IfNullAsync<T, TReturn>(
		this Task<Maybe<T>> maybe,
		Func<Task<Maybe<TReturn>>> ifNull,
		Func<T, Task<Maybe<TReturn>>> ifSome
	) =>
		F.IfNullAsync(maybe, ifNull, ifSome);
}
