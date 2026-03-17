using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class Flashcard
{
    [Required]
    public int Id { get; init; }
    [Required]
    public required string FrontText  { get; init; }
    [Required]
    public required string BackText { get; init; }
    [Required]
    public required int StudyStackId { get; init; }
}