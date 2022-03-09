// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using System.Linq;

namespace Maybe.Functions;

public static partial class MaybeF
{
	public static partial class EnumerableF
	{
		/// <summary>
		/// Return the single element or <see cref="Internals.None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="list">List of values</param>
		/// <param name="predicate">[Optional] Predicate to filter items</param>
		public static Maybe<T> SingleOrNone<T>(IEnumerable<T> list, Func<T, bool>? predicate) =>
			Catch<T>(() =>
				list.Any() switch
				{
					true =>
						list.Where(x => predicate is null || predicate(x)) switch
						{
							{ } filtered when filtered.Count() == 1 =>
								filtered.SingleOrDefault() switch
								{
									T x =>
										x,

									_ =>
										None<T, R.NullItemReason>()
								},

							{ } filtered when !filtered.Any() =>
								None<T, R.NoMatchingItemsReason>(),

							_ =>
								None<T, R.MultipleItemsReason>()
						},

					false =>
						None<T, R.ListIsEmptyReason>()
				},
				DefaultHandler
			);

		/// <summary>Reasons</summary>
		public static partial class R
		{
			/// <summary>Multiple items found when doing SingleOrDefault()</summary>
			public sealed record class MultipleItemsReason : IReason;

			/// <summary>No items found matching the predicate</summary>
			public sealed record class NoMatchingItemsReason : IReason;

			/// <summary>Null item found when doing SingleOrDefault()</summary>
			public sealed record class NullItemReason : IReason;
		}
	}
}
