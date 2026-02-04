using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Database;

using Microsoft.Data.SqlClient;

internal class SqlServerDatabaseInitializer : IDatabaseInitializer
{
    private readonly string _connectionString = "Server=localhost;Database=master;Integrated Security=SSPI;";

    public SqlServerDatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitializeDatabaseAsync()
    {
        try
        {
            string masterString = new SqlConnectionStringBuilder(_connectionString)
            {
                InitialCatalog = "master"
            }.ConnectionString;

            await using SqlConnection connection = new SqlConnection(masterString);
            await connection.OpenAsync();
        }
        catch (SqlException ex)
        {
            string errorMessage = $"""
                                   \nClass: {nameof(SqlServerDatabaseInitializer)}
                                   Method: {nameof(InitializeDatabaseAsync)}
                                   There was an error initializing the database: {ex.Message}
                                   """;
            AnsiConsole.MarkupLine(errorMessage);
            throw;
        }
    }

}