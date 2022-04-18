// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Microsoft.Extensions.Caching.Memory;

namespace MaybeF.Caching.MaybeCache_Tests;

public class SetValue_Tests
{
	[Fact]
	public void Calls_Cache_CreateEntry__Sets_Value()
	{
		// Arrange
		var key = Rnd.Str;
		var value = Rnd.Lng;
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string, long>(mc);

		// Act
		cache.SetValue(key, value);

		// Assert
		var entry = mc.Received().CreateEntry(key);
		Assert.Equal(value, entry.Value);
	}
}
