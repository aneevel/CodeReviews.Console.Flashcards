using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs;

public record CreateStudyStackDto(
    [Required] string Name);