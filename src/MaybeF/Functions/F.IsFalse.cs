// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Returns <see langword="true"/> if <paramref name="maybe"/> is <see cref="MaybeF.Some{T}"/>
	/// with a value of <see langword="false"/> returns <see langword="false"/> if <paramref name="maybe"/>
	/// is <see cref="MaybeF.None{T}"/>
	/// </summary>
	/// <param name="maybe">Input Maybe</param>
	public static bool IsFalse(Maybe<bool> maybe) =>
		Switch(maybe,
			some: x => !x,
			none: _ => false
		);
}
