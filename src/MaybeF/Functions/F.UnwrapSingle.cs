// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MaybeF;

public static partial class F
{
	/// <summary>
	/// Unwrap the value of <paramref name="maybe"/> - if it is <see cref="Internals.Some{T}"/>
	/// and <typeparamref name="TList"/> implements <see cref="IEnumerable{T}"/>
	/// </summary>
	/// <typeparam name="TList">List value type</typeparam>
	/// <typeparam name="TSingle">Single value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="noItems">Function to run if the Maybe value is a list with no items</param>
	/// <param name="tooMany">Function to run if the Maybe value is a list with more than one item</param>
	/// <param name="incorrectType">Function to run if the Maybe value type doesn't match the list type</param>
	/// <param name="notAList">Function to run if the Maybe value is not a list</param>
	public static Maybe<TSingle> UnwrapSingle<TList, TSingle>(Maybe<TList> maybe, Func<IMsg>? noItems, Func<IMsg>? tooMany, Func<IMsg>? incorrectType, Func<IMsg>? notAList) =>
		Catch(() =>
			Switch(
				maybe,
				some: v => v switch
				{
					IList<TSingle> list when list.Count == 1 =>
						Some(list.Single()),

					IList<TSingle> list when list.Count == 0 =>
						None<TSingle>(noItems?.Invoke() ?? M.UnwrapSingle.NoItems),

					IList<TSingle> =>
						None<TSingle>(tooMany?.Invoke() ?? M.UnwrapSingle.TooManyItems),

					IList =>
						None<TSingle>(incorrectType?.Invoke() ?? M.UnwrapSingle.IncorrectType),

					_ =>
						None<TSingle>(notAList?.Invoke() ?? M.UnwrapSingle.NotAList)
				},
				none: r => None<TSingle>(r)
			),
			DefaultHandler
		);

	public static partial class M
	{
		/// <summary>UnwrapSingle error messages</summary>
		public static class UnwrapSingle
		{
			/// <summary>No items in the list</summary>
			public static UnwrapSingleErrorMsg NoItems { get; } = new(UnwrapSingleError.NoItems);

			/// <summary>Too many items in the list</summary>
			public static UnwrapSingleErrorMsg TooManyItems { get; } = new(UnwrapSingleError.TooManyItems);

			/// <summary>Single type doesn't match list type</summary>
			public static UnwrapSingleErrorMsg IncorrectType { get; } = new(UnwrapSingleError.IncorrectType);

			/// <summary>Not a list</summary>
			public static UnwrapSingleErrorMsg NotAList { get; } = new(UnwrapSingleError.NotAList);
		}

		/// <summary>Base UnwrapSingle error Reason</summary>
		/// <param name="Error">UnwrapSingleError</param>
		public sealed record class UnwrapSingleErrorMsg(UnwrapSingleError Error) : IMsg;

		/// <summary>
		/// Possible reasons for
		/// <see cref="UnwrapSingle{T, TReturn}(Maybe{T}, Func{IMsg}?, Func{IMsg}?, Func{IMsg}?)"/> failing
		/// </summary>
		public enum UnwrapSingleError
		{
			/// <inheritdoc cref="UnwrapSingle.NoItems"/>
			NoItems = 1 << 0,

			/// <inheritdoc cref="UnwrapSingle.TooManyItems"/>
			TooManyItems = 1 << 1,

			/// <inheritdoc cref="UnwrapSingle.IncorrectType"/>
			IncorrectType = 1 << 2,

			/// <inheritdoc cref="UnwrapSingle.NotAList"/>
			NotAList = 1 << 3
		}
	}
}
