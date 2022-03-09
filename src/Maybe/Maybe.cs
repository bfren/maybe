// Maybe .NET Monad
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Maybe.Exceptions;
using Maybe.Functions;
using Maybe.Internals;

namespace Maybe;

/// <summary>
/// Maybe type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
/// </summary>
/// <typeparam name="T">Maybe value type</typeparam>
public abstract record class Maybe<T> : IEquatable<Maybe<T>>
{
	/// <summary>
	/// Return as <see cref="Maybe{T}"/> wrapped in <see cref="Task{TResult}"/>
	/// </summary>
	[JsonIgnore]
	public Task<Maybe<T>> AsTask =>
		Task.FromResult(this);

	/// <summary>
	/// Return as <see cref="Maybe{T}"/> wrapped in <see cref="ValueTask{TResult}"/>
	/// </summary>
	[JsonIgnore]
	public ValueTask<Maybe<T>> AsValueTask =>
		ValueTask.FromResult(this);

	/// <summary>
	/// Whether or not this is <see cref="Some{T}"/>
	/// </summary>
	[JsonIgnore]
	public bool IsSome =>
		this is Some<T>;

	/// <summary>
	/// Whether or not this is <see cref="None{T}"/>
	/// </summary>
	[JsonIgnore]
	public bool IsNone =>
		this is None<T>;

	/// <summary>
	/// Returns an enumerator to enable use in foreach blocks
	/// </summary>
	public IEnumerator<T> GetEnumerator()
	{
		if (this is Some<T> some)
		{
			yield return some.Value;
		}
	}

	/// <summary>
	/// Return:
	///    Value (if this is <see cref="Some{T}"/> and Value is not null)
	///    Reason (if this is <see cref="None{T}"/> and it has a reason)
	///    Type (if this is <see cref="Some{T}"/> with a null value, or <see cref="None{T}"/> with no reason)
	/// </summary>
	public sealed override string ToString() =>
		MaybeF.Switch(
			this,
			some: v =>
				v?.ToString() switch
				{
					string value =>
						value,

					_ =>
						$"Some: {typeof(T)}"
				},

			none: r =>
				r?.ToString() switch
				{
					string when r is IExceptionReason e =>
						$"{e.GetType()}: {e.Value.Message}",

					string reason =>
						reason,

					_ =>
						$"None: {typeof(T)}"
				}
		);

	#region Operators

	/// <summary>
	/// Wrap a value in a <see cref="Some{T}"/>
	/// </summary>
	/// <param name="value">Value</param>
	public static implicit operator Maybe<T>(T value) =>
		value switch
		{
			T =>
				new Some<T>(value), // Some<T> is only created by Some() functions and implicit operator

			_ =>
				MaybeF.None<T, MaybeF.R.NullValueReason>()
		};

	/// <summary>
	/// Compare a Maybe type with a value type
	/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Maybe</param>
	/// <param name="r">Value</param>
	public static bool operator ==(Maybe<T> l, T r) =>
		MaybeF.Switch(
			l,
			some: v => Equals(v, r),
			none: _ => false
		);

	/// <summary>
	/// Compare a Maybe type with a value type
	/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Maybe</param>
	/// <param name="r">Value</param>
	public static bool operator !=(Maybe<T> l, T r) =>
		MaybeF.Switch(
			l,
			some: v => !Equals(v, r),
			none: _ => true
		);

	/// <summary>
	/// Compare a Maybe type with a value type
	/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Value</param>
	/// <param name="r">Maybe</param>
	public static bool operator ==(T l, Maybe<T> r) =>
		MaybeF.Switch(
			r,
			some: v => Equals(v, l),
			none: _ => false
		);

	/// <summary>
	/// Compare a Maybe type with a value type
	/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Value</param>
	/// <param name="r">Maybe</param>
	public static bool operator !=(T l, Maybe<T> r) =>
		MaybeF.Switch(
			r,
			some: v => !Equals(v, l),
			none: _ => true
		);

	#endregion Operators

	#region Equals

	/// <inheritdoc cref="Equals(object?)"/>
	/// <param name="other">Comparison object</param>
	public virtual bool Equals(Maybe<T>? other) =>
		this switch
		{
			Some<T> x when other is Some<T> y =>
				Equals(x.Value, y.Value),

			None<T> x when other is None<T> y =>
				Equals(x.Reason, y.Reason),

			_ =>
				false
		};

	/// <summary>
	/// Generate custom HashCode
	/// </summary>
	/// <exception cref="UnknownMaybeException"></exception>
	public override int GetHashCode() =>
		this switch
		{
			Some<T> x when x.Value is T y =>
				typeof(Some<>).GetHashCode() ^ typeof(T).GetHashCode() ^ y.GetHashCode(),

			Some<T> =>
				typeof(Some<>).GetHashCode() ^ typeof(T).GetHashCode(),

			None<T> x =>
				typeof(None<>).GetHashCode() ^ typeof(T).GetHashCode() ^ x.Reason.GetHashCode(),

			_ =>
				throw new UnknownMaybeException() // as Maybe<T> is internal implementation only this should never happen...
		};

	#endregion Equals

	#region Audit

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public Maybe<T> Audit(Action<Maybe<T>> any) =>
		MaybeF.Audit(this, any, null, null);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public Maybe<T> Audit(Action<T> some) =>
		MaybeF.Audit(this, null, some, null);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public Maybe<T> Audit(Action<IReason> none) =>
		MaybeF.Audit(this, null, null, none);

	/// <inheritdoc cref="MaybeF.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IReason}?)"/>
	public Maybe<T> Audit(Action<T> some, Action<IReason> none) =>
		MaybeF.Audit(this, null, some, none);

	/// <inheritdoc cref="MaybeF.AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{IReason, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<Maybe<T>, Task> any) =>
		MaybeF.AuditAsync(this, any, null, null);

	/// <inheritdoc cref="MaybeF.AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{IReason, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<T, Task> some) =>
		MaybeF.AuditAsync(this, null, some, null);

	/// <inheritdoc cref="MaybeF.AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{IReason, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<IReason, Task> none) =>
		MaybeF.AuditAsync(this, null, null, none);

	/// <inheritdoc cref="MaybeF.AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{IReason, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<T, Task> some, Func<IReason, Task> none) =>
		MaybeF.AuditAsync(this, null, some, none);

	#endregion Audit

	#region Bind

	/// <inheritdoc cref="MaybeF.Bind{T, U}(Maybe{T}, Func{T, Maybe{U}})"/>
	public Maybe<TReturn> Bind<TReturn>(Func<T, Maybe<TReturn>> bind) =>
		MaybeF.Bind(this, bind);

	/// <inheritdoc cref="MaybeF.BindAsync{T, U}(Maybe{T}, Func{T, Task{Maybe{U}}})"/>
	public Task<Maybe<TReturn>> BindAsync<TReturn>(Func<T, Task<Maybe<TReturn>>> bind) =>
		MaybeF.BindAsync(this, bind);

	#endregion Bind

	#region Filter

	/// <inheritdoc cref="MaybeF.Filter{T}(Maybe{T}, Func{T, bool})"/>
	public Maybe<T> Filter(Func<T, bool> predicate) =>
		MaybeF.Filter(this, predicate);

	/// <inheritdoc cref="MaybeF.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public Task<Maybe<T>> FilterAsync(Func<T, bool> predicate) =>
		MaybeF.FilterAsync(this, x => Task.FromResult(predicate(x)));

	/// <inheritdoc cref="MaybeF.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public Task<Maybe<T>> FilterAsync(Func<T, Task<bool>> predicate) =>
		MaybeF.FilterAsync(this, predicate);

	#endregion Filter

	#region IfNull

	/// <inheritdoc cref="MaybeF.IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public Maybe<T> IfNull(Func<Maybe<T>> ifNull) =>
		MaybeF.IfNull(this, ifNull);

	/// <inheritdoc cref="MaybeF.IfNull{T, TReason}(Maybe{T}, Func{TReason})"/>
	public Maybe<T> IfNull<TReason>(Func<TReason> ifNull)
		where TReason : IReason =>
		MaybeF.IfNull(this, ifNull);

	/// <inheritdoc cref="MaybeF.IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public Task<Maybe<T>> IfNullAsync(Func<Task<Maybe<T>>> ifNull) =>
		MaybeF.IfNullAsync(this, ifNull);

	#endregion IfNull

	#region IfSome

	/// <inheritdoc cref="MaybeF.IfSome{T}(Maybe{T}, Action{T})"/>
	public Maybe<T> IfSome(Action<T> ifSome) =>
		MaybeF.IfSome(this, ifSome);

	/// <inheritdoc cref="MaybeF.IfSome{T}(Maybe{T}, Action{T})"/>
	public Task<Maybe<T>> IfSomeAsync(Func<T, Task> ifSome) =>
		MaybeF.IfSomeAsync(this, ifSome);

	#endregion IfSome

	#region Map

	/// <inheritdoc cref="MaybeF.Map{T, U}(Maybe{T}, Func{T, U}, MaybeF.Handler)"/>
	public Maybe<TReturn> Map<TReturn>(Func<T, TReturn> map, MaybeF.Handler handler) =>
		MaybeF.Map(this, map, handler);

	/// <inheritdoc cref="MapAsync{T, U}(Maybe{T}, Func{T, Task{U}}, MaybeF.Handler)"/>
	public Task<Maybe<TReturn>> MapAsync<TReturn>(Func<T, Task<TReturn>> map, MaybeF.Handler handler) =>
		MaybeF.MapAsync(this, map, handler);

	#endregion Map

	#region Switch

	/// <inheritdoc cref="MaybeF.Switch{T}(Maybe{T}, Action{T}, Action{IReason})"/>
	public void Switch(Action<T> some, Action none) =>
		MaybeF.Switch(this, some: some, none: _ => none());

	/// <inheritdoc cref="MaybeF.Switch{T}(Maybe{T}, Action{T}, Action{IReason})"/>
	public void Switch(Action<T> some, Action<IReason> none) =>
		MaybeF.Switch(this, some: some, none: none);

	/// <inheritdoc cref="MaybeF.Switch{T, U}(Maybe{T}, Func{T, U}, Func{IReason, U})"/>
	public TReturn Switch<TReturn>(Func<T, TReturn> some, TReturn none) =>
		MaybeF.Switch(this, some: some, none: _ => none);

	/// <inheritdoc cref="MaybeF.Switch{T, U}(Maybe{T}, Func{T, U}, Func{IReason, U})"/>
	public TReturn Switch<TReturn>(Func<T, TReturn> some, Func<TReturn> none) =>
		MaybeF.Switch(this, some: some, none: _ => none());

	/// <inheritdoc cref="MaybeF.Switch{T, U}(Maybe{T}, Func{T, U}, Func{IReason, U})"/>
	public TReturn Switch<TReturn>(Func<T, TReturn> some, Func<IReason, TReturn> none) =>
		MaybeF.Switch(this, some: some, none: none);

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, TReturn none) =>
		MaybeF.SwitchAsync(this, some: some, none: _ => Task.FromResult(none));

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, TReturn> some, Task<TReturn> none) =>
		MaybeF.SwitchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none);

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Task<TReturn> none) =>
		MaybeF.SwitchAsync(this, some: some, none: _ => none);

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Func<TReturn> none) =>
		MaybeF.SwitchAsync(this, some: some, none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, TReturn> some, Func<Task<TReturn>> none) =>
		MaybeF.SwitchAsync(this, some: v => Task.FromResult(some(v)), none: _ => none());

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Func<Task<TReturn>> none) =>
		MaybeF.SwitchAsync(this, some: some, none: _ => none());

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, TReturn> some, Func<IReason, Task<TReturn>> none) =>
		MaybeF.SwitchAsync(this, some: v => Task.FromResult(some(v)), none: none);

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Func<IReason, TReturn> none) =>
		MaybeF.SwitchAsync(this, some: some, none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="MaybeF.SwitchAsync{T, U}(Maybe{T}, Func{T, Task{U}}, Func{IReason, Task{U}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Func<IReason, Task<TReturn>> none) =>
		MaybeF.SwitchAsync(this, some: some, none: none);

	/// <inheritdoc cref="MaybeF.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Maybe{T}}?, Func{T, Maybe{T}}?)"/>
	public Maybe<T> SwitchIf(Func<T, bool> check, Func<T, Maybe<T>>? ifTrue = null, Func<T, Maybe<T>>? ifFalse = null) =>
		MaybeF.SwitchIf(this, check, ifTrue, ifFalse);

	/// <inheritdoc cref="MaybeF.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, IReason})"/>
	public Maybe<T> SwitchIf(Func<T, bool> check, Func<T, IReason> ifFalse) =>
		MaybeF.SwitchIf(this, check, ifFalse);

	#endregion Switch

	#region Unwrap

	/// <inheritdoc cref="MaybeF.Unwrap{T}(Maybe{T}, Func{IReason, T})"/>
	public T Unwrap(T ifNone) =>
		MaybeF.Unwrap(this, ifNone: _ => ifNone);

	/// <inheritdoc cref="MaybeF.Unwrap{T}(Maybe{T}, Func{IReason, T})"/>
	public T Unwrap(Func<T> ifNone) =>
		MaybeF.Unwrap(this, ifNone: _ => ifNone());

	/// <inheritdoc cref="MaybeF.Unwrap{T}(Maybe{T}, Func{IReason, T})"/>
	public T Unwrap(Func<IReason, T> ifNone) =>
		MaybeF.Unwrap(this, ifNone: ifNone);

	/// <inheritdoc cref="MaybeF.UnwrapSingle{T, U}(Maybe{T}, Func{IReason}?, Func{IReason}?, Func{IReason}?)"/>
	public Maybe<TSingle> UnwrapSingle<TSingle>(Func<IReason>? noItems = null, Func<IReason>? tooMany = null, Func<IReason>? notAList = null) =>
		MaybeF.UnwrapSingle<T, TSingle>(this, noItems, tooMany, notAList);

	#endregion Unwrap
}
