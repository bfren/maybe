// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// If <paramref name="maybe"/> is a <see cref="Internals.Some{T}"/>, set <paramref name="value"/>
	/// to be <see cref="Internals.Some{T}.Value"/>
	/// </summary>
	/// <remarks>
	/// Warning: <paramref name="value"/> will be <see langword="null"/> if <paramref name="maybe"/>
	/// is a <see cref="Internals.None{T}"/>
	/// </remarks>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="value">Value (null if <paramref name="maybe"/> is <see cref="Internals.None{T}"/>)</param>
	public static bool IsSome<T>(Maybe<T> maybe, out T value)
	{
		if (maybe is Internals.Some<T> some)
		{
			value = some.Value;
			return true;
		}

		value = default!;
		return false;
	}
}
