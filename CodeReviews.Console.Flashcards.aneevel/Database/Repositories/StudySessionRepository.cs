using System.Data;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Data.SqlClient;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories;

internal class StudySessionRepository(ConnectionString connectionString, IStudyStackRepository studyStackRepository) : IStudySessionRepository
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
                int studySessionId = studySessionReader.GetInt32("Id");
                DateTime studySessionDateTime = studySessionReader.GetDateTime("Date");
                int studySessionScore = studySessionReader.GetInt32("Score");
                int studyStackId = studySessionReader.GetInt32("StudyStackId");

                StudyStack studyStack = await studyStackRepository.GetStudyStack(studyStackId);

                studySessions.Add(
                    new StudySession
                    {
                        Date = studySessionDateTime,
                        Score = studySessionScore,
                        StudyStackId = studyStackId,
                    });
            }

            return studySessions;
        }
        catch (Exception ex)
        {
            // TODO: Do some real logging
            throw new Exception("NOT IMPLEMENTED", ex);
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
            // TODO: Should be error logger
            return -1;
        }
    }
}