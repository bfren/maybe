// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.Internals;

/// <summary>
/// 'None' Maybe - used to replace null returns (see <seealso cref="Some{T}"/>)
/// </summary>
/// <typeparam name="T">Maybe value type</typeparam>
public sealed record class None<T> : Maybe<T>
{
	/// <summary>
	/// A message for the 'None' value must always be set
	/// </summary>
	public IMsg Msg { get; private init; }

	/// <summary>
	/// Only allow internal creation by None() functions
	/// </summary>
	/// <param name="msg">Msg for this <see cref="None{T}"/></param>
	internal None(IMsg msg) =>
		Msg = msg;
}
