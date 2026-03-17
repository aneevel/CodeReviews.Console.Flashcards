using System.ComponentModel.DataAnnotations;
namespace CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

public record CreateFlashcardDto(
    [Required] int StudyStackId,
    [Required] string FrontText,
    [Required] string BackText);