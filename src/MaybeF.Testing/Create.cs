// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Testing;

/// <summary>
/// Create objects for testing
/// </summary>
public static class Create
{
	/// <summary>
	/// Create an empty <see cref="Internals.None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	public static Maybe<T> None<T>() =>
		F.None<T, R.EmptyNoneForTestingReason>();

	/// <summary>Reasons</summary>
	public static class R
	{
		/// <summary>Empty None created for testing</summary>
		public sealed record class EmptyNoneForTestingReason : IReason;
	}
}
