using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class StudyStack
{
    [Required]
    public int Id { get; init; }
    public string? Name { get; init; }

    public ICollection<Flashcard>? Flashcards { get; set; }
    // TODO: Add Study Sessions
}