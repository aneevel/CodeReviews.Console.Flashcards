using CodeReviews.Console.Flashcards.aneevel.Entities;
using Microsoft.Data.SqlClient;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories;

internal class StudyStackRepository : IStudyStackRepository
{
    private readonly string _connectionString;
    public StudyStackRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StudyStack>> GetStudyStacksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StudyStack> GetStudyStackAsync(string stackName)
    {
        throw new NotImplementedException();
    }

    public async Task<int> InsertStudyStackAsync(StudyStack studyStack)
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand(
                $"INSERT INTO dbo.StudyStacks (Name) VALUES (@Name)", connection);
            command.Parameters.AddWithValue("@Name", studyStack.Name);
            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(StudyStackRepository)}\n" +
                            $"Method: {nameof(InsertStudyStackAsync)}\n" +
                            $"An error occurred during an attempt to add a Study Stack to the database: {ex.Message}";
            throw;
        }

    }

    public Task UpdateStudyStackAsync(StudyStack studyStack)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStudyStackAsync(StudyStack studyStack)
    {
        throw new NotImplementedException();
    }
}