// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;

namespace MaybeF;

public static partial class F
{
	public static partial class EnumerableF
	{
		/// <summary>
		/// Lift every non-null value of <paramref name="list"/> to be a <see cref="Internals.Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Maybe value type</typeparam>
		/// <param name="list">List of values</param>
		public static IEnumerable<Maybe<T>> Map<T>(IEnumerable<T> list) =>
			Map(list, x => Some(x));

		/// <summary>
		/// Map every non-null value of <paramref name="list"/> using <paramref name="map"/>
		/// </summary>
		/// <typeparam name="T">Maybe value type</typeparam>
		/// <typeparam name="TReturn">Return value type</typeparam>
		/// <param name="list">List of values</param>
		/// <param name="map">Mapping function</param>
		public static IEnumerable<Maybe<TReturn>> Map<T, TReturn>(IEnumerable<T> list, Func<T, Maybe<TReturn>> map)
		{
			foreach (var item in list)
			{
				if (item is not null)
				{
					foreach (var value in map(item))
					{
						yield return value;
					}
				}
			}
		}

		public static partial class M
		{
			/// <summary>Item in an IEnumerable is null</summary>
			public sealed record class NullEnumerableValueMsg : IMsg;
		}
	}
}
