using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;

public record UpdateStudyStackDto([Required] string Name, [Required] int Id);