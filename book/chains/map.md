---
description: Starting to work with Maybe inputs.
---

# Map

## Functional or fluent?

Once we have used `Some<T>(T) : Maybe<T>` to lift a value into the world of `Maybe<T>`, we need to do something with it by chaining our functions together. The `Maybe<T>` class comes with a load of methods which are actually wrappers for the functions that do the work.

So, although you can use the functions directly, you'll find it much more convenient to write chains using the common C# fluent syntax. These two are functionally identical:

```csharp
var maybe = F.Some(42);

// functional syntax
var functional = F.Map(maybe, x => x.ToString(), F.DefaultHandler);

// fluent syntax
var fluent = maybe.Map(x => x.ToString(), F.DefaultHandler);
```

In that snippet, `functional` and `fluent` are both `Some<string>` with Value `"42"`.  Both styles will work, but once you start getting into chaining operations, the functional style becomes incredibly clunky.

From now on, I will give function signatures as per the fluent syntax because that _should_ help keep things a little clearer, and it's what you will actually use most of the time.

## `Map<T, TReturn>(Func<T, TReturn>, Handler) : Maybe<TReturn>`

`Map` does a `switch` and behaves like this:

* if the input `Maybe<T>` is `None<T>`, return `None<TReturn>` with the original reason message
* if the input `Maybe<T>` is `Some<T>`...
  * get the Value `T`
  * use that as the input and execute `Func<T, TReturn>`, then wrap the result in `Some<TReturn>`
  * catch any exceptions using `Handler`

See it in action here:

```csharp
var result =
    F.Some(3)
        .Map( // x is 3
            x => x * 4,
            F.DefaultHandler
        )
        .Map( // x is 12
            x => x / 2,
            e => new M.DivisionFailedMsg(e)
        )
        .Map( // x is 6
            x => x * 7,
            F.DefaultHandler
        )
        .Map(
            x => $"The answer is {x}.",
            F.DefaultHandler
        )
        .Audit(
            some: val => Console.Write(val),
            none: msg => Console.Write(msg)
        );

public static class M
{
    public sealed record class DivisionFailedMsg(Exception Value) : IExceptionMsg { }
}
```

If you are using LINQPad to to you change the second `Map` in that sample so you have `x / 0` instead of `x / 2` and re-run the snippet, you will see that you still end up with an `Maybe<string>`, only this time it's `None<string>` with a `DivisionFailedMsg` as the reason message.

There are two additional things to note here:

1. `DefaultHandler` is available for you to use, but you should use it sparingly, and not rely on it, unless you really don't care about having helpful messages.
2. `Audit(Action<T> some, Action<IMsg> none)` is useful for logging - the 'some' action receives the current Value (if the input is `Some<T>`) and the 'none' action receives the current reason message (if the input is `None<T>`).

## Addendum

In case you weren't convinced earlier when I said you would be using the fluent style more than the functional style, this is what the example would look like:

```csharp
var result =
    F.Audit(
        F.Map(
            F.Map(
                F.Map(
                    F.Map(
                        F.Some(3),
                        x => x * 4, // x is 3
                        F.DefaultHandler
                    ),
                    x => x / 2, // x is 12
                    e => new M.DivisionFailedMsg(e)
                ),
                x => x * 7, // x is 6
                F.DefaultHandler
            ),
            x => $"The answer is {x}.",
            F.DefaultHandler
        ),
        any: null,
        some: val => Console.Write(val),
        none: msg => Console.Write(msg)
    );

public static class M
{
    public sealed record class DivisionFailedMsg(Exception Value) : IExceptionMsg { }
}
```

I suspect you'll agree that is pretty horrid! Personally I would prefer it if C# had a pipe operator, but it doesn't (and I imagine never will because that's what the fluent / LINQ-style syntax is for).
