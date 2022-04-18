// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="Maybe{T}.GetEnumerator"/>
	public static async IAsyncEnumerator<T> GetAsyncEnumerator<T>(this Task<Maybe<T>> @this)
	{
		var maybe = await @this.ConfigureAwait(false);
		if (maybe.IsSome(out var value))
		{
			yield return value;
		}
	}
}
