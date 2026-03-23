using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

namespace CodeReviews.Console.Flashcards.aneevel.Services;

internal class FlashcardService(IFlashcardRepository repository) : IFlashcardService
{
    public async Task<int> CreateFlashcardAsync(CreateFlashcardDto flashcardDto)
    {
        return await repository.InsertFlashcardAsync(flashcardDto.FromCreateFlashcardDto());
    }

    public async Task<List<ReadFlashcardDto>> GetFlashcardsAsync()
    {
        List<Flashcard> flashcards = await repository.GetFlashcardsAsync();

        return flashcards
            .Select(flashcard => flashcard.FromFlashcard())
            .ToList();
    }

    public async Task<int> UpdateFlashcardAsync(UpdateFlashcardDto flashcardDto)
    {
        return await repository.UpdateFlashcardAsync(flashcardDto.FromUpdateFlashcardDto());
    }

    public async Task<int> DeleteFlashcardAsync(DeleteFlashcardDto flashcardDto)
    {
        return await repository.DeleteFlashcardAsync(flashcardDto.FromDeleteFlashcardDto());
    }
}