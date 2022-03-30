// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// If <paramref name="maybe"/> is a <see cref="Internals.None{T}"/>, set <paramref name="message"/>
	/// to be <see cref="Internals.None{T}.Msg"/>
	/// </summary>
	/// <remarks>
	/// Warning: <paramref name="message"/> will be <see langword="null"/> if <paramref name="maybe"/>
	/// is a <see cref="Internals.Some{T}"/>
	/// </remarks>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="message">Msg (null if <paramref name="maybe"/> is <see cref="Internals.Some{T}"/>)</param>
	public static bool IsNone<T>(Maybe<T> maybe, out IMsg message)
	{
		if (maybe is Internals.None<T> none)
		{
			message = none.Msg;
			return true;
		}

		message = default!;
		return false;
	}
}
