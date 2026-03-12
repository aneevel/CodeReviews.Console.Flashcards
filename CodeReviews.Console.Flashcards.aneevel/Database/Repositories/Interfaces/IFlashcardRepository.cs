using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;

internal interface IFlashcardRepository
{
    Task<List<Flashcard>> GetFlashcardsAsync();
    Task<int> InsertFlashcardAsync(Flashcard flashcard);
    Task<int> UpdateFlashcardAsync(Flashcard flashcard);
    Task<int> DeleteFlashcardAsync(Flashcard flashcard);
}