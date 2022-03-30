// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Collections.Generic;
using System.Globalization;

namespace MaybeF;

/// <summary>
/// Functions for interacting with and manipulating <see cref="Maybe{T}"/>
/// </summary>
public static partial class F
{
	/// <summary>
	/// Default culture (en-GB) - used when parsing strings
	/// </summary>
	public static CultureInfo DefaultCulture { get; set; } = CultureInfo.GetCultureInfo("en-GB");

	/// <summary>
	/// Functions for interacting with <see cref="IDictionary{TKey, TValue}"/>
	/// </summary>
	public static partial class DictionaryF
	{
		/// <summary>Messages</summary>
		public static partial class M { }
	}

	/// <summary>
	/// Functions for interacting with <see cref="IEnumerable{T}"/>
	/// </summary>
	public static partial class EnumerableF
	{
		/// <summary>Messages</summary>
		public static partial class M { }
	}

	/// <summary>Messages</summary>
	public static partial class M { }
}
