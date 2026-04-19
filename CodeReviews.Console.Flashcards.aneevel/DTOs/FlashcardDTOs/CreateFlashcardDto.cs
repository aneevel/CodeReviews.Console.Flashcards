using System.ComponentModel.DataAnnotations;
namespace CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

public class CreateFlashcardDto()
{
    public CreateFlashcardDto(int selectedStackId, string newFlashcardFrontText, string newFlashcardBackText) : this()
    {
        StudyStackId = selectedStackId;
        FrontText = newFlashcardFrontText;
        BackText = newFlashcardBackText;
    }

    public int StudyStackId {  get; }
    public string FrontText { get; } = string.Empty;
    public string BackText { get; } = string.Empty;
}