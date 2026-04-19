using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

public class UpdateFlashcardDto()
{
    public UpdateFlashcardDto(int studyStackId, string frontText, string backText, int id) : this()
    {
        StudyStackId = studyStackId;
        FrontText = frontText;
        BackText = backText;
        Id = id;
    }

    public int StudyStackId { get; }
    public string FrontText { get; } = string.Empty;
    public string BackText { get; } = string.Empty;
    public int Id { get; }
}