using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

public interface IStudySessionService
{
    Task<int> CreateStudySessionAsync(CreateStudySessionDto studySessionDto);
    Task<List<ReadStudySessionDto>> GetStudySessionsAsync();
}