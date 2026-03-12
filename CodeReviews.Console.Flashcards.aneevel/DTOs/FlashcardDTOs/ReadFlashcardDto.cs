using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

public record ReadFlashcardDto([Required] string Front, [Required] string Back)
{
    public override string ToString()
    {
        return $"Front: {Front}\n, Back: {Back}";
    }
}