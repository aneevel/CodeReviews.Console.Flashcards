namespace CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;

internal interface IStudyStackService
{
    private Task<int> AddStackAsync(CreateStudyStackDto dto);
}