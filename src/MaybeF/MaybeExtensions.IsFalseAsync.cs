// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.IsFalse(Maybe{bool})"/>
	public static Task<bool> IsFalseAsync(this Task<Maybe<bool>> @this) =>
		F.IsFalseAsync(@this);
}
