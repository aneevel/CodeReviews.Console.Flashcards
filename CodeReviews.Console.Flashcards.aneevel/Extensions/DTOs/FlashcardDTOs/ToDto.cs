using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.FlashcardDTOs;

internal static class ToDto
{
    extension(Flashcard flashcard)
    {
        public ReadFlashcardDto FromFlashcard()
        {
            return new ReadFlashcardDto(flashcard.Id, flashcard.FrontText, flashcard.BackText);
        }
    }
}