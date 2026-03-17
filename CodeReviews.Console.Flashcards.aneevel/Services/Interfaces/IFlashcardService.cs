using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

internal interface IFlashcardService
{
   public Task AddFlashcardAsync(CreateFlashcardDto flashcardDto);
}