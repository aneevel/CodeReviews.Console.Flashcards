using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

internal interface IStudyStackService
{
    public Task<int> AddStudyStackAsync(CreateStudyStackDto dto);
    public Task<List<ReadStudyStackDto>> GetStudyStacksAsync();
}