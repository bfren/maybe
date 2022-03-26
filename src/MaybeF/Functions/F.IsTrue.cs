// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Returns true if <paramref name="maybe"/> is <see cref="Internals.Some{T}"/> with a value of <see cref="true"/>,
	/// returns false if <paramref name="maybe"/> is <see cref="Internals.None{T}"/>
	/// </summary>
	/// <param name="maybe">Input Maybe</param>
	public static bool IsTrue(Maybe<bool> maybe) =>
		Switch(maybe,
			some: x => x,
			none: _ => false
		);
}
