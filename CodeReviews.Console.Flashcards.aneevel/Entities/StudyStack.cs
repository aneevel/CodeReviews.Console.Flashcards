using System.ComponentModel.DataAnnotations;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class StudyStack
{
    [Required]
    public int Id { get; init; }
    
    public string? Name { get; init; }

    public ICollection<Flashcard>? Flashcards { get; init; }
    // TODO: Add Study Sessions
}