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
	/// Add a <see cref="IMaybeCache{TKey}"/> to <paramref name="this"/> with a Singleton lifetime
	/// </summary>
	/// <typeparam name="TKey">Cache Key type</typeparam>
	/// <param name="this"></param>
	public static IServiceCollection AddMaybeCache<TKey>(this IServiceCollection @this)
		where TKey : notnull =>
		@this.AddSingleton<IMaybeCache<TKey>, MaybeCache<TKey>>();
}
