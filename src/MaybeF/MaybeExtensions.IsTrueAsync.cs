// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.IsTrue(Maybe{bool})"/>
	public static Task<bool> IsTrueAsync(this Task<Maybe<bool>> @this) =>
		F.IsTrueAsync(@this);
}
