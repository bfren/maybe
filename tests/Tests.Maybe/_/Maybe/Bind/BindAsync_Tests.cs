﻿// Maybe Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

using System.Threading.Tasks;

namespace Maybe.Maybe_Tests;

public class BindAsync_Tests : Tests.Maybe.Abstracts.BindAsync_Tests
{
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionReason()
	{
		await Test00((mbe, bind) => mbe.BindAsync(bind)).ConfigureAwait(false);
	}

	public override async Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionReason()
	{
		await Test01((mbe, bind) => mbe.BindAsync(bind)).ConfigureAwait(false);
	}

	public override async Task Test02_If_None_Gets_None()
	{
		await Test02((mbe, bind) => mbe.BindAsync(bind)).ConfigureAwait(false);
	}

	public override async Task Test03_If_None_With_Reason_Gets_None_With_Same_Reason()
	{
		await Test03((mbe, bind) => mbe.BindAsync(bind)).ConfigureAwait(false);
	}

	public override async Task Test04_If_Some_Runs_Bind_Function()
	{
		await Test04((mbe, bind) => mbe.BindAsync(bind)).ConfigureAwait(false);
	}
}
