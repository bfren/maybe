// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF.Linq;

public static partial class MaybeExtensions
{
	/// <summary>
	/// Enables LINQ select on Maybe objects, e.g.
	/// <code>from x in Maybe<br/>
	/// select x</code>
	/// </summary>
	/// <typeparam name="T">Maybe type</typeparam>
	/// <typeparam name="TReturn">Return type</typeparam>
	/// <param name="this">Maybe</param>
	/// <param name="f">Return map function</param>
	public static Maybe<TReturn> Select<T, TReturn>(this Maybe<T> @this, Func<T, TReturn> f) =>
		@this.Map(f, F.DefaultHandler);

	/// <inheritdoc cref="Select{T, TReturn}(Maybe{T}, Func{T, TReturn})"/>
	public static Task<Maybe<TReturn>> Select<T, TReturn>(this Maybe<T> @this, Func<T, Task<TReturn>> f) =>
		@this.MapAsync(f, F.DefaultHandler);

	/// <inheritdoc cref="Select{T, TReturn}(Maybe{T}, Func{T, TReturn})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Return map function</param>
	public static Task<Maybe<TReturn>> Select<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> f) =>
		@this.MapAsync(f, F.DefaultHandler);

	/// <inheritdoc cref="Select{T, TReturn}(Maybe{T}, Func{T, TReturn})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Return map function</param>
	public static Task<Maybe<TReturn>> Select<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> f) =>
		@this.MapAsync(f, F.DefaultHandler);
}
