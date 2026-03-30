using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;

internal interface IStudySessionRepository
{
    Task<List<StudySession>> GetStudySessionsAsync();
    Task<int> InsertStudySessionAsync(StudySession studySession);
}