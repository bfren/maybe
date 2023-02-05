---
description: Welcome to a strange new world.
---

# Introduction

What if you could get away from huge classes with monster methods? What if you didn't have to worry about handling `null` values?

What if you could wrap values in a consistent way, have helpful feedback when something goes wrong, and chain small reusable testable reliable functions to provide stable functionality?

You can: in the world of `Maybe<T>`!

## What can it do?

To whet your appetite for what using `Maybe<T>` enables you to write, check out the following snippets - you'll notice that `Maybe<T>` supports mixing sync and async functions.

The first is from my website's `BlogController`. Each of the `GetXXX(query)` methods returns `Maybe<T>`, which are joined together via a Linq `SelectMany`, and used to build the model for the List view.

```csharp
ProcessOptionAsync(
    from posts in GetPostsAsync(query)
    from title in GetTitle(query)
    from sidebar in GetSidebarAsync(query)
    select new ListModel
    {
        Query = query,
        Title = title,
        Section = Section,
        Page = Page,
        Posts = posts,
        Sidebar = sidebar
    },
    m => View("List", m)
);
```

Or this, which builds a SQL query, executes it, and then processes the results:

```csharp
F.Some(opt)
    .Map(
        x => GetPostsQuery<TModel>(x),
        e => new M.ErrorGettingPostsQueryExceptionMsg(e)
    )
    .BindAsync(
        x => x.ExecuteQueryAsync()
    )
    .BindAsync(
        x => Process<List<TModel>, TModel>(x, filters)
    );
```

In both situations there _cannot_ be an unhandled exception, there _cannot_ be any `null` values, and if something does go wrong an object with a helpful error message is return.

## Key principles

In the world of `Maybe<T>` the following key principles are followed:

1. the return type is always `Maybe<T>` (rather than `Some<T>` or `None<T>`)
2. if a function returns `Maybe<T>` it means all exceptions have been handled
3. when returning `None<T>` we must give a reason
4. everything that can go wrong in your system has an `IMsg` which describes it
5. once in the async world, we must stay in the async world

If you haven't done any functional programming I suggest you read through the following, but if you want to see what `Maybe<T>` can do, you can go straight to \[\[Some and None|Some-and-None]].

## Why?

F# contains a built-in `Option` type, and I wanted to be able to code in that style but using C#. Over a year or so I have designed `Maybe<T>` to mimic some of F#'s behaviour using C# way. It works best when you chain pure (and small) functions together - and if you use it well your exception handler will have very little to do!

The other inspiration behind the type is Scott Wlaschin's [Railway Oriented Programming](https://fsharpforfunandprofit.com/posts/against-railway-oriented-programming/). I have limited experience of functional programming, so my `Maybe<T>` is not an implementation of his work, but it contains some similar ideas: in particular the 'failure' or 'sad' path, whereby if one function fails, the rest are skipped over.

## Concepts

The following serves as an introduction to the concepts behind `Maybe<T>` which are likely to be a little alien to most C# developers. However, if you want to skip ahead straight to the code, feel free!

### Pure functions

'Pure' functions have no effect outside themselves. In other words, they receive an input, act on it, and return an output. They don't affect state, and they don't affect the input object.

In the OO world of C#, I'll admit, this is odd. There's not really any such thing as a 'function' - at least not one that exists outside a class definition. However as far as I can, `Maybe<T>` is written as a series of pure functions, so even the methods in the `Maybe<T>` class and the extension methods are simply wrappers for the functions, which all live in the `MaybeF.F` static class.

### `Some<T>` and `None<T>`

`Maybe<T>` is an abstract class with two implementations. The return type for a function is _always_ `Maybe<T>`, but the actual object will be one of these:

* `Some<T>` which contains a `Value` property, of type `T`
* `None<T>` which contains a `Reason` property, of type `IMsg`

Think of `None<T>` as a more useful nullable, because it comes with a reason _why_ there is no value.

### No More Exceptions

In my projects - and I encourage you to follow the same discipline if you decide to use `Maybe<T>` - the convention is that **if a function returns `Maybe<T>` it has handled all its exceptions so you don't have to**.

This is a critical part of the usefulness of `Maybe<T>`, and to be honest if you prefer having and handling exceptions I suggest you stop reading! However, I do believe there is a better way...

## Function signatures

As `Maybe<T>` is a mix of OO and functional programming styles I will be using the following notation across the documentation: `function name (argument type,...) : return type`. For example `AddTwoNumbers(int, int) : int` means `AddTwoNumbers` takes two `integers` as input, and returns an `integer` value. Removing the name of the arguments makes the signatures shorter - I hope you find that helpful.

## LINQPad settings

All the code snippets are tested in LINQPad (v7.6, with .NET 7 support). If you want to try them out for yourself:

1. Add `MaybeF` NuGet package (Ctrl+Shift+P)
2. Add the following namespaces to the query (Ctrl+Shift+M):

```
MaybeF
MaybeF.Linq
```
