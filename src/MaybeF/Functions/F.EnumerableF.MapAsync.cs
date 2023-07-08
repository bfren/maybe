// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	public static partial class EnumerableF
	{
		/// <inheritdoc cref="Map{T, TReturn}(IEnumerable{T}, Func{T, Maybe{TReturn}})"/>
		public static async IAsyncEnumerable<Maybe<TReturn>> MapAsync<T, TReturn>(IEnumerable<T> list, Func<T, Task<Maybe<TReturn>>> map)
		{
			if (list is null || map is null)
			{
				yield break;
			}

			foreach (var item in list)
			{
				if (item is not null)
				{
					await foreach (var value in map(item))
					{
						yield return value;
					}
				}
			}
		}
	}
}
