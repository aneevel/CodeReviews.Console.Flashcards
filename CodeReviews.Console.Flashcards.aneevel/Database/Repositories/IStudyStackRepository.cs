using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories;

internal interface IStudyStackRepository : IDisposable
{
   Task<IEnumerable<StudyStack>> GetStudyStacksAsync();
   Task<StudyStack> GetStudyStackAsync(string stackName);
   Task InsertStudyStackAsync(StudyStack studyStack);
   Task UpdateStudyStackAsync(StudyStack studyStack);
   Task DeleteStudyStackAsync(StudyStack studyStack);
}