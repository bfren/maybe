// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;

namespace MaybeF;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<Maybe<T>> any) =>
		F.AuditAsync(
			@this,
			any: x => { any(x); return Task.CompletedTask; },
			some: null,
			none: null
		);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<Maybe<T>, Task> any) =>
		F.AuditAsync(
			@this,
			any: any,
			some: null,
			none: null
		);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<T> some) =>
		F.AuditAsync(
			@this,
			any: null,
			some: v => { some?.Invoke(v); return Task.CompletedTask; },
			none: null
		);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<T, Task> some) =>
		F.AuditAsync(
			@this,
			any: null,
			some: some,
			none: null
		);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<IMsg> none) =>
		F.AuditAsync(
			@this,
			any: null,
			some: null,
			none: r => { none?.Invoke(r); return Task.CompletedTask; }
		);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<IMsg, Task> none) =>
		F.AuditAsync(
			@this,
			any: null,
			some: null,
			none: none
		);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<T> some, Action<IMsg> none) =>
		F.AuditAsync(
			@this,
			any: null,
			some: v => { some?.Invoke(v); return Task.CompletedTask; },
			none: r => { none?.Invoke(r); return Task.CompletedTask; }
		);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<T, Task> some, Func<IMsg, Task> none) =>
		F.AuditAsync(
			@this,
			any: null,
			some: some,
			none: none
		);
}
