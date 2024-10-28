using System;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using QuickGridTest.Data.Contexts;

namespace QuickGridTest.Migrations.Executor;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ParserResult<ConsoleOptions> result;
        using (var parser = new Parser(SetParserSettings))
        {
            result = parser.ParseArguments<ConsoleOptions>(args);
        }

        await result.MapResult(ExecuteActionAsync,
            err => Task.FromResult(-1)).ConfigureAwait(false);
    }

    private static async Task ExecuteActionAsync(ConsoleOptions options)
    {
        var dbContext = GetDbContext(options.CommandTimeout, options.ConnectionString);
        var migrator = dbContext.GetService<IMigrator>();
        await migrator.MigrateAsync().ConfigureAwait(false);
    }

    private static QuickGridDbContext GetDbContext(int commandTimeout, string currentDatabaseConnectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<QuickGridDbContext>();
        optionsBuilder.UseSqlServer(currentDatabaseConnectionString, sqlServerOptions => sqlServerOptions.MaxBatchSize(128).EnableRetryOnFailure());
        var dbContext = new QuickGridDbContext(optionsBuilder.Options);
        dbContext.Database.SetCommandTimeout(TimeSpan.FromSeconds(commandTimeout));
        return dbContext;
    }

    /// <summary>
    /// Define the settings for the parser.
    /// </summary>
    /// <param name="settings"></param>
    private static void SetParserSettings(ParserSettings settings)
    {
        settings.CaseSensitive = false;
        settings.HelpWriter = Console.Out;
        settings.EnableDashDash = true;
    }
}