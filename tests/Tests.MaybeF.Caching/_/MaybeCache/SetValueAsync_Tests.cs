// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Microsoft.Extensions.Caching.Memory;

namespace MaybeF.Caching.MaybeCache_Tests;

public class SetValueAsync_Tests
{
	[Fact]
	public async Task Null_Key__Throws_ArgumentNullException()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string, long>(mc);

		// Act
		var action = Task () => cache.SetValueAsync(null!, () => Task.FromResult(Rnd.Lng));

		// Assert
		await Assert.ThrowsAsync<ArgumentNullException>(action);
	}

	[Fact]
	public async Task Null_Value__Throws_ArgumentNullException()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<long, string>(mc);

		// Act
		var action = Task () => cache.SetValueAsync(Rnd.Lng, null!);

		// Assert
		await Assert.ThrowsAsync<ArgumentNullException>(action);
	}

	[Fact]
	public async Task Calls_Cache_CreateEntry__Sets_Value()
	{
		// Arrange
		var key = Rnd.Str;
		var value = Rnd.Lng;
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string, long>(mc);

		// Act
		await cache.SetValueAsync(key, () => Task.FromResult(value));

		// Assert
		var entry = mc.Received().CreateEntry(key);
		Assert.Equal(value, entry.Value);
	}
}
