// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="Some{T}(Func{T}, Handler)"/>
	public static Maybe<T> Some<T>(T value) =>
		value switch
		{
			T x =>
				new Some<T>(x), // Some<T> is only created by Some() functions and implicit operator

			_ =>
				None<T, M.NullValueMsg>()
		};

	/// <summary>
	/// Create a <see cref="MaybeF.Some{T}"/> Maybe, containing <paramref name="value"/><br/>
	/// If <paramref name="value"/> returns null, <see cref="MaybeF.None{T}"/> will be returned instead
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="value">Some value</param>
	/// <param name="handler">Exception handler</param>
	public static Maybe<T> Some<T>(Func<T> value, Handler handler)
	{
		if (value is null)
		{
			return None<T, M.NullValueFunctionMsg>();
		}

		try
		{
			return value() switch
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

	/// <summary>
	/// Create a <see cref="MaybeF.Some{T}"/> Maybe, containing <paramref name="value"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="value">Some value</param>
	/// <param name="allowNull">If true, <see cref="MaybeF.Some{T}"/> will always be returned whatever the value is</param>
	/// <param name="handler">Exception handler</param>
	public static Maybe<T?> Some<T>(Func<T?> value, bool allowNull, Handler handler)
	{
		if (value is null)
		{
			return None<T?, M.NullValueFunctionMsg>();
		}

		try
		{
			var v = value();

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

	/// <inheritdoc cref="Some{T}(Func{T}, bool, Handler)"/>
	public static Maybe<T?> Some<T>(T? value, bool allowNull) =>
		Some(() => value, allowNull, DefaultHandler);

	public static partial class M
	{
		/// <summary>Value was null when trying to wrap using Some</summary>
		public sealed record class NullValueMsg : IMsg;

		/// <summary>Value function was null when trying to wrap using Some</summary>
		public sealed record class NullValueFunctionMsg : IMsg;

		/// <summary>Allow null was set to false when trying to return null value</summary>
		public sealed record class AllowNullWasFalseMsg : IMsg;
	}
}
