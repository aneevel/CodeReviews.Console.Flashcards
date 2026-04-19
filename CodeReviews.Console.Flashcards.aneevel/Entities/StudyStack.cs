using System.ComponentModel.DataAnnotations;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Entities;

public class StudyStack
{
    public int Id { get; init; }
    
    public string? Name { get; init; }

    public ICollection<Flashcard>? Flashcards { get; init; } = new List<Flashcard>();
}