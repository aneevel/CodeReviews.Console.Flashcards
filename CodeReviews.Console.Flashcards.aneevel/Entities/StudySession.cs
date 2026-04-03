using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class StudySession
{
    public int Id { get; }
    [Required]
    public DateTime Date { get; init; }
    [Required]
    public int Score { get; init; }

    public StudyStack? StudyStack { get; init; }
    public int StudyStackId { get; init; }
}