using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Enums;

internal enum StudySessionMenuOptions
{
   [Display(Name = "View All Study Sessions")]
   ViewAllStudySessions,
   [Display(Name = "Start A Study Session")]
   StartAStudySession,
   [Display(Name = "Exit Study Session Module")]
   ExitStudySessionModule,
}