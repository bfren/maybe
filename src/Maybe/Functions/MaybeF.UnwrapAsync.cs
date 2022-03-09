// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace Maybe.Functions;

public static partial class MaybeF
{
	/// <summary>
	/// Unwrap the value of <paramref name="maybe"/> - if it is a <see cref="Internals.Some{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TSingle">Single value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="unwrap">Fluent unwrap function</param>
	public static async Task<TSingle> UnwrapAsync<T, TSingle>(
		Task<Maybe<T>> maybe,
		Func<FluentUnwrapAsync<T>, TSingle> unwrap
	) =>
		unwrap(new FluentUnwrapAsync<T>(await maybe.ConfigureAwait(false)));

	/// <summary>
	/// Fluent unwrapper
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	public class FluentUnwrapAsync<T>
	{
		private readonly Maybe<T> maybe;

		internal FluentUnwrapAsync(Maybe<T> maybe) =>
			this.maybe = maybe;

		/// <inheritdoc cref="Unwrap{T}(Maybe{T}, Func{IReason, T})"/>
		public T Value(T ifNone) =>
			Unwrap(maybe, ifNone: _ => ifNone);

		/// <inheritdoc cref="Unwrap{T}(Maybe{T}, Func{IReason, T})"/>
		public T Value(Func<T> ifNone) =>
			Unwrap(maybe, ifNone: _ => ifNone());

		/// <inheritdoc cref="Unwrap{T}(Maybe{T}, Func{IReason, T})"/>
		public T Value(Func<IReason, T> ifNone) =>
			Unwrap(maybe, ifNone);

		/// <inheritdoc cref="UnwrapSingle{T, TReturn}(Maybe{T}, Func{IReason}?, Func{IReason}?, Func{IReason}?)"/>
		public Maybe<TSingle> SingleValue<TSingle>(Func<IReason>? noItems = null, Func<IReason>? tooMany = null, Func<IReason>? notAList = null) =>
			UnwrapSingle<T, TSingle>(maybe, noItems, tooMany, notAList);
	}
}
