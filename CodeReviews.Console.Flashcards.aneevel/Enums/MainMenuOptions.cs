using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Enums;

internal enum MainMenuOptions
{
   [Display(Name="Enter Flashcard Module")]
   EnterFlashcardModule,
   [Display(Name="Enter Stacks Module")]
   EnterStacksModule,
   [Display(Name="Enter Study Sessions Module")]
   EnterStudySessionsModule,
   [Display(Name="Exit Application")]
   ExitApplication,
}