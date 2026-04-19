using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

namespace CodeReviews.Console.Flashcards.aneevel.Services;

public class StudySessionService(IStudySessionRepository repository) : IStudySessionService
{
    public async Task<int> CreateStudySessionAsync(CreateStudySessionDto studySessionDto)
    {
        return await repository.InsertStudySessionAsync(studySessionDto.FromCreateStudySessionDto());
    }

    public async Task<List<ReadStudySessionDto>> GetStudySessionsAsync()
    {
        List<StudySession> studySessions = await repository.GetStudySessionsAsync();

        return studySessions
            .Select(studySession => studySession.FromStudySession())
            .ToList();
    }
}