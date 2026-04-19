using System.Data;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories;

public class StudySessionRepository(ConnectionString connectionString, IStudyStackRepository studyStackRepository, ILogger<StudySessionRepository> logger) : IStudySessionRepository
{
    public async Task<List<StudySession>> GetStudySessionsAsync()
    {
        try
        {
            await using SqlConnection connection = new(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand studySessionsCommand = new(
                "SELECT * FROM dbo.StudySessions", connection);

            List<StudySession> studySessions = [];
            await using SqlDataReader studySessionReader = await studySessionsCommand.ExecuteReaderAsync();
            while (await studySessionReader.ReadAsync())
            {
                DateTime studySessionDateTime = studySessionReader.GetDateTime("Date");
                int studySessionScore = studySessionReader.GetInt32("Score");
                int studyStackId = studySessionReader.GetInt32("StudyStackId");

                StudyStack? studyStack = await studyStackRepository.GetStudyStackAsync(studyStackId);

                studySessions.Add(
                    new StudySession
                    {
                        Date = studySessionDateTime,
                        Score = studySessionScore,
                        StudyStackId = studyStackId,
                        StudyStack = studyStack
                    });
            }

            return studySessions;
        }
        catch (Exception ex)
        {
           string errorMessage = $"""
                                   Class: {nameof(StudySessionRepository)}
                                   Method:  {nameof(GetStudySessionsAsync)}
                                   An error occurred trying to retrieve study sessions from the database: {ex.Message}
                                   """;
            logger.LogError(errorMessage);
            return [];
        }
    }

    public async Task<int> InsertStudySessionAsync(StudySession studySession)
    {
        try
        {
            await using SqlConnection connection = new(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new(
                $"INSERT INTO dbo.StudySessions (Date, Score, StudyStackId) VALUES (@Date, @Score, @StudyStackId)",
                connection);
            command.Parameters.AddWithValue("@StudyStackId", studySession.StudyStackId);
            command.Parameters.AddWithValue("@Score", studySession.Score);
            command.Parameters.AddWithValue("@Date", studySession.Date);
            return await command.ExecuteNonQueryAsync();
        }
        catch
            (Exception ex)
        {
           string errorMessage = $"""
                                   Class: {nameof(StudySessionRepository)}
                                   Method:  {nameof(InsertStudySessionAsync)}
                                   An error occurred during an attempt to insert a study session into the database: {ex.Message}
                                   """;
           logger.LogError(errorMessage);
           return -1;
        }
    }
}