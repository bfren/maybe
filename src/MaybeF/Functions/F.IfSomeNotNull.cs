// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Diagnostics.CodeAnalysis;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Delegate action to inform the compiler that a value is not null
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="value">Value object (not null)</param>
	public delegate void SomeNotNull<T>([NotNull] T value);

	/// <summary>
	/// Run <paramref name="ifSomeNotNull"/> if <paramref name="maybe"/> is a <see cref="MaybeF.Some{T}"/>
	/// and the inner value is not null, and returns the original <paramref name="maybe"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="ifSomeNotNull">Will receive <see cref="Some{T}.Value"/> if this is a <see cref="MaybeF.Some{T}"/></param>
	public static Maybe<T> IfSomeNotNull<T>(Maybe<T> maybe, SomeNotNull<T> ifSomeNotNull) =>
		Catch(() =>
			{
				if (maybe is Some<T> some && some.Value is T value)
				{
					ifSomeNotNull(value);
				}

				return maybe;
			},
			DefaultHandler
		);
}
