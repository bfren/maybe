---
description: Short-circuiting when something goes wrong.
---

# Chains

## Isn't this an over-complicated mess?

It's all very well creating a lovely `Maybe<T>` with `Some<T>(T) : Maybe<T>`, but what do you do with it? Do you really want to have code like this everywhere:

```csharp
var maybe = F.Some(42);
if (maybe.IsNone(out var reason))
{
    // do something
}
else if (maybe.IsSome(out var value))
{
    // do something else
}
else
{
    // what do we do here??
}
```

Of course not! That's a mess.

## Principles

Instead we use functions to chain `Maybe<T>` results together, so the flow looks something like this:

```
Some<A> ->> Link<A, B> ->> Link<B, C> ->> Link<C, D> ->> Some<D>
```

If all the links work, each receives value A / B / C, returns value B / C / D, and we end up with `Some<D>`. This is the basic concept behind Scott Wlaschin's [Railway Oriented Programming](https://fsharpforfunandprofit.com/posts/against-railway-oriented-programming/). It's a different way of coding to OO / C#, because instead of writing a method that does a lot of things, we write several small functions, each of which does one thing, and then we chain them together. (In F# this is called composition, but we can't do that in C# so instead we use a 'fluent' style syntax.)

This is great - but what if something goes wrong? Then we want the flow to look something like this:

```
                 something goes wrong in this link
                /
               /
Some<A> --> Link<A, B> --> Link<B, C> --> Link<C, D> --> None<D>
                     \                                   /
                      \                                 /
                       - - - - - - - - - - - - - - - - -
                         short circuit the other links
```

Something goes wrong in the first link, and instead of a `Some<B>` to pass a value to the next link in the chain, we get a `None<B>` (with a reason message of course). All the next links in the chain are skipped, the `None<B>` is transformed into a `None<C>` and then to a `None<D>` (preserving the original reason message), and returned.

The chaining is handled by two function groups: `Map` and `Bind`. These do the switching for you:

* if the input is `Some<T>` they pass the value to your function
* if the input is `None<T>` they skip your function and return `None<TReturn>` (the next type), preserving the reason message
