using System.ComponentModel.DataAnnotations;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;

public record ReadStudySessionDto([Required] int Id, [Required] int StudyStackId, [Required] int Score, [Required] DateTime Date, [Required] ReadStudyStackDto ReadStudyStackDto);