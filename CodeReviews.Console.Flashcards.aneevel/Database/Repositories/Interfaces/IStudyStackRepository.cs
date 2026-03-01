using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;

internal interface IStudyStackRepository : IDisposable
{
   Task<List<StudyStack>> GetStudyStacksAsync();
   Task<StudyStack> GetStudyStackAsync(string stackName);
   Task<int> InsertStudyStackAsync(StudyStack studyStack);
   Task UpdateStudyStackAsync(StudyStack studyStack);
   Task DeleteStudyStackAsync(StudyStack studyStack);
}