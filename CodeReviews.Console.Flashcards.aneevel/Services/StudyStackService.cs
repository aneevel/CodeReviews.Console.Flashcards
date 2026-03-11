using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Services;

internal class StudyStackService(IStudyStackRepository repository) : IStudyStackService
{
    public async Task<int> AddStudyStackAsync(CreateStudyStackDto stackDto)
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
        AnsiConsole.WriteLine($"Updating StudyStack {stackDto}");
        return await repository.UpdateStudyStackAsync(stackDto.FromUpdateStudyStackDto());
    }
}