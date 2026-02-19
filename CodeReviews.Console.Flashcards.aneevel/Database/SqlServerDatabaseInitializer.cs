using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Database;

using Microsoft.Data.SqlClient;

internal class SqlServerDatabaseInitializer : IDatabaseInitializer
{
    private string _connectionString;

    public async Task InitializeDatabaseAsync()
    {
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                UserID = "sa",
                Password = "password1@",
                InitialCatalog = "master",
                TrustServerCertificate = true
            };

            _connectionString = builder.ConnectionString;
            await CreateTablesAsync();
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

    private async Task CreateTablesAsync()
    {
        try
        {

            await using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string studyStackSql = """
                                         IF OBJECT_ID('dbo.StudyStacks') IS NULL CREATE TABLE dbo.StudyStacks (
                                           StudyStackId INTEGER PRIMARY KEY IDENTITY(1, 1),
                                           Name VARCHAR(255) NOT NULL UNIQUE);
                                         """;

            await using SqlCommand stackCommand = new SqlCommand(studyStackSql, connection);
            await stackCommand.ExecuteNonQueryAsync();

            const string flashcardSql = """
                                        IF OBJECT_ID('dbo.Flashcards') IS NULL CREATE TABLE dbo.Flashcards (
                                                    FlashcardId INTEGER PRIMARY KEY IDENTITY(1, 1),
                                                    FrontText VARCHAR(255) NOT NULL,
                                                    BackText VARCHAR(255) NOT NULL,
                                                    StudyStackId INTEGER NOT NULL
                                                    CONSTRAINT FK_Flashcards_StudyStacks FOREIGN KEY (StudyStackId) 
                                                    REFERENCES dbo.StudyStacks (StudyStackId) 
                                                    ON DELETE CASCADE
                                                    ON UPDATE CASCADE
                                               );
                                        """;

            await using SqlCommand flashcardCommand = new SqlCommand(flashcardSql, connection);
            await flashcardCommand.ExecuteNonQueryAsync();

            const string studySessionSql = """
                                           IF OBJECT_ID('dbo.StudySessions') IS NULL CREATE TABLE dbo.StudySessions (
                                              StudySessionId INTEGER PRIMARY KEY IDENTITY(1, 1),
                                              Date DATETIME NOT NULL,
                                              Score INTEGER NOT NULL);
                                           """;

            await using SqlCommand studySessionCommand = new SqlCommand(studySessionSql, connection);
            await studySessionCommand.ExecuteNonQueryAsync();

            const string studySessionStackSql = """
                                                IF OBJECT_ID('dbo.StudySessionStacks') IS NULL CREATE TABLE dbo.StudySessionStacks (
                                                  StudySessionStackID INTEGER PRIMARY KEY IDENTITY(1, 1),
                                                  StudyStackID INTEGER NOT NULL,
                                                  StudySessionID INTEGER NOT NULL,
                                                  CONSTRAINT FK_StudySessionStacks_StudySessions FOREIGN KEY (StudySessionId)
                                                  REFERENCES dbo.StudySessions (StudySessionId),
                                                  CONSTRAINT FK_StudySessionStacks_StudyStacks FOREIGN KEY (StudyStackId)
                                                  REFERENCES dbo.StudyStacks (StudyStackId));
                                                """;

            await using SqlCommand studySessionStackCommand = new SqlCommand(studySessionStackSql, connection);
            await studySessionStackCommand.ExecuteNonQueryAsync();
        }
        catch (SqlException ex)
        {
            string errorMessage = $"""
                                   Class: {nameof(SqlServerDatabaseInitializer)}
                                   Method: {nameof(CreateTablesAsync)}
                                   There was an error building the database tables: {ex.Message}
                                   """;
            AnsiConsole.MarkupLine(errorMessage);
            throw;
        }
        catch (Exception ex)
        {
            string errorMessage = $"""
                                   Class: {nameof(SqlServerDatabaseInitializer)}
                                   Method: {nameof(CreateTablesAsync)}
                                   There was an error building the database tables: {ex.Message}
                                   """;
            AnsiConsole.MarkupLine(errorMessage);
            throw;
        }

    }

}