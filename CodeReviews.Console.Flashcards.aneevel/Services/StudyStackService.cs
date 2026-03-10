using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

namespace CodeReviews.Console.Flashcards.aneevel.Services;

internal class StudyStackService(IStudyStackRepository repository) : IStudyStackService
{
    public async Task<int> AddStudyStackAsync(CreateStudyStackDto dto)
    {
        return await repository.InsertStudyStackAsync(dto.FromCreateStudyStackDto());
    }

    public async Task<List<ReadStudyStackDto>> GetStudyStacksAsync()
    {
        List<StudyStack> studyStacks = await repository.GetStudyStacksAsync();

        return studyStacks
            .Select(stack => stack.FromStudyStack())
            .ToList();
    }

    public async Task<int> UpdateStudyStackAsync(UpdateStudyStackDto dto)
    {
        return await repository.UpdateStudyStackAsync(dto.FromUpdateStudyStackDto());
    }
}