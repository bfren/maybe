// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Microsoft.Extensions.Caching.Memory;

namespace MaybeF.Caching.MaybeCache_Tests;

public class RemoveValue_Tests
{
	[Fact]
	public void Null_Key__Throws_ArgumentNullException()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string>(mc);

		// Act
		var action = void () => cache.RemoveValue(null!);

		// Assert
		Assert.Throws<ArgumentNullException>(action);
	}

	[Fact]
	public void Calls_Cache_Remove__With_Correct_Values()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string>(mc);
		var key = Rnd.Str;

		// Act
		cache.RemoveValue(key);

		// Assert
		mc.Received().Remove(key);
	}
}
