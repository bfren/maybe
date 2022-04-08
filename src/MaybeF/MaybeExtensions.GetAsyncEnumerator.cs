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
		if (await @this.ConfigureAwait(false) is Some<T> some)
		{
			yield return some.Value;
		}
	}
}
