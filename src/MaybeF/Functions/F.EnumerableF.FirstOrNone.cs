// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using System.Linq;

namespace MaybeF;

public static partial class F
{
	public static partial class EnumerableF
	{
		/// <summary>
		/// Return the first element or <see cref="Internals.None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="list">List of values</param>
		/// <param name="predicate">[Optional] Predicate to filter items</param>
		public static Maybe<T> FirstOrNone<T>(IEnumerable<T> list, Func<T, bool>? predicate) =>
			Catch<T>(() =>
				list.Any() switch
				{
					true =>
						list.FirstOrDefault(x => predicate is null || predicate(x)) switch
						{
							T x =>
								x,

							_ =>
								None<T, M.FirstItemIsNullMsg>()
						},

					false =>
						None<T, M.ListIsEmptyMsg>()
				},
				DefaultHandler
			);

		public static partial class M
		{
			/// <summary>Null item found when doing FirstOrDefault()</summary>
			public sealed record class FirstItemIsNullMsg : IMsg;
		}
	}
}
