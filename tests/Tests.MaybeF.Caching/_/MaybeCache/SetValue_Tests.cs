// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Microsoft.Extensions.Caching.Memory;

namespace MaybeF.Caching.MaybeCache_Tests;

public class SetValue_Tests
{
	[Fact]
	public void Null_Key__Throws_ArgumentNullException()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string>(mc);

		// Act
		var a0 = void () => cache.SetValue(null!, Rnd.Lng);
		var a1 = void () => cache.SetValue(null!, Rnd.Lng, new());

		// Assert
		Assert.Throws<ArgumentNullException>(a0);
		Assert.Throws<ArgumentNullException>(a1);
	}

	[Fact]
	public void Null_Value__Throws_ArgumentNullException()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<long>(mc);

		// Act
		var a0 = void () => cache.SetValue<string>(Rnd.Lng, null!);
		var a1 = void () => cache.SetValue<string>(Rnd.Lng, null!, new());

		// Assert
		Assert.Throws<ArgumentNullException>(a0);
		Assert.Throws<ArgumentNullException>(a1);
	}

	[Fact]
	public void Calls_Cache_CreateEntry__Sets_Value()
	{
		// Arrange
		var key = Rnd.Str;
		var value = Rnd.Lng;
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string>(mc);

		// Act
		cache.SetValue(key, value);
		cache.SetValue(key, value, new());

		// Assert
		var entry = mc.Received(2).CreateEntry(key);
		Assert.Equal(value, entry.Value);
	}
}
