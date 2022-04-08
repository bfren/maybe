// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Collections.Generic;

namespace MaybeF;

public static partial class F
{
	public static partial class DictionaryF
	{
		/// <summary>
		/// Return the value or <see cref="None{T}"/>
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
									None<TValue>(new M.NullValueMsg<TKey>(key)),

								false =>
									None<TValue>(new M.KeyDoesNotExistMsg<TKey>(key))
							},

						_ =>
							None<TValue, M.KeyCannotBeNullMsg>()
					},

				false =>
					None<TValue, M.DictionaryIsEmptyMsg>()

			};

		public static partial class M
		{
			/// <summary>The dictionary is empty</summary>
			public sealed record class DictionaryIsEmptyMsg : IMsg;

			/// <summary>The dictionary key cannot be null</summary>
			public sealed record class KeyCannotBeNullMsg : IMsg;

			/// <summary>The specified key does not exist in the dictionary</summary>
			/// <typeparam name="TKey">Key type</typeparam>
			/// <param name="Key">Dictionary key</param>
			public sealed record class KeyDoesNotExistMsg<TKey>(TKey Key) : IMsg;

			/// <summary>The dictionary value for the specified key was null</summary>
			/// <typeparam name="TKey">Key type</typeparam>
			/// <param name="Key">Dictionary key</param>
			public sealed record class NullValueMsg<TKey>(TKey Key) : IMsg;
		}
	}
}
