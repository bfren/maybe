// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.IsFalse(Maybe{bool})"/>
	public static bool IsFalse(this Maybe<bool> @this) =>
		F.IsFalse(@this);
}
