// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Jeebs.Some_Tests;

public class AsTask_Tests
{
	[Fact]
	public void Returns_None_As_Generic_Maybe()
	{
		// Arrange
		var some = F.Some(Rnd.Int);

		// Act
		var result = some.AsTask();

		// Assert
		Assert.IsType<Task<Maybe<int>>>(result);
	}
}
