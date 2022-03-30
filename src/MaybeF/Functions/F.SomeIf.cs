// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Create <see cref="Internals.Some{T}"/> with <paramref name="value"/> if <paramref name="predicate"/> is true
	/// <para>Otherwise, will return <see cref="Internals.None{T}"/></para>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="predicate">Predicate to evaluate</param>
	/// <param name="value">Function to return value</param>
	/// <param name="handler">Exception handler</param>
	public static Maybe<T> SomeIf<T>(Func<bool> predicate, Func<T> value, Handler handler) =>
		Catch(() =>
			predicate() switch
			{
				true =>
					Some(value, handler),

				false =>
					None<T, M.PredicateWasFalseMsg>()
			},
			handler
		);

	/// <inheritdoc cref="SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
	public static Maybe<T> SomeIf<T>(Func<bool> predicate, T value, Handler handler) =>
		SomeIf(predicate, () => value, handler);

	public static partial class M
	{
		/// <summary>Predicate was false</summary>
		public sealed record class PredicateWasFalseMsg : IMsg;
	}
}
