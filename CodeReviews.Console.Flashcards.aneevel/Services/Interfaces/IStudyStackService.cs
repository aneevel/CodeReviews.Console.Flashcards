using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

internal interface IStudyStackService
{
    Task<int> CreateStudyStackAsync(CreateStudyStackDto stackDto);
    Task<List<ReadStudyStackDto>> GetStudyStacksAsync();
    Task<int> UpdateStudyStackAsync(UpdateStudyStackDto stackDto);
    Task<int> DeleteStudyStackAsync(DeleteStudyStackDto  stackDto);
}