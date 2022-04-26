// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using static MaybeF.Testing.Create.M;

namespace MaybeF.Testing.Create_Tests;

public class None_Tests
{
	[Fact]
	public void Creates_None_With_EmptyNoneForTestingMsg()
	{
		// Arrange

		// Act
		var result = Create.None<int>();

		// Assert
		result.AssertNone().AssertType<EmptyNoneForTestingMsg>();
	}
}
