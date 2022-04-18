// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Microsoft.Extensions.Caching.Memory;
using static MaybeF.Caching.MaybeCache.M;

namespace MaybeF.Caching.MaybeCache_Tests;

public class GetOrCreate_Tests
{
	[Fact]
	public void Value_Exists__Returns_Value()
	{
		// Arrange
		var key = Rnd.Str;
		var value = Rnd.Lng;
		var mc = Substitute.For<IMemoryCache>();
		mc.TryGetValue(key, out Arg.Any<object>())
			.Returns(x =>
			{
				x[1] = value;
				return true;
			});
		var cache = new MaybeCache<string>(mc);

		// Act
		var r0 = cache.GetOrCreate(key, () => Rnd.Lng);
		var r1 = cache.GetOrCreate(key, () => F.Some(Rnd.Lng));

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(value, s1);
	}

	[Fact]
	public void Value_Does_Not_Exist__Calls_Cache_CreateEntry__Sets_Value__Returns_Value()
	{
		// Arrange
		var key = Rnd.Str;
		var value = Rnd.Lng;
		var f0 = Substitute.For<Func<long>>();
		f0.Invoke()
			.Returns(value);
		var f1 = Substitute.For<Func<Maybe<long>>>();
		f1.Invoke()
			.Returns(value);
		var mc = Substitute.For<IMemoryCache>();
		mc.TryGetValue(Arg.Any<string>(), out Arg.Any<object>())
			.Returns(x =>
			{
				x[1] = null;
				return false;
			});
		var cache = new MaybeCache<string>(mc);

		// Act
		var r0 = cache.GetOrCreate(key, f0);
		var r1 = cache.GetOrCreate(key, f1);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(value, s1);
		var entry = mc.Received(2).CreateEntry(key);
		Assert.Equal(value, entry.Value);
	}

	[Fact]
	public void Caches_Exception_In_ValueFactory__Returns_None_With_ErrorCreatingCacheValueMsg()
	{
		// Arrange
		var key = Rnd.Str;
		var ex = new Exception();
		long f0() => throw ex;
		Maybe<long> f1() => throw ex;
		var mc = Substitute.For<IMemoryCache>();
		mc.TryGetValue(Arg.Any<string>(), out Arg.Any<object>())
			.Returns(x =>
			{
				x[1] = null;
				return false;
			});
		var cache = new MaybeCache<string>(mc);

		// Act
		var r0 = cache.GetOrCreate(key, f0);
		var r1 = cache.GetOrCreate(key, f1);

		// Assert
		var n0 = r0.AssertNone().AssertType<ErrorCreatingCacheValueMsg>();
		Assert.Equal(ex, n0.Value);
		var n1 = r1.AssertNone().AssertType<ErrorCreatingCacheValueMsg>();
		Assert.Equal(ex, n1.Value);
	}
}
