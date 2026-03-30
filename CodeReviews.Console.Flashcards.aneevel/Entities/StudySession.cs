using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class StudySession
{
    [Required]
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int Score { get; set; }

    public required StudyStack StudyStack { get; set; }
}