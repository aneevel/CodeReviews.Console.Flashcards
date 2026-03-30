using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;

public record CreateStudySessionDto([Required] int StudyStackId,
    [Required] int Score,
    [Required] DateTime Date);