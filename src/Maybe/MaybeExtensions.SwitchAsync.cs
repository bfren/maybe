// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using MaybeF;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, TReturn none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult<TReturn>(some(v)), none: _ => Task.FromResult<TReturn>(none));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, TReturn none) =>
		F.SwitchAsync(@this, some: some, none: _ => Task.FromResult<TReturn>(none));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Task<TReturn> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult<TReturn>(some(v)), none: _ => none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Task<TReturn> none) =>
		F.SwitchAsync(@this, some: some, none: _ => none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<TReturn> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult<TReturn>(some(v)), none: _ => Task.FromResult<TReturn>(none()));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<TReturn> none) =>
		F.SwitchAsync(@this, some: some, none: _ => Task.FromResult<TReturn>(none()));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<Task<TReturn>> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult<TReturn>(some(v)), none: _ => none());

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<Task<TReturn>> none) =>
		F.SwitchAsync(@this, some: some, none: _ => none());

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<IReason, TReturn> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult<TReturn>(some(v)), none: r => Task.FromResult<TReturn>(none(r)));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<IReason, TReturn> none) =>
		F.SwitchAsync(@this, some: some, none: r => Task.FromResult<TReturn>(none(r)));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<IReason, Task<TReturn>> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult<TReturn>(some(v)), none: none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IReason, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<IReason, Task<TReturn>> none) =>
		F.SwitchAsync(@this, some: some, none: none);
}
