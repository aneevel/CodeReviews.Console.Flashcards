using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Data.SqlClient;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories;

internal class StudySessionRepository(ConnectionString connectionString) : IStudySessionRepository
{
    public Task<List<StudySession>> GetStudySessionsAsync()
    {
        throw new NotImplementedException();
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