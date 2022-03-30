// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MaybeF.Exceptions;
using MaybeF.Internals;

namespace MaybeF;

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
	///    Msg (if this is <see cref="None{T}"/> and it has a message)
	///    Type (if this is <see cref="Some{T}"/> with a null value, or <see cref="None{T}"/> with no message)
	/// </summary>
	public sealed override string ToString() =>
		F.Switch(
			this,
			some: (T v) =>
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
					string when r is IExceptionMsg e =>
						$"{e.GetType()}: {e.Value.Message}",

					string message =>
						message,

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
				F.None<T, F.M.NullValueMsg>()
		};

	/// <summary>
	/// Compare a Maybe type with a value type
	/// <para>If <paramref name="l"/> is a <see cref="Some{T}"/> the <see cref="Some{T}.Value"/> will be compared to <paramref name="r"/></para>
	/// </summary>
	/// <param name="l">Maybe</param>
	/// <param name="r">Value</param>
	public static bool operator ==(Maybe<T> l, T r) =>
		F.Switch(
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
		F.Switch(
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
		F.Switch(
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
		F.Switch(
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
				Equals(x.Msg, y.Msg),

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
				typeof(None<>).GetHashCode() ^ typeof(T).GetHashCode() ^ x.Msg.GetHashCode(),

			_ =>
				throw new UnknownMaybeException() // as Maybe<T> is internal implementation only this should never happen...
		};

	#endregion Equals

	#region Audit

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public Maybe<T> Audit(Action<Maybe<T>> any) =>
		F.Audit(this, any, null, null);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public Maybe<T> Audit(Action<T> some) =>
		F.Audit(this, null, some, null);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public Maybe<T> Audit(Action<IMsg> none) =>
		F.Audit<T>(this, null, null, none);

	/// <inheritdoc cref="F.Audit{T}(Maybe{T}, Action{Maybe{T}}, Action{T}?, Action{IMsg}?)"/>
	public Maybe<T> Audit(Action<T> some, Action<IMsg> none) =>
		F.Audit(this, null, some, none);

	/// <inheritdoc cref="F.AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{IMsg, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<Maybe<T>, Task> any) =>
		F.AuditAsync(this, any, null, null);

	/// <inheritdoc cref="F.AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{IMsg, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<T, Task> some) =>
		F.AuditAsync(this, null, some, null);

	/// <inheritdoc cref="F.AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{IMsg, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<IMsg, Task> none) =>
		F.AuditAsync<T>(this, null, null, none);

	/// <inheritdoc cref="F.AuditAsync{T}(Maybe{T}, Func{Maybe{T}, Task}, Func{T, Task}?, Func{IMsg, Task}?)"/>
	public Task<Maybe<T>> AuditAsync(Func<T, Task> some, Func<IMsg, Task> none) =>
		F.AuditAsync(this, null, some, none);

	#endregion Audit

	#region Bind

	/// <inheritdoc cref="F.Bind{T, TReturn}(Maybe{T}, Func{T, Maybe{TReturn}})"/>
	public Maybe<TReturn> Bind<TReturn>(Func<T, Maybe<TReturn>> bind) =>
		F.Bind(this, bind);

	/// <inheritdoc cref="F.BindAsync{T, TReturn}(Maybe{T}, Func{T, Task{Maybe{TReturn}}})"/>
	public Task<Maybe<TReturn>> BindAsync<TReturn>(Func<T, Task<Maybe<TReturn>>> bind) =>
		F.BindAsync(this, bind);

	#endregion Bind

	#region Filter

	/// <inheritdoc cref="F.Filter{T}(Maybe{T}, Func{T, bool})"/>
	public Maybe<T> Filter(Func<T, bool> predicate) =>
		F.Filter(this, predicate);

	/// <inheritdoc cref="F.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public Task<Maybe<T>> FilterAsync(Func<T, bool> predicate) =>
		F.FilterAsync(this, (T x) => Task.FromResult(predicate(x)));

	/// <inheritdoc cref="F.FilterAsync{T}(Maybe{T}, Func{T, Task{bool}})"/>
	public Task<Maybe<T>> FilterAsync(Func<T, Task<bool>> predicate) =>
		F.FilterAsync(this, predicate);

	#endregion Filter

	#region IfNull

	/// <inheritdoc cref="F.IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public Maybe<T> IfNull(Func<Maybe<T>> ifNull) =>
		F.IfNull(this, ifNull);

	/// <inheritdoc cref="F.IfNull{T, TMsg}(Maybe{T}, Func{TMsg})"/>
	public Maybe<T> IfNull<TMsg>(Func<TMsg> ifNull)
		where TMsg : IMsg =>
		F.IfNull<T, TMsg>(this, ifNull);

	/// <inheritdoc cref="F.IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	public Task<Maybe<T>> IfNullAsync(Func<Task<Maybe<T>>> ifNull) =>
		F.IfNullAsync(this, ifNull);

	#endregion IfNull

	#region IfSome

	/// <inheritdoc cref="F.IfSome{T}(Maybe{T}, Action{T})"/>
	public Maybe<T> IfSome(Action<T> ifSome) =>
		F.IfSome(this, ifSome);

	/// <inheritdoc cref="F.IfSome{T}(Maybe{T}, Action{T})"/>
	public Task<Maybe<T>> IfSomeAsync(Func<T, Task> ifSome) =>
		F.IfSomeAsync(this, ifSome);

	#endregion IfSome

	#region IsNone

	/// <inheritdoc cref="F.IsNone{T}(Maybe{T}, out IMsg)"/>
	public bool IsNone(out IMsg message) =>
		F.IsNone(this, out message);

	#endregion IsNone

	#region IsSome

	/// <inheritdoc cref="F.IsSome{T}(Maybe{T}, out T)"/>
	public bool IsSome(out T value) =>
		F.IsSome(this, out value);

	#endregion IsSome

	#region Map

	/// <inheritdoc cref="F.Map{T, TReturn}(Maybe{T}, Func{T, TReturn}, F.Handler)"/>
	public Maybe<TReturn> Map<TReturn>(Func<T, TReturn> map, F.Handler handler) =>
		F.Map(this, map, handler);

	/// <inheritdoc cref="F.MapAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, F.Handler)"/>
	public Task<Maybe<TReturn>> MapAsync<TReturn>(Func<T, Task<TReturn>> map, F.Handler handler) =>
		F.MapAsync(this, map, handler);

	#endregion Map

	#region Switch

	/// <inheritdoc cref="F.Switch{T}(Maybe{T}, Action{T}, Action{IMsg})"/>
	public void Switch(Action<T> some, Action none) =>
		F.Switch(this, some: some, none: _ => none());

	/// <inheritdoc cref="F.Switch{T}(Maybe{T}, Action{T}, Action{IMsg})"/>
	public void Switch(Action<T> some, Action<IMsg> none) =>
		F.Switch(this, some: some, none: none);

	/// <inheritdoc cref="F.Switch{T, TReturn}(Maybe{T}, Func{T, TReturn}, Func{IMsg, TReturn})"/>
	public TReturn Switch<TReturn>(Func<T, TReturn> some, TReturn none) =>
		F.Switch(this, some: some, none: _ => none);

	/// <inheritdoc cref="F.Switch{T, TReturn}(Maybe{T}, Func{T, TReturn}, Func{IMsg, TReturn})"/>
	public TReturn Switch<TReturn>(Func<T, TReturn> some, Func<TReturn> none) =>
		F.Switch(this, some: some, none: _ => none());

	/// <inheritdoc cref="F.Switch{T, TReturn}(Maybe{T}, Func{T, TReturn}, Func{IMsg, TReturn})"/>
	public TReturn Switch<TReturn>(Func<T, TReturn> some, Func<IMsg, TReturn> none) =>
		F.Switch(this, some: some, none: none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, TReturn none) =>
		F.SwitchAsync(this, some: some, none: _ => Task.FromResult(none));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, TReturn> some, Task<TReturn> none) =>
		F.SwitchAsync(this, some: (T v) => Task.FromResult(some(v)), none: _ => none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Task<TReturn> none) =>
		F.SwitchAsync(this, some: some, none: _ => none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Func<TReturn> none) =>
		F.SwitchAsync(this, some: some, none: _ => Task.FromResult(none()));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, TReturn> some, Func<Task<TReturn>> none) =>
		F.SwitchAsync(this, some: (T v) => Task.FromResult(some(v)), none: _ => none());

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Func<Task<TReturn>> none) =>
		F.SwitchAsync(this, some: some, none: _ => none());

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, TReturn> some, Func<IMsg, Task<TReturn>> none) =>
		F.SwitchAsync(this, some: (T v) => Task.FromResult(some(v)), none: none);

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Func<IMsg, TReturn> none) =>
		F.SwitchAsync(this, some: some, none: r => Task.FromResult(none(r)));

	/// <inheritdoc cref="F.SwitchAsync{T, TReturn}(Maybe{T}, Func{T, Task{TReturn}}, Func{IMsg, Task{TReturn}})"/>
	public Task<TReturn> SwitchAsync<TReturn>(Func<T, Task<TReturn>> some, Func<IMsg, Task<TReturn>> none) =>
		F.SwitchAsync(this, some: some, none: none);

	/// <inheritdoc cref="F.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, Maybe{T}}?, Func{T, Maybe{T}}?)"/>
	public Maybe<T> SwitchIf(Func<T, bool> check, Func<T, Maybe<T>>? ifTrue = null, Func<T, Maybe<T>>? ifFalse = null) =>
		F.SwitchIf(this, check, ifTrue, ifFalse);

	/// <inheritdoc cref="F.SwitchIf{T}(Maybe{T}, Func{T, bool}, Func{T, IMsg})"/>
	public Maybe<T> SwitchIf(Func<T, bool> check, Func<T, IMsg> ifFalse) =>
		F.SwitchIf(this, check, ifFalse);

	#endregion Switch

	#region Unwrap

	/// <inheritdoc cref="F.Unwrap{T}(Maybe{T}, Func{IMsg, T})"/>
	public T Unwrap(T ifNone) =>
		F.Unwrap(this, ifNone: _ => ifNone);

	/// <inheritdoc cref="F.Unwrap{T}(Maybe{T}, Func{IMsg, T})"/>
	public T Unwrap(Func<T> ifNone) =>
		F.Unwrap(this, ifNone: _ => ifNone());

	/// <inheritdoc cref="F.Unwrap{T}(Maybe{T}, Func{IMsg, T})"/>
	public T Unwrap(Func<IMsg, T> ifNone) =>
		F.Unwrap(this, ifNone: ifNone);

	/// <inheritdoc cref="F.UnwrapSingle{T, TReturn}(Maybe{T}, Func{IMsg}?, Func{IMsg}?, Func{IMsg}?)"/>
	public Maybe<TSingle> UnwrapSingle<TSingle>(Func<IMsg>? noItems = null, Func<IMsg>? tooMany = null, Func<IMsg>? notAList = null) =>
		F.UnwrapSingle<T, TSingle>(this, noItems, tooMany, notAList);

	#endregion Unwrap
}
