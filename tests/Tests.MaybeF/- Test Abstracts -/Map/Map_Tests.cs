﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;
using MaybeF.Exceptions;
using MaybeF.Testing;
using static MaybeF.F.R;

namespace Abstracts;

public abstract class Map_Tests
{
	public abstract void Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason();

	protected static void Test00(Func<Maybe<int>, Func<int, string>, F.Handler, Maybe<string>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var map = Substitute.For<Func<int, string>>();

		// Act
		var result = act(maybe, map, F.DefaultHandler);

		// Assert
		var none = result.AssertNone();
		var reason = Assert.IsType<UnhandledExceptionReason>(none);
		_ = Assert.IsType<UnknownMaybeException>(reason.Value);
	}

	public abstract void Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionReason();

	protected static void Test01(Func<Maybe<string>, Func<string, int>, F.Handler, Maybe<int>> act)
	{
		// Arrange
		var maybe = F.Some(Rnd.Str);
		var exception = new Exception();
		var throwFunc = int (string _) => throw exception;

		// Act
		var result = act(maybe, throwFunc, F.DefaultHandler);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnhandledExceptionReason>(none);
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
		_ = result.AssertNone();
		_ = handler.Received().Invoke(exception);
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
		_ = result.AssertNone();
	}

	public abstract void Test04_If_None_With_Reason_Returns_None_With_Same_Reason();

	protected static void Test04(Func<Maybe<int>, Func<int, string>, F.Handler, Maybe<string>> act)
	{
		// Arrange
		var reason = new TestReason();
		var maybe = F.None<int>(reason);
		var map = Substitute.For<Func<int, string>>();

		// Act
		var result = act(maybe, map, F.DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public abstract void Test05_If_Some_Runs_Map_Function();

	protected static void Test05(Func<Maybe<int>, Func<int, string>, F.Handler, Maybe<string>> act)
	{
		// Arrange
		var value = Rnd.Int;
		var maybe = F.Some(value);
		var map = Substitute.For<Func<int, string>>();

		// Act
		_ = act(maybe, map, F.DefaultHandler);

		// Assert
		_ = map.Received().Invoke(value);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestReason : IReason;
}