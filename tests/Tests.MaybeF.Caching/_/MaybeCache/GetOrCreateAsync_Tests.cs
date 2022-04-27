// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Microsoft.Extensions.Caching.Memory;
using static MaybeF.Caching.MaybeCache.M;

namespace MaybeF.Caching.MaybeCache_Tests;

public class GetOrCreateAsync_Tests
{
	[Fact]
	public async Task Key_Null__Returns_None_With_KeyIsNullMsg()
	{
		// Arrange
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string>(mc);

		// Act
		var result = await cache.GetOrCreateAsync(null!, () => Task.FromResult(Rnd.DateTime));

		// Assert
		result.AssertNone().AssertType<KeyIsNullMsg>();
	}

	[Fact]
	public async Task Value_Exists__Incorrect_Type__Returns_None_With_CacheEntryIsIncorrectTypeMsg()
	{
		// Arrange
		var key = Rnd.Str;
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string>(mc);
		mc.TryGetValue(key, out Arg.Any<object>()!)
			.Returns(x =>
			{
				x[1] = Rnd.Lng;
				return true;
			});

		// Act
		var r0 = await cache.GetOrCreateAsync(key, () => Task.FromResult(Rnd.DateTime));
		var r1 = await cache.GetOrCreateAsync(key, () => Task.FromResult(F.Some(Rnd.DateTime)));

		// Assert
		r0.AssertNone().AssertType<CacheEntryIsIncorrectTypeMsg>();
		r1.AssertNone().AssertType<CacheEntryIsIncorrectTypeMsg>();
	}

	[Fact]
	public async Task Value_Exists__Is_Null__Returns_None_With_CacheEntryIsNullMsg()
	{
		// Arrange
		var key = Rnd.Str;
		var mc = Substitute.For<IMemoryCache>();
		var cache = new MaybeCache<string>(mc);
		mc.TryGetValue(key, out Arg.Any<object>()!)
			.Returns(x =>
			{
				x[1] = null;
				return true;
			});

		// Act
		var r0 = await cache.GetOrCreateAsync(key, () => Task.FromResult(Rnd.DateTime));
		var r1 = await cache.GetOrCreateAsync(key, () => Task.FromResult(F.Some(Rnd.DateTime)));

		// Assert
		r0.AssertNone().AssertType<CacheEntryIsNullMsg>();
		r1.AssertNone().AssertType<CacheEntryIsNullMsg>();
	}

	[Fact]
	public async Task Value_Exists__Correct_Type__Returns_Value()
	{
		// Arrange
		var key = Rnd.Str;
		var value = Rnd.Lng;
		var mc = Substitute.For<IMemoryCache>();
		mc.TryGetValue(key, out Arg.Any<object>()!)
			.Returns(x =>
			{
				x[1] = value;
				return true;
			});
		var cache = new MaybeCache<string>(mc);

		// Act
		var r0 = await cache.GetOrCreateAsync(key, () => Task.FromResult(Rnd.Lng));
		var r1 = await cache.GetOrCreateAsync(key, () => Task.FromResult(F.Some(Rnd.Lng)));

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(value, s1);
	}

	[Fact]
	public async Task Value_Does_Not_Exist__Calls_Cache_CreateEntry__Sets_Value__Returns_Value()
	{
		// Arrange
		var key = Rnd.Str;
		var value = Rnd.Lng;
		var f0 = Substitute.For<Func<Task<long>>>();
		f0.Invoke()
			.Returns(Task.FromResult(value));
		var f1 = Substitute.For<Func<Task<Maybe<long>>>>();
		f1.Invoke()
			.Returns(Task.FromResult(F.Some(value)));
		var mc = Substitute.For<IMemoryCache>();
		mc.TryGetValue(Arg.Any<string>(), out Arg.Any<object>()!)
			.Returns(x =>
			{
				x[1] = null;
				return false;
			});
		var cache = new MaybeCache<string>(mc);

		// Act
		var r0 = await cache.GetOrCreateAsync(key, f0);
		var r1 = await cache.GetOrCreateAsync(key, f1);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(value, s1);
		var entry = mc.Received(2).CreateEntry(key);
		Assert.Equal(value, entry.Value);
	}

	[Fact]
	public async Task Caches_Exception_In_ValueFactory__Returns_None_With_ErrorCreatingCacheValueMsg()
	{
		// Arrange
		var key = Rnd.Str;
		var ex = new Exception();
		Task<long> f0() => throw ex;
		Task<Maybe<long>> f1() => throw ex;
		var mc = Substitute.For<IMemoryCache>();
		mc.TryGetValue(Arg.Any<string>(), out Arg.Any<object>()!)
			.Returns(x =>
			{
				x[1] = null;
				return false;
			});
		var cache = new MaybeCache<string>(mc);

		// Act
		var r0 = await cache.GetOrCreateAsync(key, f0);
		var r1 = await cache.GetOrCreateAsync(key, f1);

		// Assert
		var n0 = r0.AssertNone().AssertType<ErrorCreatingCacheValueMsg>();
		Assert.Equal(ex, n0.Value);
		var n1 = r1.AssertNone().AssertType<ErrorCreatingCacheValueMsg>();
		Assert.Equal(ex, n1.Value);
	}

	[Fact]
	public void Multiple_Threads__Calls_CreateEntry__Once()
	{
		// Arrange
		var key = Rnd.Str;
		var created = false;
		var f = Substitute.For<Func<Task<long>>>();
		var mc = Substitute.For<IMemoryCache>();
		mc.TryGetValue(key, out Arg.Any<object>()!)
			.Returns(x =>
			{
				x[1] = Rnd.Lng;
				return created;
			});
		mc.When(x => x.CreateEntry(key))
			.Do(Callback.First(_ => created = true));
		var cache = new MaybeCache<string>(mc);

		// Act
		Parallel.ForEach(
			source: Enumerable.Range(0, 30),
			parallelOptions: new() { MaxDegreeOfParallelism = 10 },
			body: async _ => await cache.GetOrCreateAsync(key, f)
		);

		// Assert
		mc.Received(1).CreateEntry(key);
	}
}
