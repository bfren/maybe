// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Audit the Maybe and return unmodified<br/>
	/// Errors will not be returned as they affect the state of the object, but will be written to the console,
	/// or <see cref="DefaultLogger"/> if set
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being audited</param>
	/// <param name="any">[Optional] Action will run for any <paramref name="maybe"/></param>
	/// <param name="some">[Optional] Action will run if <paramref name="maybe"/> is <see cref="MaybeF.Some{T}"/></param>
	/// <param name="none">[Optional] Action will run if <paramref name="maybe"/> is <see cref="MaybeF.None{T}"/></param>
	public static Maybe<T> Audit<T>(Maybe<T> maybe, Action<Maybe<T>>? any, Action<T>? some, Action<IMsg>? none)
	{
		// Do nothing if the user gave us nothing to do!
		if (any is null && some is null && none is null)
		{
			return maybe;
		}

		// Work out which audit function to use
		var audit = Switch<T, Action>(
			maybe,
			some: v => () => some?.Invoke(v),
			none: r => () => none?.Invoke(r)
		);

		// Perform the audit
		try
		{
			any?.Invoke(maybe);
			audit();
		}
		catch (Exception e)
		{
			LogException(e);
		}

		// Return the original object
		return maybe;
	}
}
