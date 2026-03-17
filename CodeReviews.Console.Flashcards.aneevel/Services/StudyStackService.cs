using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

namespace CodeReviews.Console.Flashcards.aneevel.Services;

internal class StudyStackService(IStudyStackRepository repository) : IStudyStackService
{
    public async Task<int> CreateStudyStackAsync(CreateStudyStackDto stackDto)
    {
        return await repository.InsertStudyStackAsync(stackDto.FromCreateStudyStackDto());
    }

    public async Task<List<ReadStudyStackDto>> GetStudyStacksAsync()
    {
        List<StudyStack> studyStacks = await repository.GetStudyStacksAsync();

        return studyStacks
            .Select(stack => stack.FromStudyStack())
            .ToList();
    }

    public async Task<int> UpdateStudyStackAsync(UpdateStudyStackDto stackDto)
    {
        return await repository.UpdateStudyStackAsync(stackDto.FromUpdateStudyStackDto());
    }

    public async Task<int> DeleteStudyStackAsync(DeleteStudyStackDto stackDto)
    {
        return await repository.DeleteStudyStackAsync(stackDto.FromDeleteStudyStackDto());
    }
}