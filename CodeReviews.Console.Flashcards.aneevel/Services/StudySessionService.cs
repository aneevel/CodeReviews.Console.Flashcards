using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

namespace CodeReviews.Console.Flashcards.aneevel.Services;

internal class StudySessionService : IStudySessionService
{
    public Task<int> CreateStudySessionAsync(CreateStudySessionDto studySessionDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReadStudySessionDto>> GetStudySessionsAsync()
    {
        throw new NotImplementedException();
    }
}