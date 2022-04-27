// Maybe: .NET Monad.
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using Microsoft.Extensions.DependencyInjection;

namespace MaybeF.Caching;

/// <summary>
/// <see cref="ServiceCollection"/> extension methods
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Add a <see cref="IMaybeCache{TKey}"/> to <paramref name="this"/> with a Scoped lifetime
	/// </summary>
	/// <typeparam name="TKey">Cache Key type</typeparam>
	/// <param name="this"></param>
	public static IServiceCollection AddMaybeCache<TKey>(this IServiceCollection @this)
		where TKey : notnull =>
		AddMaybeCache<TKey>(@this, false);

	/// <summary>
	/// Add a <see cref="IMaybeCache{TKey}"/> to <paramref name="this"/> -<br/>
	///  - if <paramref name="singleton"/> is true it will have a Singleton lifetime
	///  - if <paramref name="singleton"/> is false it will have a Scoped lifetime
	/// </summary>
	/// <typeparam name="TKey">Cache Key type</typeparam>
	/// <param name="this"></param>
	/// <param name="singleton"></param>
	public static IServiceCollection AddMaybeCache<TKey>(this IServiceCollection @this, bool singleton)
		where TKey : notnull =>
		singleton switch
		{
			true =>
				@this.AddSingleton<IMaybeCache<TKey>, MaybeCache<TKey>>(),

			false =>
				@this.AddScoped<IMaybeCache<TKey>, MaybeCache<TKey>>(),
		};
}
