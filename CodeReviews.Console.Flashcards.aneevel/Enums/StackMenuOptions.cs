using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Enums;

internal enum StackMenuOptions
{
   [Display(Name = "View All Stacks")]
   ViewAllStacks,
   [Display(Name = "Add A Stack")]
   AddAStack,
   [Display(Name = "Delete A Stack")]
   DeleteAStack,
   [Display(Name = "Edit A Stack")]
   EditAStack,
   [Display(Name = "Exit Stack Module")]
   ExitStackModule,
}