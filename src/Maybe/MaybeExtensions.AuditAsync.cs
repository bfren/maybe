// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Threading.Tasks;
using Maybe.Functions;

namespace Maybe;

public static partial class MaybeExtensions
{
	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<Maybe<T>> any) =>
		MaybeF.AuditAsync(
			@this,
			any: x => { any(x); return Task.CompletedTask; },
			some: null,
			none: null
		);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<Maybe<T>, Task> any) =>
		MaybeF.AuditAsync(
			@this,
			any: any,
			some: null,
			none: null
		);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<T> some) =>
		MaybeF.AuditAsync(
			@this,
			any: null,
			some: v => { some?.Invoke(v); return Task.CompletedTask; },
			none: null
		);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<T, Task> some) =>
		MaybeF.AuditAsync(
			@this,
			any: null,
			some: some,
			none: null
		);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<IReason> none) =>
		MaybeF.AuditAsync(
			@this,
			any: null,
			some: null,
			none: r => { none?.Invoke(r); return Task.CompletedTask; }
		);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<IReason, Task> none) =>
		MaybeF.AuditAsync(
			@this,
			any: null,
			some: null,
			none: none
		);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Action<T> some, Action<IReason> none) =>
		MaybeF.AuditAsync(
			@this,
			any: null,
			some: v => { some?.Invoke(v); return Task.CompletedTask; },
			none: r => { none?.Invoke(r); return Task.CompletedTask; }
		);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public static Task<Maybe<T>> AuditAsync<T>(this Task<Maybe<T>> @this, Func<T, Task> some, Func<IReason, Task> none) =>
		MaybeF.AuditAsync(
			@this,
			any: null,
			some: some,
			none: none
		);
}
