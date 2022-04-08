// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Run <paramref name="ifSome"/> if <paramref name="maybe"/> is a <see cref="Some{T}"/>,
	/// and returns the original <paramref name="maybe"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="ifSome">Will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
	public static Maybe<T> IfSome<T>(Maybe<T> maybe, Action<T> ifSome) =>
		Catch(() =>
			{
				if (maybe is Some<T> some)
				{
					ifSome(some.Value);
				}

				return maybe;
			},
			DefaultHandler
		);
}
