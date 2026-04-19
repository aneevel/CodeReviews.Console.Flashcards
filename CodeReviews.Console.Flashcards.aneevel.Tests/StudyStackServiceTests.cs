using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Services;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using Moq;

namespace CodeReviews.Console.Flashcards.aneevel.Tests;

public class StudyStackServiceTests
{
    private readonly Mock<IStudyStackRepository> _mockStudyStackRepository;
    private readonly IStudyStackService _studyStackService;
    private const int StudyStackId = 1;

    public StudyStackServiceTests()
    {
        _mockStudyStackRepository = new Mock<IStudyStackRepository>();
        _studyStackService = new StudyStackService(_mockStudyStackRepository.Object);
    }

    [Fact]
    public async Task AddStudyStackAsync_ShouldReturnZero_WhenSuccessful()
    {
        const int expected = 0;
        _mockStudyStackRepository
            .Setup(repo => repo.InsertStudyStackAsync(It.IsAny<StudyStack>()))
            .ReturnsAsync(expected);

        int result = await _studyStackService.CreateStudyStackAsync(new CreateStudyStackDto());
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task AddStudyStackAsync_ShouldReturnNegativeOne_WhenUnsuccessful()
    {
        const int expected = -1;
        _mockStudyStackRepository
            .Setup(repo => repo.InsertStudyStackAsync(It.IsAny<StudyStack>()))
            .ReturnsAsync(expected);

        int result = await _studyStackService.CreateStudyStackAsync(new CreateStudyStackDto());
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task UpdateStudyStackAsync_ShouldReturnZero_WhenSuccessful()
    {
        const int expected = 0;
        _mockStudyStackRepository
            .Setup(repo => repo.UpdateStudyStackAsync(It.IsAny<StudyStack>()))
            .ReturnsAsync(expected);

        int result = await _studyStackService.UpdateStudyStackAsync(new UpdateStudyStackDto());
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task UpdateStudyStackAsync_ShouldReturnNegativeOne_WhenUnsuccessful()
    {
        const int expected = -1;
        _mockStudyStackRepository
            .Setup(repo => repo.UpdateStudyStackAsync(It.IsAny<StudyStack>()))
            .ReturnsAsync(expected);

        int result = await _studyStackService.UpdateStudyStackAsync(new UpdateStudyStackDto());
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task DeleteStudyStackAsync_ShouldReturnZero_WhenSuccessful()
    {
        const int expected = 0;
        _mockStudyStackRepository
            .Setup(repo => repo.DeleteStudyStackAsync(It.IsAny<StudyStack>()))
            .ReturnsAsync(expected);

        int result = await _studyStackService.DeleteStudyStackAsync(new DeleteStudyStackDto(StudyStackId));
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task DeleteStudyStackAsync_ShouldReturnNegativeOne_WhenUnsuccessful()
    {
        const int expected = -1;
        _mockStudyStackRepository
            .Setup(repo => repo.DeleteStudyStackAsync(It.IsAny<StudyStack>()))
            .ReturnsAsync(expected);

        int result = await _studyStackService.DeleteStudyStackAsync(new DeleteStudyStackDto(StudyStackId));
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetStudyStacksAsync_ShouldReturnListOfStudyStacks_WhenStudyStacksExist()
    {
        List<StudyStack> expected =
        [
            new StudyStack(),
            new StudyStack(),
            new StudyStack()
        ];

        _mockStudyStackRepository
            .Setup(repo => repo.GetStudyStacksAsync())
            .ReturnsAsync(expected);

        List<ReadStudyStackDto> result = await _studyStackService.GetStudyStacksAsync();
        Assert.NotNull(result);
        Assert.Equal(expected.Count, result.Count);
        Assert.All(result, (item, index) => Assert.Equal(item.Id, expected[index].Id));
        Assert.All(result, (item, index) => Assert.Equal(item.Name, expected[index].Name));
    }

    [Fact]
    public async Task GetStudyStacksAsync_ShouldReturnEmptyList_WhenStudyStacksDoNotExist()
    {
        List<StudyStack> expected = [];

        _mockStudyStackRepository
            .Setup(repo => repo.GetStudyStacksAsync())
            .ReturnsAsync(expected);

        List<ReadStudyStackDto> result = await _studyStackService.GetStudyStacksAsync();
        Assert.Empty(result);
        Assert.Equal(result.Count, expected.Count);
    }
}