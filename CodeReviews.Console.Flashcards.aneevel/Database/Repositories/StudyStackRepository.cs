using System.Data;
using System.Diagnostics;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Data.SqlClient;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories;

internal class StudyStackRepository(ConnectionString connectionString) : IStudyStackRepository
{
    public async Task<List<StudyStack>> GetStudyStacksAsync()
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand(
                $"SELECT * FROM dbo.StudyStacks", connection);

            List<StudyStack> studyStacks = [];
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                studyStacks.Add(new StudyStack { Name = reader.GetString("Name"), Id = reader.GetInt32("Id") });
            }

            return studyStacks;
        }
        catch (Exception ex)
        {
           string errorMessage = $"\nClass: {nameof(StudyStackRepository)}\n" +
                            $"Method: {nameof(GetStudyStacksAsync)}\n" +
                            $"An error occurred during an attempt to get Study Stacks from the database: {ex}";
           // TODO: Log
           // ? Return what??? TODO: Remove
            throw new Exception(errorMessage, ex);
        }
    }

    public async Task<int> InsertStudyStackAsync(StudyStack studyStack)
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString.Value);
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
            // TODO: Should be error logger
            return -1;
        }

    }

    public async Task<int> UpdateStudyStackAsync(StudyStack studyStack)
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand(
                $"UPDATE dbo.StudyStacks SET Name = @Name WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Name", studyStack.Name);
            command.Parameters.AddWithValue("@Id", studyStack.Id);
            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(StudyStackRepository)}\n" +
                                  $"Method: {nameof(UpdateStudyStackAsync)}\n" +
                                  $"An error occurred during an attempt to update a Study Stack to the database: {ex.Message}";
            // TODO: Log
            return -1;
        }
    }

    public async Task<int> DeleteStudyStackAsync(StudyStack studyStack)
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand(
                $"DELETE FROM dbo.StudyStacks WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", studyStack.Id);
            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(StudyStackRepository)}\n" +
                                  $"Method: {nameof(DeleteStudyStackAsync)}\n" +
                                  $"An error occurred during an attempt to delete a Study Stack from the database: {ex.Message}";
            throw new Exception(errorMessage, ex);
        }
    }
}