// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using MaybeF.Internals;

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
				None<T, R.NullValueReason>()
		};

	/// <summary>
	/// Create a <see cref="Internals.Some{T}"/> Maybe, containing <paramref name="value"/><br/>
	/// If <paramref name="value"/> returns null, <see cref="Internals.None{T}"/> will be returned instead
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="value">Some value</param>
	/// <param name="handler">Exception handler</param>
	public static Maybe<T> Some<T>(Func<T> value, Handler handler)
	{
		try
		{
			return value() switch
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

	/// <summary>
	/// Create a <see cref="Internals.Some{T}"/> Maybe, containing <paramref name="value"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="value">Some value</param>
	/// <param name="allowNull">If true, <see cref="Internals.Some{T}"/> will always be returned whatever the value is</param>
	/// <param name="handler">Exception handler</param>
	public static Maybe<T?> Some<T>(Func<T?> value, bool allowNull, Handler handler)
	{
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
							None<T?, R.AllowNullWasFalseReason>()
					}

			};
		}
		catch (Exception e)
		{
			return None<T?>(handler(e));
		}
	}

	/// <inheritdoc cref="Some{T}(Func{T}, bool, Handler)"/>
	public static Maybe<T?> Some<T>(T? value, bool allowNull) =>
		Some(() => value, allowNull, DefaultHandler);

	/// <summary>Reasons</summary>
	public static partial class R
	{
		/// <summary>Value was null when trying to wrap using Return</summary>
		public sealed record class NullValueReason : IReason;

		/// <summary>Allow null was set to false when trying to return null value</summary>
		public sealed record class AllowNullWasFalseReason : IReason;
	}
}
