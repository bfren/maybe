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
		/// Filter elements to return only <see cref="MaybeF.Some{T}"/> and transform using <paramref name="map"/>
		/// </summary>
		/// <typeparam name="T">Maybe value type</typeparam>
		/// <typeparam name="TReturn">Next value type</typeparam>
		/// <param name="list">Maybe list</param>
		/// <param name="map">Mapping function</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		public static IEnumerable<Maybe<TReturn>> FilterMap<T, TReturn>(
			IEnumerable<Maybe<T>> list,
			Func<T, TReturn> map,
			Func<T, bool>? predicate
		)
		{
			if (list is null || map is null)
			{
				yield break;
			}

			foreach (var some in Filter(list, predicate))
			{
				yield return map(some);
			}
		}
	}
}
