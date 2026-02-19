using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class StudyStack
{
    public int StudyStackId { get; set; }
    [Required]
    public string Name { get; set; }

    public ICollection<Flashcard>? Flashcards { get; set; }
    // TODO: Add Study Sessions
}