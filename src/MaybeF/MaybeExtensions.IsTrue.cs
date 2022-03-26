// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.IsTrue(Maybe{bool})"/>
	public static bool IsTrue(this Maybe<bool> @this) =>
		F.IsTrue(@this);
}
