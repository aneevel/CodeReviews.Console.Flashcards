using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Enums;

internal enum FlashcardMenuOptions
{
   [Display(Name = "View All Flashcards")]
   ViewAllFlashcards,
   [Display(Name = "Create A Flashcard")]
   CreateAFlashcard,
   [Display(Name = "Remove A Flashcard")]
   RemoveAFlashcard,
   [Display(Name = "Edit A Flashcard")]
   EditAFlashcard,
   [Display(Name = "Exit Flashcard Module")]
   ExitFlashcardModule,
}