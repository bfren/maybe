// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace Maybe.Functions;

public static partial class MaybeF
{
	/// <summary>
	/// Return the current type if it is <see cref="Internals.Some{T}"/> and the predicate is true
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="predicate">Predicate to use with filter</param>
	public static Maybe<T> Filter<T>(Maybe<T> maybe, Func<T, bool> predicate) =>
		Bind(
			maybe,
			x =>
				predicate(x) switch
				{
					true =>
						Some(x),

					false =>
						None<T, R.FilterPredicateWasFalseReason>()
				}
		);

	/// <summary>Reasons</summary>
	public static partial class R
	{
		/// <summary>Predicate was false</summary>
		public sealed record class FilterPredicateWasFalseReason : IReason;
	}
}
