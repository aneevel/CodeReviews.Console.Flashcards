using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories;

internal class StudySessionRepository : IStudySessionRepository
{
    public Task<List<StudySession>> GetStudySessionsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> InsertStudySessionAsync(StudySession studySession)
    {
        throw new NotImplementedException();
    }
}