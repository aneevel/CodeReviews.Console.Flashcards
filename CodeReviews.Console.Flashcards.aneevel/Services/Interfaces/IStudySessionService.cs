using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

internal interface IStudySessionService
{
    Task<int> CreateStudySessionAsync(CreateStudySessionDto studySessionDto);
    Task<List<ReadStudySessionDto>> GetStudySessionsAsync();
}