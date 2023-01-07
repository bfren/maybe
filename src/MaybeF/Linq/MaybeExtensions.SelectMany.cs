// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF.Linq;

public static partial class MaybeExtensions
{
	/// <summary>
	/// Enables LINQ select many on Maybe objects, e.g.
	/// <code>from x in Maybe{int}<br/>
	/// from y in Maybe{int}<br/>
	/// select x + y</code>
	/// </summary>
	/// <typeparam name="T">Maybe type</typeparam>
	/// <typeparam name="TInner">Interim type</typeparam>
	/// <typeparam name="TReturn">Return type</typeparam>
	/// <param name="this">Maybe</param>
	/// <param name="f">Interim bind function</param>
	/// <param name="g">Return map function</param>
	public static Maybe<TReturn> SelectMany<T, TInner, TReturn>(this Maybe<T> @this, Func<T, Maybe<TInner>> f, Func<T, TInner, TReturn> g) =>
		@this.Bind(x => from y in f(x) select g(x, y));

	/// <inheritdoc cref="SelectMany{T, TInner, TReturn}(Maybe{T}, Func{T, Maybe{TInner}}, Func{T, TInner, TReturn})"/>
	public static Task<Maybe<TReturn>> SelectMany<T, TInner, TReturn>(this Maybe<T> @this, Func<T, Task<Maybe<TInner>>> f, Func<T, TInner, TReturn> g) =>
		@this.BindAsync(x => from y in f(x) select g(x, y));

	/// <inheritdoc cref="SelectMany{T, TInner, TReturn}(Maybe{T}, Func{T, Maybe{TInner}}, Func{T, TInner, TReturn})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Interim bind function</param>
	/// <param name="g">Return map function</param>
	public static Task<Maybe<TReturn>> SelectMany<T, TInner, TReturn>(this Task<Maybe<T>> @this, Func<T, Maybe<TInner>> f, Func<T, TInner, TReturn> g) =>
		@this.BindAsync(x => from y in f(x) select g(x, y));

	/// <inheritdoc cref="SelectMany{T, TInner, TReturn}(Maybe{T}, Func{T, Maybe{TInner}}, Func{T, TInner, TReturn})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Interim bind function</param>
	/// <param name="g">Return map function</param>
	public static Task<Maybe<TReturn>> SelectMany<T, TInner, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<Maybe<TInner>>> f, Func<T, TInner, TReturn> g) =>
		@this.BindAsync(x => from y in f(x) select g(x, y));
}
