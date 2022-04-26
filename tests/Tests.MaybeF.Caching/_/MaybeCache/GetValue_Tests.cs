// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Microsoft.Extensions.Caching.Memory;
using static MaybeF.Caching.MaybeCache.M;

namespace MaybeF.Caching.MaybeCache_Tests;

public class GetValue_Tests
{
	[Fact]
	public void Key_Null__Returns_None_With_KeyIsNullMsg()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string, DateTime>(mc);

		// Act
		var result = cache.GetValue(null!);

		// Assert
		result.AssertNone().AssertType<KeyIsNullMsg>();
	}

	[Fact]
	public void Value_Exists__Returns_Value()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var value = Rnd.Guid;
		mc.TryGetValue(Arg.Any<string>(), out Arg.Any<object>()!)
			.Returns(x =>
			{
				x[1] = value;
				return true;
			});
		var cache = new MaybeCache<string, Guid>(mc);

		// Act
		var result = cache.GetValue(Rnd.Str);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	[Fact]
	public void Value_Is_Null__Returns_None_With_CacheEntryIsNullMsg()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		mc.TryGetValue(Arg.Any<string>(), out Arg.Any<object>()!)
			.Returns(x =>
			{
				x[1] = null;
				return true;
			});
		var cache = new MaybeCache<string, long>(mc);

		// Act
		var result = cache.GetValue(Rnd.Str);

		// Assert
		result.AssertNone().AssertType<CacheEntryIsNullMsg>();
	}

	[Fact]
	public void Value_Does_Not_Exist__Returns_None_With_CacheEntryDoesNotExistMsg()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string, long>(mc);

		// Act
		var result = cache.GetValue(Rnd.Str);

		// Assert
		result.AssertNone().AssertType<CacheEntryDoesNotExistMsg>();
	}
}
