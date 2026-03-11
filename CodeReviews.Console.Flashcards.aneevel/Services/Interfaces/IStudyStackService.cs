using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

internal interface IStudyStackService
{
    public Task<int> AddStudyStackAsync(CreateStudyStackDto stackDto);
    public Task<List<ReadStudyStackDto>> GetStudyStacksAsync();
    public Task<int> UpdateStudyStackAsync(UpdateStudyStackDto stackDto);
}