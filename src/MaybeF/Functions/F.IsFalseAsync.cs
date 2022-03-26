// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="IsFalse(Maybe{bool})"/>
	public static Task<bool> IsFalseAsync(Task<Maybe<bool>> maybe) =>
		SwitchAsync(maybe,
			some: x => Task.FromResult(!x),
			none: _ => Task.FromResult(false)
		);
}
