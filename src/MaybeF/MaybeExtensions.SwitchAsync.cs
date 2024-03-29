// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, TReturn none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, TReturn none) =>
		F.SwitchAsync(@this, some, none: _ => Task.FromResult(none));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Task<TReturn> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Task<TReturn> none) =>
		F.SwitchAsync(@this, some, none: _ => none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<TReturn> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<TReturn> none) =>
		F.SwitchAsync(@this, some, none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<Task<TReturn>> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: _ => none());

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<Task<TReturn>> none) =>
		F.SwitchAsync(@this, some, none: _ => none());

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<IMsg, TReturn> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<IMsg, TReturn> none) =>
		F.SwitchAsync(@this, some, none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, TReturn> some, Func<IMsg, Task<TReturn>> none) =>
		F.SwitchAsync(@this, some: v => Task.FromResult(some(v)), none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Task{Maybe{T}}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public static Task<TReturn> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<TReturn>> some, Func<IMsg, Task<TReturn>> none) =>
		F.SwitchAsync(@this, some, none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{Maybe{TReturn}}}, Func{Task{Maybe{TReturn}}})"/>
	public static Task<Maybe<TReturn>> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<Maybe<TReturn>>> some, Task<Maybe<TReturn>> none) =>
		F.SwitchAsync(@this, some, none: () => none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{Maybe{TReturn}}}, Func{Task{Maybe{TReturn}}})"/>
	public static Task<Maybe<TReturn>> SwitchAsync<T, TReturn>(this Task<Maybe<T>> @this, Func<T, Task<Maybe<TReturn>>> some, Func<Task<Maybe<TReturn>>> none) =>
		F.SwitchAsync(@this, some, none);
}
