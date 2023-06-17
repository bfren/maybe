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
		/// Iterate through a list and execute <paramref name="f"/> on the value of all <see cref="MaybeF.Some{T}"/> objects
		/// </summary>
		/// <typeparam name="T">Maybe value type</typeparam>
		/// <param name="list">List of Maybe values</param>
		/// <param name="f">Function to run on all <see cref="MaybeF.Some{T}"/> items in <paramref name="list"/></param>
		public static void Iterate<T>(IEnumerable<Maybe<T>> list, Action<T> f)
		{
			foreach (var item in list)
			{
				foreach (var some in item)
				{
					f(some);
				}
			}
		}
	}
}
