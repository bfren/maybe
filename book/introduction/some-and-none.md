---
description: Lifting values into the world of Maybe<T>.
---

# Some and None

## `Some<T>(T) : Maybe<T>`

To lift a value into the world of `Maybe<T>` the main functions we use are all called **Some**.

The following snippets all do the same thing, which is wrap a value in `Some<T>` (you should be able to run this code in C# interactive):

```csharp
int number = 42;

// use a pure function to return a value as an Maybe<T>
var maybe = F.Some(number);

// use implicit operator - which calls F.Some()
var maybe = number;

// use an extension method (from MaybeF.Extensions) - which calls F.Some()
var maybe = number.Some();
```

In each case the variable `maybe` is of type `Maybe<int>`, but specifically `Some<int>`, which means you can do the following:

```csharp
if (maybe.IsSome(out var number))
{
    Console.Write("The answer to the question is '{0}'.", number);
}

// The answer to the question is '42'.
```

This is nice and simple, but what if your value comes from another function? Wouldn't it be nice if you could pass a `Func<T>` instead of a `T`, and wouldn't it be nice if it caught any exceptions for you, so don't have to wrap it in a `try..catch` block?

Yes, it would.

But first, we need to explore how we return `None<T>`

## `None<T, IMsg>() : Maybe<T>`

Where `Some<T>` wraps a value, `None<T>` is a null-safe way of representing no value, with a helpful reason _why_ there is no value: some input was invalid, an exception occurred, and so on. Note that **we always need to give a reason for a `None<T>`** - a [key principle](../#key-principles) in the world of `Maybe<T>`.

Let's dive right in!

```csharp
var none = Divide(42, 0);
if (none.IsNone(out var reason))
{
    Console.WriteLine("Something went wrong: {0}.", reason);
}

var some = Divide(42, 6);
if (some.IsSome(out var value))
{
    Console.WriteLine("The result is: {0}.", value);
}

// Something went wrong: YouCannotDivideByZeroMsg { }.
// The result is: 7.

Maybe<int> Divide(int x, int y)
{
    if (y == 0)
    {
        return F.None<int, M.YouCannotDivideByZeroMsg>();
    }

    return x / y;
}

public static class M
{
    public sealed record class YouCannotDivideByZeroMsg : IMsg { }
}
```

My pattern is for each class I write to have an `M` static class, which contains all the reasons relating to that class. That way we can follow the [key principle](../#key-principles) that **everything that can go wrong in an application has an IMsg**. (My practice is to implement my reason messages as `sealed record class` types - you don't have to do that.)

`MaybeF` comes with two message interfaces: `IMsg` (which has no properties or methods) and `IExceptionMsg` (which has one property: the exception). We'll explore exception handling in the next section - but another way of writing the code block above would be like this:

```csharp
var none = Divide(42, 0);
if (none.IsNone(out var reason))
{
    Console.WriteLine("Something went wrong: {0}.", reason);
}

var some = Divide(42, 6);
if (some.IsSome(out var value))
{
    Console.WriteLine("The result is: {0}.", value);
}

// Something went wrong: DivisionExceptionMsg { Value = ... }.
// The result is: 7.

Maybe<int> Divide(int x, int y)
{
    try
    {
        return x / y;
    }
    catch (Exception e)
    {
        return F.None<int>(new M.DivisionExceptionMsg(e));
    }
}

public static class M
{
    public sealed record class DivisionExceptionMsg(Exception Value) : IExceptionMsg { }
}
```

This time the reason message doesn't have a parameterless constructor so we can't use the function `None<T, IMsg>()` - instead we use `None<T>(IMsg) : Maybe<T>`.  This allows us to pass additional information to the reason message, in this case the exception we've caught.

## `Some<T>(Func<T>, Handler) : Maybe<T>`

A [key principle](../#key-principles) of `Maybe<T>` is that **we always handle our exceptions**. Therefore whenever we try to 'lift' a function instead of a value into the world of `Maybe<T>`, we need to catch things that go wrong.

This is where the delegate `F.Handler` comes in, well, handy. Here is the definition of `Handler`:

```csharp
public delegate IExceptionMsg Handler(Exception e);
```

It takes an `Exception` and returns an `IExceptionMsg`. The handler is used by `Some<T>(Func<T>, Handler)`, which creates a `None<T>` and adds the reason message created by the handler.

So, this snippet does exactly what the `Divide(int, int) : Maybe<int>` function did in the previous example, but without the `try..catch` block:

```csharp
int number = 42;
var maybe = F.Some(
    () => number / 0,
    e => new M.DivisionExceptionMsg(e)
);

if (maybe.IsNone(out var reason))
{
    Console.Write("Failed with {0}.", reason);
}

// Failed with M.DivisionExceptionMsg { Value = ... }.

public static class M
{
    public sealed record class DivisionExceptionMsg(Exception Value) : IExceptionMsg { }
}
```

## Reason Messages

Reason messages are a simple but powerful way of describing everything that can go wrong in your system. You could have your own namespace for them all, but I prefer to define messages next to the class that uses them.

To realise the true power of reason messages, you need to be disciplined about _never_ reusing them (or only rarely - I reuse mine only when I have two versions of the same function, for example sync / async).

This means:

* when you log a `None<T>` with its reason message, you know exactly where the problem occurred
* if you want to provide user feedback and translations, you can have specific error messages based on where the problem occurred

The messages we've used so far have been pretty simple, but here is an example:

```csharp
public sealed record class UserEmailAddressNotUpdatedMsg<T>(long UserId) : IMsg;
```

This reason message captures various important pieces of information:

* the type `T` is the type of the entity being updated
* `Method` is the name of the update method
* `Id` is the ID of the entity being updated

Then in an `UpdateUserEmailAddressAsync()` function you could do something like this:

```csharp
return rowsAffected switch
{
    1 =>
        F.True,

    _ =>
        F.None<bool>(new M.UserEmailAddressNotUpdatedMsg<TEntity>(user.Id))
}
```

## True and False

In that last code snippet you may have `True`. That isn't a typo - there are two properties of the static clas `MaybeF.F`:

* `F.True` which returns `Some<bool>` with Value `true`
* `F.False` which returns `Some<bool>` with Value `false`

They are identical to writing `F.Some(true)` and `F.Some(false)` - but I like the shorthands.

They exist for the times you don't want to return a `None<bool>` when something fails, but a `Some<bool>` with a `false` value, so you can continue processing.
