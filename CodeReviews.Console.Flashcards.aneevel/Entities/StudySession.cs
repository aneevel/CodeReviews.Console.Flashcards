using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class StudySession
{
    [Required]
    public int Id { get; }
    [Required]
    public DateTime Date { get; init; }
    [Required]
    public int Score { get; init; }

    public required int StudyStackId { get; init; }
}