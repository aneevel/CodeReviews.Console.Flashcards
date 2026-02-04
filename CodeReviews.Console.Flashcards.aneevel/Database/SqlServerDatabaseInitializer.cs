using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Database;

using Microsoft.Data.SqlClient;

internal class SqlServerDatabaseInitializer : IDatabaseInitializer
{
    private readonly string _connectionString = "Data Source=localhost;Initial Catalog=master;User ID=sa;Password=Password!;TrustServerCertificate=True;";

    public SqlServerDatabaseInitializer()
    {
    }

    public async Task InitializeDatabaseAsync()
    {
        try
        {
            await CreateTables();
        }
        catch (SqlException ex)
        {
            string errorMessage = $"""
                                   Class: {nameof(SqlServerDatabaseInitializer)}
                                   Method: {nameof(InitializeDatabaseAsync)}
                                   There was an error initializing the database: {ex.Message}
                                   """;
            AnsiConsole.MarkupLine(errorMessage);
            throw;
        }
        catch (Exception ex)
        {
            string errorMessage = $"""
                                   Class: {nameof(SqlServerDatabaseInitializer)}
                                   Method: {nameof(InitializeDatabaseAsync)}
                                   There was an error initializing the database: {ex.Message}
                                   """;
            AnsiConsole.MarkupLine(errorMessage);
            throw;
        }
    }

    private async Task CreateTables()
    {
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string stackSql = """
                          IF OBJECT_ID('dbo.Stacks') IS NULL CREATE TABLE dbo.Stacks (
                            StackId INTEGER PRIMARY KEY IDENTITY(1, 1),
                            Name VARCHAR(255) NOT NULL);
                          """;

        await using SqlCommand stackCommand = new SqlCommand(stackSql, connection);
        await stackCommand.ExecuteNonQueryAsync();

        string flashcardSql = """
                     IF OBJECT_ID('dbo.Flashcards') IS NULL CREATE TABLE dbo.Flashcards (
                                 FlashcardId INTEGER PRIMARY KEY IDENTITY(1, 1),
                                 FrontText VARCHAR(255) NOT NULL,
                                 BackText VARCHAR(255) NOT NULL,
                                 StackId INTEGER NOT NULL
                                 CONSTRAINT FK_Flashcards_Stacks FOREIGN KEY (StackId) 
                                 REFERENCES dbo.Stacks (StackId) 
                                 ON DELETE CASCADE
                                 ON UPDATE CASCADE
                            );
                     """;

        await using SqlCommand flashcardCommand = new SqlCommand(flashcardSql, connection);
        await flashcardCommand.ExecuteNonQueryAsync();

        string studySessionSql = """
                                 IF OBJECT_ID('dbo.StudySessions') IS NULL CREATE TABLE dbo.StudySessions (
                                    StudySessionId INTEGER PRIMARY KEY IDENTITY(1, 1),
                                    Date DATETIME NOT NULL,
                                    Score INTEGER NOT NULL);
                                 """;

        await using SqlCommand studySessionCommand = new SqlCommand(studySessionSql, connection);
        await studySessionCommand.ExecuteNonQueryAsync();

        string studySessionStackSql = """
                                      IF OBJECT_ID('dbo.StudySessionStacks') IS NULL CREATE TABLE dbo.StudySessionStacks (
                                        StudySessionStackID INTEGER PRIMARY KEY IDENTITY(1, 1),
                                        StackID INTEGER NOT NULL,
                                        StudySessionID INTEGER NOT NULL,
                                        CONSTRAINT FK_StudySessionStacks_StudySessions FOREIGN KEY (StudySessionId)
                                        REFERENCES dbo.StudySessions (StudySessionId),
                                        CONSTRAINT FK_StudySessionStacks_Stacks FOREIGN KEY (StackId)
                                        REFERENCES dbo.Stacks (StackId));
                                      """;

        await using SqlCommand studySessionStackCommand = new SqlCommand(studySessionStackSql, connection);
        await studySessionStackCommand.ExecuteNonQueryAsync();
    }

}