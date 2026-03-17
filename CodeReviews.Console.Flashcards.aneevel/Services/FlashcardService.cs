using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.FlashcardDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

internal class FlashcardService(IFlashcardRepository repository) : IFlashcardService
{
    public async Task<int> CreateFlashcardAsync(CreateFlashcardDto flashcardDto)
    {
        return await repository.InsertFlashcardAsync(flashcardDto.FromCreateFlashcardDto());
    }
}