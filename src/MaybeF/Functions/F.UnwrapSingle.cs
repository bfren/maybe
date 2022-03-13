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
	/// <param name="notAList">Function to run if the Maybe value is not a list</param>
	public static Maybe<TSingle> UnwrapSingle<TList, TSingle>(Maybe<TList> maybe, Func<IReason>? noItems, Func<IReason>? tooMany, Func<IReason>? notAList) =>
		Catch(() =>
			Switch(
				maybe,
				some: v => v switch
				{
					IList<TSingle> list when list.Count == 1 =>
						Some(list.Single()),

					IList<TSingle> list when list.Count == 0 =>
						None<TSingle>(noItems?.Invoke() ?? new R.UnwrapSingleNoItemsReason()),

					IList<TSingle> =>
						None<TSingle>(tooMany?.Invoke() ?? new R.UnwrapSingleTooManyItemsErrorReason()),

					IList =>
						None<TSingle, R.UnwrapSingleIncorrectTypeErrorReason>(),

					_ =>
						None<TSingle>(notAList?.Invoke() ?? new R.UnwrapSingleNotAListReason())
				},
				none: r => None<TSingle>(r)
			),
			DefaultHandler
		);

	/// <summary>Reasons</summary>
	public static partial class R
	{
		/// <summary>Base UnwrapSingle error Reason</summary>
		/// <param name="Error">UnwrapSingleError</param>
		public abstract record class UnwrapSingleErrorReason(UnwrapSingleError Error) : IReason;

		/// <summary>No items in the list</summary>
		public sealed record class UnwrapSingleNoItemsReason() : UnwrapSingleErrorReason(UnwrapSingleError.NoItems) { }

		/// <summary>Too many items in the list</summary>
		public sealed record class UnwrapSingleTooManyItemsErrorReason() : UnwrapSingleErrorReason(UnwrapSingleError.TooManyItems) { }

		/// <summary>Too many items in the list</summary>
		public sealed record class UnwrapSingleIncorrectTypeErrorReason() : UnwrapSingleErrorReason(UnwrapSingleError.IncorrectType) { }

		/// <summary>Not a list</summary>
		public sealed record class UnwrapSingleNotAListReason() : UnwrapSingleErrorReason(UnwrapSingleError.NoItems) { }

		/// <summary>
		/// Possible reasons for
		/// <see cref="UnwrapSingle{T, TReturn}(Maybe{T}, Func{IReason}?, Func{IReason}?, Func{IReason}?)"/> failing
		/// </summary>
		public enum UnwrapSingleError
		{
			/// <inheritdoc cref="UnwrapSingleNoItemsReason"/>
			NoItems = 0,

			/// <inheritdoc cref="UnwrapSingleTooManyItemsErrorReason"/>
			TooManyItems = 1,

			/// <inheritdoc cref="UnwrapSingleIncorrectTypeErrorReason"/>
			IncorrectType = 2,

			/// <inheritdoc cref="UnwrapSingleNotAListReason"/>
			NotAList = 3
		}
	}
}
