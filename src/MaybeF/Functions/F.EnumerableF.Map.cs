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
		/// Lift every value of <paramref name="list"/> to be a <see cref="Maybe{T}"/>
		/// </summary>
		/// <typeparam name="T">Maybe value type</typeparam>
		/// <param name="list">List of values</param>
		public static IEnumerable<Maybe<T>> Map<T>(IEnumerable<T> list) =>
			Map(list, x => Some(x));

		/// <inheritdoc cref="Map{T, TReturn}(IEnumerable{T}, Func{T, Maybe{TReturn}})"/>
		public static IEnumerable<Maybe<TReturn>> Map<T, TReturn>(IEnumerable<T> list, Func<T, TReturn> map) =>
			Map(list, x => Some(map(x)));

		/// <summary>
		/// Map every value of <paramref name="list"/> to be a <see cref="Maybe{TReturn}"/>
		/// </summary>
		/// <typeparam name="T">Maybe value type</typeparam>
		/// <typeparam name="TReturn">Return value type</typeparam>
		/// <param name="list">List of values</param>
		/// <param name="map">Mapping function</param>
		public static IEnumerable<Maybe<TReturn>> Map<T, TReturn>(IEnumerable<T> list, Func<T, Maybe<TReturn>> map)
		{
			foreach (var value in list)
			{
				if (value is not null)
				{
					yield return map(value);
				}
				else
				{
					yield return None<TReturn, R.NullEnumerableValueReason>();
				}
			}
		}

		public static partial class R
		{
			/// <summary>Item in an IEnumerable is null</summary>
			public sealed record class NullEnumerableValueReason : IReason;
		}
	}
}
