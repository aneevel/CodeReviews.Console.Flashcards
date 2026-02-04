using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Enums;

public enum FlashcardMenuOptions
{
   [Display(Name = "View All Flashcards")]
   ViewAllFlashcards,
   [Display(Name = "Add A Flashcard")]
   AddAFlashcard,
   [Display(Name = "Remove A Flashcard")]
   RemoveAFlashcard,
   [Display(Name = "Edit A Flashcard")]
   EditAFlashcard,
   [Display(Name = "Exit Flashcard Module")]
   ExitFlashcard,
}