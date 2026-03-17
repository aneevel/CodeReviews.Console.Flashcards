using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Enums;

internal enum StackMenuOptions
{
   [Display(Name = "View All Stacks")]
   ViewAllStacks,
   [Display(Name = "Create A Stack")]
   CreateAStack,
   [Display(Name = "Delete A Stack")]
   DeleteAStack,
   [Display(Name = "Edit A Stack")]
   EditAStack,
   [Display(Name = "Exit Stack Module")]
   ExitStackModule,
}