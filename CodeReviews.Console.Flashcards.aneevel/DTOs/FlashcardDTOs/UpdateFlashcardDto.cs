using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

<<<<<<< HEAD
public record UpdateFlashcardDto([Required] int StudyStackId, [Required] string Front, [Required] string Back, [Required] int Id);
=======
public record UpdateFlashcardDto([Required] int StudyStackId, [Required] string FrontText, [Required] string BackText, [Required] int Id);
>>>>>>> 16cc63c ([feat]: implement module for Edit Flashcard)
