// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe.Linq;

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
		MaybeF.Map(@this, f, MaybeF.DefaultHandler);

	/// <inheritdoc cref="Select{T, U}(Maybe{T}, Func{T, U})"/>
	public static Task<Maybe<TReturn>> Select<T, TReturn>(this Maybe<T> @this, Func<T, Task<TReturn>> f) =>
		MaybeF.MapAsync(@this, f, MaybeF.DefaultHandler);

	/// <inheritdoc cref="Select{T, U}(Maybe{T}, Func{T, U})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Return map function</param>
	public static Task<Maybe<TReturn>> Select<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> f) =>
		MaybeF.MapAsync(@this, x => Task.FromResult(f(x)), MaybeF.DefaultHandler);

	/// <inheritdoc cref="Select{T, U}(Maybe{T}, Func{T, U})"/>
	/// <param name="this">Maybe (awaitable)</param>
	/// <param name="f">Return map function</param>
	public static Task<Maybe<TReturn>> Select<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> f) =>
		MaybeF.MapAsync(@this, f, MaybeF.DefaultHandler);
}
