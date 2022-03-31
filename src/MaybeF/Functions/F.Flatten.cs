// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Flattens a double-Maybe type
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	public static Maybe<T> Flatten<T>(Maybe<Maybe<T>> maybe) =>
		Switch(
			maybe,
			some: x => x,
			none: r => None<T>(r)
		);
}
