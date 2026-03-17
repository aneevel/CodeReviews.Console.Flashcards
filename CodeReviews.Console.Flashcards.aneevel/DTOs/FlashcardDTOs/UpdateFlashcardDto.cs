using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

public record UpdateFlashcardDto([Required] int StudyStackId, [Required] string Front, [Required] string Back, [Required] int Id);