// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Unwrap the value of <paramref name="maybe"/> - if it is <see cref="MaybeF.Some{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="ifNone">Value to return if <paramref name="maybe"/> is a <see cref="MaybeF.None{T}"/></param>
	public static T Unwrap<T>(Maybe<T> maybe, Func<T> ifNone) =>
		Switch(
			maybe,
			some: v => v,
			none: _ => ifNone()
		);

	/// <summary>
	/// Unwrap the value of <paramref name="maybe"/> - if it is <see cref="MaybeF.Some{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="ifNone">Value to return if <paramref name="maybe"/> is a <see cref="MaybeF.None{T}"/></param>
	public static T Unwrap<T>(Maybe<T> maybe, Func<IMsg, T> ifNone) =>
		Switch(
			maybe,
			some: v => v,
			none: ifNone
		);
}
