using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;

internal interface IStudyStackRepository
{
   Task<List<StudyStack>> GetStudyStacksAsync();
   Task<int> InsertStudyStackAsync(StudyStack studyStack);
   Task<int> UpdateStudyStackAsync(StudyStack studyStack);
   Task<int> DeleteStudyStackAsync(StudyStack studyStack);
}