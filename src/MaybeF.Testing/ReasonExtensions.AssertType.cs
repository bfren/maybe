// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing;

public static partial class ReasonExtensions
{
	/// <summary>
	/// Assert that <paramref name="this"/> is a reason of type <typeparamref name="TReason"/>
	/// </summary>
	/// <typeparam name="TReason">Reason type</typeparam>
	/// <param name="this">Reason</param>
	public static TReason AssertType<TReason>(this IReason @this)
		where TReason : IReason =>
		Assert.IsType<TReason>(@this);
}
