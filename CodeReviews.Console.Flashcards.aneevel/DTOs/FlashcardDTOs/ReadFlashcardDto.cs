using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

public record ReadFlashcardDto([Required] int Id, [Required] string FrontText, [Required] string BackText)
{
    public override string ToString()
    {
        return $"Front: {FrontText}\n Back: {BackText}";
    }
}