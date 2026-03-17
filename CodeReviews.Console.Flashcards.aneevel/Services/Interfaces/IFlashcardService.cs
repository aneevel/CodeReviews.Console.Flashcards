namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

internal interface IFlashcardService
{
   public Task AddFlashcardAsync(CreateFlashcardDto flashcardDto);
}