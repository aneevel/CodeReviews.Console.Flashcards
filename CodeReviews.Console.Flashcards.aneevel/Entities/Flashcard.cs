using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class Flashcard
{
    public int Id { get; init; }
    [Required]
    public required string FrontText  { get; set; }
    [Required]
    public required string BackText { get; set; }
    [Required]
    public required int StudyStackId { get; set; }
}