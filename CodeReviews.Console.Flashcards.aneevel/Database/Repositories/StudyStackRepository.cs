using System.Data;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories;

internal class StudyStackRepository(ConnectionString connectionString, IFlashcardRepository flashcardRepository, ILogger<StudyStackRepository> logger) : IStudyStackRepository
{
    public async Task<StudyStack?> GetStudyStackAsync(int id)
    {
        try
        {
            await using SqlConnection connection = new(connectionString.Value); await connection.OpenAsync();

            await using SqlCommand command = new("SELECT * FROM dbo.StuyStacks WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            StudyStack? studyStack = null;
            while (await reader.ReadAsync())
            {
                int studyStackId = reader.GetInt32("Id");
                string studyStackName = reader.GetString("Name");

                List<Flashcard> studyStackFlashcards =
                    await flashcardRepository.GetFlashcardsFromStudyStackAsync(studyStackId);

                studyStack = new StudyStack
                {
                    Id = studyStackId,
                    Name = studyStackName,
                    Flashcards = studyStackFlashcards
                };

            }
            return studyStack;
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(StudyStackRepository)}\n" +
                                  $"Method: {nameof(GetStudyStackAsync)}\n" +
                                  $"An error occurred during an attempt to get a Study Stack with id {id} from the database: {ex.Message}";
            logger.LogError(errorMessage);
            return null;
        }
    }
    public async Task<List<StudyStack>> GetStudyStacksAsync()
    {
        try
        {
            await using SqlConnection connection = new(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand studyStackCommand = new(
                $"SELECT * FROM dbo.StudySacks", connection);

            List<StudyStack> studyStacks = [];
            await using SqlDataReader studyStackReader = await studyStackCommand.ExecuteReaderAsync();
            while (await studyStackReader.ReadAsync())
            {
                int studyStackId = studyStackReader.GetInt32("Id");
                string studyStackName = studyStackReader.GetString("Name");

                List<Flashcard> studyStackFlashcards = await flashcardRepository.GetFlashcardsFromStudyStackAsync(studyStackId);
                
                studyStacks.Add(
                    new StudyStack
                    {
                        Name = studyStackName,
                        Id = studyStackId,
                        Flashcards = studyStackFlashcards
                    });
            }

            return studyStacks;
        }
        catch (Exception ex)
        {
           string errorMessage = $"\nClass: {nameof(StudyStackRepository)}\n" +
                            $"Method: {nameof(GetStudyStacksAsync)}\n" +
                            $"An error occurred during an attempt to get Study Stacks from the database: {ex.Message}";
           logger.LogError(errorMessage);
           return [];
        }
    }

    public async Task<int> InsertStudyStackAsync(StudyStack studyStack)
    {
        try
        {
            await using SqlConnection connection = new(connectionString.Value);
            await connection.OpenAsync();

            await using SqlCommand command = new(
                $"INSERT INTO dbo.StdyStacks (Name) VALUES (@Name)", connection);
            command.Parameters.AddWithValue("@Name", studyStack.Name);
            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(StudyStackRepository)}\n" +
                            $"Method: {nameof(InsertStudyStackAsync)}\n" +
                            $"An error occurred during an attempt to add a Study Stack to the database: {ex.Message}";
            logger.LogError(errorMessage);
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
                $"UPDATE dbo.StudyStcks SET Name = @Name WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Name", studyStack.Name);
            command.Parameters.AddWithValue("@Id", studyStack.Id);
            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(StudyStackRepository)}\n" +
                                  $"Method: {nameof(UpdateStudyStackAsync)}\n" +
                                  $"An error occurred during an attempt to update a Study Stack to the database: {ex.Message}";
            logger.LogError(errorMessage);
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
                $"DELETE FROM dbo.StudyStcks WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", studyStack.Id);
            return await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            string errorMessage = $"\nClass: {nameof(StudyStackRepository)}\n" +
                                  $"Method: {nameof(DeleteStudyStackAsync)}\n" +
                                  $"An error occurred during an attempt to delete a Study Stack from the database: {ex.Message}";
            logger.LogError(errorMessage);
            return -1;
        }
    }
}