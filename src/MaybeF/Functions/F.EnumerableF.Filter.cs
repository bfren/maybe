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
		/// Filter elements to return only the values of those that are <see cref="Internals.Some{T}"/>
		/// </summary>
		/// <typeparam name="T">Maybe value type</typeparam>
		/// <param name="list">List of Maybe values</param>
		/// <param name="predicate">[Optional] Predicate to use with filter</param>
		public static IEnumerable<T> Filter<T>(IEnumerable<Maybe<T>> list, Func<T, bool>? predicate)
		{
			foreach (var maybe in list)
			{
				foreach (var some in maybe)
				{
					if (predicate is null || predicate(some))
					{
						yield return some;
					}
				}
			}
		}
	}
}
