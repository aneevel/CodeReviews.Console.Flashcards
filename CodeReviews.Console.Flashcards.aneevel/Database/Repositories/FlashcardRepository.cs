using System.Data;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Data.SqlClient;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;

internal class FlashcardRepository(ConnectionString connectionString) : IFlashcardRepository
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
                    FrontText = reader.GetString("FrontText"), BackText = reader.GetString("BackText"),
                    StudyStackId = reader.GetInt32("StudyStackId")
                });
            }

            return flashcards;
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(FlashcardRepository)}\n" +
                                  $"Method: {nameof(InsertFlashcardAsync)}\n" +
                                  $"An error occurred during an attempt to add a new Flashcard to the database: {ex.Message}";
            throw new Exception(errorMessage, ex);
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
            throw new Exception(errorMessage, ex);
        }
    }

    public Task<int> UpdateFlashcardAsync(Flashcard flashcard)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteFlashcardAsync(Flashcard flashcard)
    {
        throw new NotImplementedException();
    }
}