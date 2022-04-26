// Maybe: Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2019

namespace MaybeF.F_Tests;

public class Handler_Tests
{
	[Fact]
	public void If_LogAuditExceptions_Is_Not_Null_Calls_With_Exception()
	{
		// Arrange
		var log = Substitute.For<F.Logger>();
		var exception = new Exception();

		// Act
		F.HandleException(exception, log, Console.Out);

		// Assert
		log.Received().Invoke(exception);
	}

	[Fact]
	public void If_LogAuditExceptions_Is_Null_Writes_To_Console()
	{
		// Arrange
		var message = Rnd.Str;
		var exception = new Exception(message);
		var writer = Substitute.For<TextWriter>();

		// Act
		F.HandleException(exception, null, writer);

		// Assert
		writer.Received().WriteLine("Error: {0}", exception);
	}

	[Fact]
	public void Writes_To_LogAuditExceptions()
	{
		// Arrange
		var handler = Substitute.For<F.Logger>();
		F.DefaultLogger = handler;
		var exception = new Exception();

		// Act
		F.LogException(exception);

		// Assert
		handler.Received().Invoke(exception);

		// Reset
		F.DefaultLogger = null;
	}
}
