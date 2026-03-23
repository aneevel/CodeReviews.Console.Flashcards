using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;

public record ReadStudyStackDto([Required] string? Name, [Required] int Id)
{
    public override string ToString()
    {
        return Name;
    }
}