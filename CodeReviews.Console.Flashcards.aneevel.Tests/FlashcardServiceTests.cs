using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Services;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using Moq;


namespace CodeReviews.Console.Flashcards.aneevel.Tests;

public class FlashcardServiceTests
{
    private readonly Mock<IFlashcardRepository> _mockFlashcardRepository;
    private readonly IFlashcardService _flashcardService;
    private const int FlashcardId = 1;

    public FlashcardServiceTests()
    {
        _mockFlashcardRepository = new Mock<IFlashcardRepository>();
       _flashcardService = new FlashcardService(_mockFlashcardRepository.Object);
    }
    [Fact]
    public async Task AddFlashcardAsync_ShouldReturnZero_WhenSuccessful()
    {
        const int expected = 0;
        _mockFlashcardRepository
            .Setup(repo => repo.InsertFlashcardAsync(It.IsAny<Flashcard>()))
            .ReturnsAsync(expected);

        int result = await _flashcardService.CreateFlashcardAsync(new CreateFlashcardDto());

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task AddFlashcardAsync_ShouldReturnNegativeOne_WhenUnsuccessful()
    {
        const int expected = -1;
        _mockFlashcardRepository
            .Setup(repo => repo.InsertFlashcardAsync(It.IsAny<Flashcard>()))
            .ReturnsAsync(expected);

        int result = await _flashcardService.CreateFlashcardAsync(new CreateFlashcardDto());

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task UpdateFlashcardAsync_ShouldReturnZero_WhenSuccessful()
    {
        const int expected = 0;
        _mockFlashcardRepository
            .Setup(repo => repo.UpdateFlashcardAsync(It.IsAny<Flashcard>()))
            .ReturnsAsync(expected);

        int result = await _flashcardService.UpdateFlashcardAsync(new UpdateFlashcardDto());

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task UpdateFlashcardAsync_ShouldReturnNegativeOne_WhenUnsuccessful()
    {
        const int expected = -1;
        _mockFlashcardRepository
            .Setup(repo => repo.UpdateFlashcardAsync(It.IsAny<Flashcard>()))
            .ReturnsAsync(expected);

        int result = await _flashcardService.UpdateFlashcardAsync(new UpdateFlashcardDto());

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task DeleteFlashcardAsync_ShouldReturnZero_WhenSuccessful()
    {
        const int expected = 0;
        _mockFlashcardRepository
            .Setup(repo => repo.DeleteFlashcardAsync(It.IsAny<Flashcard>()))
            .ReturnsAsync(expected);

        int result = await _flashcardService.DeleteFlashcardAsync(new DeleteFlashcardDto(FlashcardId));

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task DeleteFlashcardAsync_ShouldReturnNegativeOne_WhenUnsuccessful()
    {
        const int expected = -1;
        _mockFlashcardRepository
            .Setup(repo => repo.DeleteFlashcardAsync(It.IsAny<Flashcard>()))
            .ReturnsAsync(expected);

        int result = await _flashcardService.DeleteFlashcardAsync(new DeleteFlashcardDto(FlashcardId));

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetFlashcardsAsync_ShouldReturnListOfFlashcards_WhenFlashcardsExist()
    {
        List<Flashcard> expected =
        [
            new Flashcard
            {
                Id = FlashcardId,
                FrontText = "This is a test question",
                BackText = "This is a test answer"
            },
            new Flashcard
                {
                    Id = FlashcardId + 1,
                    FrontText = "Another test question",
                    BackText = "Another test answer"
                }
        ];

        _mockFlashcardRepository
            .Setup(repo => repo.GetFlashcardsAsync())
            .ReturnsAsync(expected);
 List<ReadFlashcardDto> result = await _flashcardService.GetFlashcardsAsync();

        Assert.NotNull(result);
        Assert.Equal(expected.Count, result.Count);
        Assert.All(result, (item, index) => Assert.Equal(item.Id, expected[index].Id));
        Assert.All(result, (item, index) => Assert.Equal(item.FrontText, expected[index].FrontText));
        Assert.All(result, (item, index) => Assert.Equal(item.BackText, expected[index].BackText));
        Assert.Equal(expected[0].BackText, result[0].BackText);
    }

    [Fact]
    public async Task GetFlashcardsAsync_ShouldReturnEmptyList_WhenNoFlashcardsExist()
    {
        List<Flashcard> expected = [];

        _mockFlashcardRepository
            .Setup(repo => repo.GetFlashcardsAsync())
            .ReturnsAsync(expected);

        List<ReadFlashcardDto> result = await _flashcardService.GetFlashcardsAsync();

        Assert.Equal(expected.Count, result.Count);
        Assert.Empty(result);
    }
}