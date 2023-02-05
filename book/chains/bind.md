---
description: Where things start to get really useful.
---

# Bind

## What's the difference?

Like `Map`, the `Bind` functions receive the value of the input `Maybe<T>` if it's a `Some<T>`, and are bypassed if it's a `None<T>`.

However, there are two important differences:

1. `Bind` functions return an `Maybe<T>` (`Map` functions return `T` which is wrapped by `Some`).
2. Therefore they are expected to handle their own exceptions - one of our key principles is the contract that **if a function returns `Maybe<T>` its exceptions have been handled**. (The method signature therefore does not have a `Handler` in it.)

That said, you can be naughty because your binding function is still wrapped in a `try..catch` block. However _all_ exceptions caught in that block will return `None<T>` with a `M.UnhandledExceptionMsg` as the reason message.

## `Bind<T, TReturn>(Func<T, Maybe<TReturn>>) : Maybe<TReturn>`

`Bind` does a `switch` and behaves like this:

* if the input `Maybe<T>` is `None<T>`, return `None<TReturn>` with the original reason message
* if the input `Maybe<T>` is `Some<T>`...
  * get the Value `T`
  * use that as the input and execute `Func<T, Maybe<TReturn>>`, then return the result
  * catch any unhandled exceptions using `DefaultHandler`

See it in action here:

```csharp
var result =
    F.Some(3)
        .Bind( // x is 3
            x => F.Some(x * 4)
        )
        .Bind( // x is 12
            x => F.Some(x / 2)
        )
        .Bind( // x is 6
            x => F.Some(x * 7)
        )
        .Bind( // x is 42
            x => F.Some($"The answer is {x}.")
        )
        .Audit(
            some: val => Console.Write(val),
            none: msg => Console.Write(msg)
        );
```

This is identical to the previous snippet at the end of the [Map](map.md) section - except there are no exception handling options. So, if you change `x / 2` to `x / 0` you will get an `UnhandledExceptionMsg`.

However, binding comes into its own in more complex scenarios, for example data access:

```csharp
F.Some(cart)
    .Bind( // retrieve customer details (returns Customer object)
        cart => db.GetCustomer(cart.EmailAddress)
    )
    .Bind( // insert the new order (returns new Order ID)
        customer => db.InsertOrder(customer, cart)
    )
    .Bind( // retrieve full order details (returns Order object)
        db.GetOrderById
    )
    .Audit( // send confirmation email or log that the order failed
        some: mailer.SendOrderConfirmationEmail,
        none: notifier.OrderFailed
    )
    .Map( // if everything has succeeded, return a DTO to show the order details
        order => new OrderPlacedModel
        {
            Id = order.Id,
            Products = order.Products
        },
        DefaultHandler
    );

// result is Maybe<OrderPlacedModel>
```

If `db.GetCustomer()` fails, the next two operations are skipped, `Audit` is run with the `none` option, and a `None<OrderPlacedModel>` is returned with the reason message from `db.GetCustomer()`.

You can of course also return Tuples instead of single values, and C# is clever enough to give Tuples automatic names now as well:

```csharp
F.Some(customerId)
    .Bind(
        db.GetCustomer
    )
    .Bind(
        customer =>
        {
            var orderId = db.InsertCustomerOrder(customer, order);
            return (customer, orderId)
        }
    )
    .Bind(
        x =>
        {
            var order = db.GetOrderById(x.orderId);
            return (customer, order);
        }
    );

// result is Maybe<(CustomerEntity, OrderEntity)>
```

## Adding it all together

In the next section we will actually make a simple test app with everything you need to play around with `Map` and `Bind`. Before then here is an example of what this can look like in practice:

```csharp
// get the terms for a given taxonomy
async Task<Maybe<List<T>>> GetTaxonomyAsync<T>(Taxonomy taxonomy)
    where T : ITermWithRealCount
{
    using var w = await Db.StartWorkAsync();

    return await
        F.Some(
            () => GetQuery(Db, taxonomy), // lift a function instead of a value
            e => new M.GetTaxonomyQueryExceptionMsg(e)
        )
        .BindAsync(
            query => ExecuteQueryAsync(w, query, taxonomy)
        )
        .MapAsync(
            x => x.ToList(), // a simple lambda function is fine for this
            F.DefaultHandler // no need for a custom error message here
        );
}

// returns a simple string - but things can still go wrong
static string GetQuery(IWpDb db, Taxonomy taxonomy) =>
    $"SELECT * FROM {db.Taxonomy} WHERE {taxonomy} ...";

// returns an Maybe<T> so we use Bind() and assume it has handled its exceptions
static Task<Maybe<IEnumerable<T>>> ExecuteQueryAsync(IUnitOfWork w, string query, Taxonomy taxonomy) =>
    w.QueryAsync<T>(query, new { taxonomy });
```

Here we have two potentially problem-ridden methods - returning a complex SQL query from a given input and executing a database query - that are now **pure functions**: notice they are declared `static`. This is best practice, think of it like Dependency Injection but _functional_. The functions require no state, and interact with nothing else, so given the same input they will _always_ return the same output.

This means they can be tested easily, and once they work correctly they will _always_ work in the same way so you can rely on them.

You will also notice the use of the `-Async` variations of `Map` and `Bind` - we will come to these later, but for now note that although `x.ToList()` is not doing anything asynchronously, because the _input_ is a `Task<Maybe<T>>` we must use the `-Async` variant. This leads to another of our [key principles](../#key-principles): **once we are in the async world we stay in the async world**.
