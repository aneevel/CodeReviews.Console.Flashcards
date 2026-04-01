using System.ComponentModel.DataAnnotations;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;

public record ReadStudyStackDto([Required] string Name, [Required] int Id, [Required] ICollection<ReadFlashcardDto> Flashcards)
{
    public override string ToString()
    {
        return Name;
    }
}