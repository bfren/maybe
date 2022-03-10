// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Special case for boolean - returns Some{bool}(true)
	/// </summary>
	public static Maybe<bool> True =>
		true;

	/// <summary>
	/// Special case for boolean - returns Some{bool}(false)
	/// </summary>
	public static Maybe<bool> False =>
		false;
}
