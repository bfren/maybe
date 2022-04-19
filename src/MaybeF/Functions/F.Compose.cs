// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Use Kleisi composition to compose two Maybe functions
	/// </summary>
	/// <typeparam name="TInput">Input type</typeparam>
	/// <typeparam name="TInner">Inner (conversion) type</typeparam>
	/// <typeparam name="TReturn">Return type</typeparam>
	/// <param name="f">First map function</param>
	/// <param name="g">Second map function</param>
	public static Func<TInput, Maybe<TReturn>> Compose<TInput, TInner, TReturn>(
		Func<TInput, Maybe<TInner>> f,
		Func<TInner, Maybe<TReturn>> g
	) =>
		x => f(x).Bind(g);
}
