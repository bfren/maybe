// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Internals;

namespace Maybe.Functions;

public static partial class MaybeF
{
	/// <inheritdoc cref="Some{T}(Func{T}, Handler?)"/>
	public static async Task<Maybe<T>> SomeAsync<T>(Func<Task<T>> value, Handler handler)
	{
		try
		{
			return await value().ConfigureAwait(false) switch
			{
				T x =>
					new Some<T>(x), // Some<T> is only created by Some() functions and implicit operator

				_ =>
					None<T, R.NullValueReason>()

			};
		}
		catch (Exception e)
		{
			return None<T>(handler(e));
		}
	}

	/// <inheritdoc cref="Some{T}(Func{T}, bool, Handler)"/>
	public static async Task<Maybe<T?>> SomeAsync<T>(Func<Task<T?>> value, bool allowNull, Handler handler)
	{
		try
		{
			var v = await value().ConfigureAwait(false);

			return v switch
			{
				T x =>
					new Some<T?>(x), // Some<T> is only created by Some() functions and implicit operator

				_ =>
					allowNull switch
					{
						true =>
							new Some<T?>(v), // Some<T> is only created by Some() functions and implicit operator

						false =>
							None<T?, R.AllowNullWasFalseReason>()
					}

			};
		}
		catch (Exception e)
		{
			return None<T?>(handler(e));
		}
	}
}
