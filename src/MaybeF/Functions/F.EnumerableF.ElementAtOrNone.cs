// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Collections.Generic;
using System.Linq;

namespace MaybeF;

public static partial class F
{
	public static partial class EnumerableF
	{
		/// <summary>
		/// Return the element at <paramref name="index"/> or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="list">List of values</param>
		/// <param name="index">Index</param>
		public static Maybe<T> ElementAtOrNone<T>(IEnumerable<T> list, int index) =>
			Catch<T>(() =>
				list.Any() switch
				{
					true =>
						list.ElementAtOrDefault(index) switch
						{
							T x =>
								x,

							_ =>
								None<T, M.ElementAtIsNullMsg>()
						},

					false =>
						None<T, M.ListIsEmptyMsg>()
				},
				DefaultHandler
			);

		public static partial class M
		{
			/// <summary>Null or no item found when doing ElementAtOrDefault()</summary>
			public sealed record class ElementAtIsNullMsg : IMsg;

			/// <summary>The list is empty</summary>
			public sealed record class ListIsEmptyMsg : IMsg;
		}
	}
}
