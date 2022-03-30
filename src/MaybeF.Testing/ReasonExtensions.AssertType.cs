// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing;

public static partial class MsgExtensions
{
	/// <summary>
	/// Assert that <paramref name="this"/> is a message of type <typeparamref name="TMsg"/>
	/// </summary>
	/// <typeparam name="TMsg">Msg type</typeparam>
	/// <param name="this">Msg</param>
	public static TMsg AssertType<TMsg>(this IMsg @this)
		where TMsg : IMsg =>
		Assert.IsType<TMsg>(@this);
}
