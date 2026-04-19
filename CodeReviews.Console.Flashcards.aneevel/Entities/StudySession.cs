using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

public class StudySession
{
    public int Id { get; }
    [Required]
    public DateTime Date { get; init; }
    [Required]
    public int Score { get; init; }

    public StudyStack? StudyStack { get; init; } = new StudyStack();
    public int StudyStackId { get; init; }
}