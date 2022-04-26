// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="Some{T}(Func{T}, Handler?)"/>
	public static async Task<Maybe<T>> SomeAsync<T>(Func<Task<T>> value, Handler handler)
	{
		if (value is null)
		{
			return None<T, M.NullValueFunctionMsg>();
		}

		try
		{
			return await value().ConfigureAwait(false) switch
			{
				T x =>
					new Some<T>(x), // Some<T> is only created by Some() functions and implicit operator

				_ =>
					None<T, M.NullValueMsg>()

			};
		}
		catch (Exception e) when (handler is not null)
		{
			return None<T>(handler(e));
		}
		catch (Exception e)
		{
			return None<T>(DefaultHandler(e));
		}
	}

	/// <inheritdoc cref="Some{T}(Func{T}, bool, Handler)"/>
	public static async Task<Maybe<T?>> SomeAsync<T>(Func<Task<T?>> value, bool allowNull, Handler handler)
	{
		if (value is null)
		{
			return None<T?, M.NullValueFunctionMsg>();
		}

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
							None<T?, M.AllowNullWasFalseMsg>()
					}

			};
		}
		catch (Exception e) when (handler is not null)
		{
			return None<T?>(handler(e));
		}
		catch (Exception e)
		{
			return None<T?>(DefaultHandler(e));
		}
	}
}
