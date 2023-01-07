// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using static MaybeF.F.M;

namespace Abstracts;

public abstract class Map_Tests
{
	public abstract void Test00_If_Unknown_Maybe_Returns_None_With_UnknownMaybeTypeMsg();

	protected static void Test00(Func<Maybe<int>, Func<int, string>, F.Handler, Maybe<string>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var map = Substitute.For<Func<int, string>>();

		// Act
		var result = act(maybe, map, F.DefaultHandler);

		// Assert
		var msg = result.AssertNone().AssertType<UnknownMaybeTypeMsg>();
		Assert.Equal(typeof(FakeMaybe), msg.MaybeType);
	}

	public abstract void Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test01(Func<Maybe<string>, Func<string, int>, F.Handler, Maybe<int>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Str);
		var exception = new Exception();
		var throwFunc = int (string _) => throw exception;

		// Act
		var result = act(maybe, throwFunc, F.DefaultHandler);

		// Assert
		result.AssertNone().AssertType<UnhandledExceptionMsg>();
	}

	public abstract void Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None();

	protected static void Test02(Func<Maybe<string>, Func<string, int>, F.Handler, Maybe<int>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Str);
		var handler = Substitute.For<F.Handler>();
		var exception = new Exception();
		var throwFunc = int (string _) => throw exception;

		// Act
		var result = act(maybe, throwFunc, handler);

		// Assert
		result.AssertNone();
		handler.Received().Invoke(exception);
	}

	public abstract void Test03_If_None_Returns_None();

	protected static void Test03(Func<Maybe<int>, Func<int, string>, F.Handler, Maybe<string>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var map = Substitute.For<Func<int, string>>();

		// Act
		var result = act(maybe, map, F.DefaultHandler);

		// Assert
		result.AssertNone();
	}

	public abstract void Test04_If_None_With_Msg_Returns_None_With_Same_Msg();

	protected static void Test04(Func<Maybe<int>, Func<int, string>, F.Handler, Maybe<string>> act)
	{
		// Arrange
		var message = new TestMsg();
		var maybe = F.None<int>(message);
		var map = Substitute.For<Func<int, string>>();

		// Act
		var result = act(maybe, map, F.DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.Same(message, none);
	}

	public abstract void Test05_If_Some_Runs_Map_Function();

	protected static void Test05(Func<Maybe<int>, Func<int, string>, F.Handler, Maybe<string>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var map = Substitute.For<Func<int, string>>();

		// Act
		act(maybe, map, F.DefaultHandler);

		// Assert
		map.Received().Invoke(value);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : IMsg;
}
