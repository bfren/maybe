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
		/// <inheritdoc cref="Bind{T, TReturn}(IEnumerable{Maybe{T}}, Func{T, Maybe{TReturn}})"/>
		public static async IAsyncEnumerable<Maybe<TReturn>> BindAsync<T, TReturn>(IEnumerable<Maybe<T>> list, Func<T, Task<Maybe<TReturn>>> bind)
		{
			if (list is null || bind is null)
			{
				yield break;
			}

			foreach (var item in list)
			{
				foreach (var value in item)
				{
					if (value is not null)
					{
						yield return await bind(value);
					}
				}
			}
		}
	}
}
