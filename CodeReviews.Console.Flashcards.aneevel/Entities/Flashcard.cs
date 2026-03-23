using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class Flashcard
{
    [Required]
    public int Id { get; init; }
    public string? FrontText  { get; init; }
    public string? BackText { get; init; }
    public int? StudyStackId { get; init; }
}