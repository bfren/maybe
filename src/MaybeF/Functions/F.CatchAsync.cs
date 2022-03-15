// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class F
{
	/// <inheritdoc cref="Catch{T}(Func{Maybe{T}}, Handler?)"/>
	internal static async Task<Maybe<T>> CatchAsync<T>(Func<Task<Maybe<T>>> f, Handler handler)
	{
		try
		{
			return await f().ConfigureAwait(false);
		}
		catch (NullReferenceException)
		{
			return None<T, R.MaybeCannotBeNullReason>();
		}
		catch (Exception e)
		{
			return None<T>(handler(e));
		}
	}
}
