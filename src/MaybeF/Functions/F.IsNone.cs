// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// If <paramref name="maybe"/> is a <see cref="Internals.None{T}"/>, set <paramref name="reason"/>
	/// to be <see cref="Internals.None{T}.Reason"/>
	/// </summary>
	/// <remarks>
	/// Warning: <paramref name="reason"/> will be <see langword="null"/> if <paramref name="maybe"/>
	/// is a <see cref="Internals.Some{T}"/>
	/// </remarks>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="reason">Reason (null if <paramref name="maybe"/> is <see cref="Internals.Some{T}"/>)</param>
	public static bool IsNone<T>(Maybe<T> maybe, out IReason reason)
	{
		if (maybe is Internals.None<T> none)
		{
			reason = none.Reason;
			return true;
		}

		reason = default!;
		return false;
	}
}