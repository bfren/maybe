// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, TReturn none) =>
		MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none));

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, TReturn none) =>
		MaybeF.SwitchAsync(@this, some: some, none: _ => Task.FromResult(none));

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Task<TReturn> none) =>
		MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none);

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Task<TReturn> none) =>
		MaybeF.SwitchAsync(@this, some: some, none: _ => none);

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<TReturn> none) =>
		MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<TReturn> none) =>
		MaybeF.SwitchAsync(@this, some: some, none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<Task<TReturn>> none) =>
		MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none());

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<Task<TReturn>> none) =>
		MaybeF.SwitchAsync(@this, some: some, none: _ => none());

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<IReason, TReturn> none) =>
		MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<IReason, TReturn> none) =>
		MaybeF.SwitchAsync(@this, some: some, none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<IReason, Task<TReturn>> none) =>
		MaybeF.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: none);

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Task{Maybe{T}}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<IReason, Task<TReturn>> none) =>
		MaybeF.SwitchAsync(@this, some: some, none: none);
}
