// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Collections.Generic;

namespace Maybe.Functions;

public static partial class MaybeF
{
	public static partial class DictionaryF
	{
		/// <summary>
		/// Return the value or <see cref="Internals.None{T}"/>
		/// </summary>
		/// <typeparam name="TKey">Key type</typeparam>
		/// <typeparam name="TValue">Value type</typeparam>
		/// <param name="dictionary">Dictionary object</param>
		/// <param name="key">Key value</param>
		public static Maybe<TValue> GetValueOrNone<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) =>
			(dictionary.Count > 0) switch
			{
				true =>
					key switch
					{
						TKey =>
							dictionary.ContainsKey(key) switch
							{
								true when dictionary[key] is TValue value =>
									value,

								true =>
									None<TValue>(new R.NullValueReason<TKey>(key)),

								false =>
									None<TValue>(new R.KeyDoesNotExistReason<TKey>(key))
							},

						_ =>
							None<TValue, R.KeyCannotBeNullReason>()
					},

				false =>
					None<TValue, R.DictionaryIsEmptyReason>()

			};

		/// <summary>Reasons</summary>
		public static class R
		{
			/// <summary>The dictionary is empty</summary>
			public sealed record class DictionaryIsEmptyReason : IReason;

			/// <summary>The dictionary key cannot be null</summary>
			public sealed record class KeyCannotBeNullReason : IReason;

			/// <summary>The specified key does not exist in the dictionary</summary>
			/// <typeparam name="TKey">Key type</typeparam>
			/// <param name="Key">Dictionary key</param>
			public sealed record class KeyDoesNotExistReason<TKey>(TKey Key) : IReason;

			/// <summary>The dictionary value for the specified key was null</summary>
			/// <typeparam name="TKey">Key type</typeparam>
			/// <param name="Key">Dictionary key</param>
			public sealed record class NullValueReason<TKey>(TKey Key) : IReason;
		}
	}
}
