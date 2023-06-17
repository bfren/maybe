// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using MaybeF;

namespace Abstracts.Enumerable;

public abstract class IterateAsync_Tests
{
	public abstract Task Test00_List_Is_Empty_Does_Nothing();

	protected async Task Test00(Func<IEnumerable<Maybe<string>>, Func<string, Task>, Task> act)
	{
		// Arrange
		var list = System.Linq.Enumerable.Empty<Maybe<string>>();
		var f = Substitute.For<Func<string, Task>>();

		// Act
		await act(list, f);

		// Assert
		await f.DidNotReceiveWithAnyArgs().Invoke(default!);
	}

	public abstract Task Test01_Ignores_None_Values();

	protected async Task Test01(Func<IEnumerable<Maybe<string>>, Func<string, Task>, Task> act)
	{
		// Arrange
		var list = new[] { Create.None<string>(), F.Some(Rnd.Str), Create.None<string>() };
		var f = Substitute.For<Func<string, Task>>();

		// Act
		await act(list, f);

		// Assert
		await f.ReceivedWithAnyArgs(1).Invoke(default!);
	}

	public abstract Task Test02_Runs_Func_For_Some_Values();

	protected async Task Test02(Func<IEnumerable<Maybe<string>>, Func<string, Task>, Task> act)
	{
		// Arrange
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;
		var v2 = Rnd.Str;
		var list = new[] { F.Some(v0), F.Some(v1), F.Some(v2) };
		var f = Substitute.For<Func<string, Task>>();

		// Act
		await act(list, f);

		// Assert
		await f.Received(1).Invoke(v0);
		await f.Received(1).Invoke(v1);
		await f.Received(1).Invoke(v2);
	}
}
