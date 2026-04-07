using System.Data;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories;

internal class FlashcardRepository(ConnectionString connectionString, ILogger<FlashcardRepository> logger) : IFlashcardRepository
{
    public async Task<List<Flashcard>> GetFlashcardsAsync()
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand("SELECT * FROM dbo.Flashcards", connection);

            List<Flashcard> flashcards = [];
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                flashcards.Add(new Flashcard
                {
                    Id = reader.GetInt32("Id"),
                    FrontText = reader.GetString("FrontText"),
                    BackText = reader.GetString("BackText"),
                    StudyStackId = reader.GetInt32("StudyStackId")
                });
            }

            return flashcards;
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(FlashcardRepository)}\n" +
                                  $"Method: {nameof(GetFlashcardsAsync)}\n" +
                                  $"An error occurred during an attempt to get Flashcards from the database: {ex.Message}";
            logger.LogError(errorMessage);
            return [];
        }
    }
    
    public async Task<List<Flashcard>> GetFlashcardsFromStudyStackAsync(int studyStackId)
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand("SELECT * FROM dbo.Flashcards WHERE StudyStackId = @StudyStackId", connection);
            command.Parameters.AddWithValue("@StudyStackId", studyStackId);

            List<Flashcard> flashcards = [];
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                flashcards.Add(new Flashcard
                {
                    Id = reader.GetInt32("Id"),
                    FrontText = reader.GetString("FrontText"),
                    BackText = reader.GetString("BackText"),
                    StudyStackId = reader.GetInt32("StudyStackId")
                });
            }

            return flashcards;
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(FlashcardRepository)}\n" +
                                  $"Method: {nameof(GetFlashcardsFromStudyStackAsync)}\n" +
                                  $"An error occurred during an attempt to get Flashcards for a Study Stack from the database: {ex.Message}";
            logger.LogError(errorMessage);
            return [];
        }
    }

    public async Task<int> InsertFlashcardAsync(Flashcard flashcard)
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand(
                $"INSERT INTO dbo.Flashcards (StudyStackId, FrontText, BackText) VALUES (@StudyStackId, @FrontText, @BackText)",
                connection);
            command.Parameters.AddWithValue("@StudyStackId", flashcard.StudyStackId);
            command.Parameters.AddWithValue("@FrontText", flashcard.FrontText);
            command.Parameters.AddWithValue("@BackText", flashcard.BackText);
            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(FlashcardRepository)}\n" +
                                  $"Method: {nameof(InsertFlashcardAsync)}\n" +
                                  $"An error occurred during an attempt to add a Flashcard to the database: {ex.Message}";
            logger.LogError(errorMessage);
            return -1;
        }
    }

    public async Task<int> UpdateFlashcardAsync(Flashcard flashcard)
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand(
                $"UPDATE dbo.Flashcards SET StudyStackId = @StudyStackId, FrontText = @FrontText, BackText = @BackText WHERE Id = @Id",
                connection);
            command.Parameters.AddWithValue("@StudyStackId", flashcard.StudyStackId);
            command.Parameters.AddWithValue("@FrontText", flashcard.FrontText);
            command.Parameters.AddWithValue("@BackText", flashcard.BackText);
            command.Parameters.AddWithValue("@Id", flashcard.Id);

            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(FlashcardRepository)}\n" +
                                  $"Method: {nameof(InsertFlashcardAsync)}\n" +
                                  $"An error occurred during an attempt to edit a flashcard in the database: {ex.Message}";
            logger.LogError(errorMessage);
            return -1;
        }
    }

    public async Task<int> DeleteFlashcardAsync(Flashcard flashcard)
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand(
                $"DELETE FROM dbo.Flashcards WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", flashcard.Id);
            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(FlashcardRepository)}\n" +
                                  $"Method: {nameof(DeleteFlashcardAsync)}\n" +
                                  $"An error occurred during an attempt to delete a flashcard in the database: {ex.Message}";
            logger.LogError(errorMessage);
            return -1;
        }
    }
}