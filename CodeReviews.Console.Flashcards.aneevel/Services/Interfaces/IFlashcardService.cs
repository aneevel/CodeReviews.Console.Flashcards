using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

public interface IFlashcardService
{
   Task<int> CreateFlashcardAsync(CreateFlashcardDto flashcardDto);
   Task<List<ReadFlashcardDto>> GetFlashcardsAsync();
   Task<int> UpdateFlashcardAsync(UpdateFlashcardDto flashcardDto);
   Task<int> DeleteFlashcardAsync(DeleteFlashcardDto flashcardDto);

}