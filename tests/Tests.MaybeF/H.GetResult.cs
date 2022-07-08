// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

internal static class H
{
	public static T GetResult<T>(Task<T> t) =>
		t.GetAwaiter().GetResult();

	public static T GetResult<T>(ValueTask<T> t)
	{
		Assert.True(t.IsCompleted);
		return t.Result;
	}
}
