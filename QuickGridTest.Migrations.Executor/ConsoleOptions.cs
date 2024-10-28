using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace QuickGridTest.Migrations.Executor;

public class ConsoleOptions
{
    #region Public Properties

    [Option('c', "connectionString", Required = true, HelpText = "The connection string to the database")]
    public string ConnectionString { get; set; }

    /// <summary>
    /// Command timeout during migration. Default to 5 minutes if not provided
    /// </summary>
    [Option('o', "timeout", Default = 300, HelpText = "Command timeout during migration.")]
    public int CommandTimeout { get; private set; }

    #endregion
}