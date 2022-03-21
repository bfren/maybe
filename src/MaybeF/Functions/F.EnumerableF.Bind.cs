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
		/// Map every value of <paramref name="list"/> to be a <see cref="Maybe{TReturn}"/>
		/// </summary>
		/// <typeparam name="T">Maybe value type</typeparam>
		/// <typeparam name="TReturn">Return value type</typeparam>
		/// <param name="list">List of values</param>
		/// <param name="bind">Binding function</param>
		public static IEnumerable<Maybe<TReturn>> Bind<T, TReturn>(IEnumerable<Maybe<T>> list, Func<T, Maybe<TReturn>> bind)
		{
			foreach (var item in list)
			{
				foreach (var value in F.Bind(item, bind))
				{
					yield return value;
				}
			}
		}
	}
}
