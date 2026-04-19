using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

public class ReadFlashcardDto()
{

    public ReadFlashcardDto(int id, string frontText, string backText) : this()
    {
        Id = id;
        FrontText = frontText;
        BackText = backText;
    }
    public override string ToString()
    {
        return $"Front: {FrontText}\n Back: {BackText}";
    }

    public int Id { get;  }
    public string FrontText { get; } = string.Empty;
    public string BackText { get; } = string.Empty;
}