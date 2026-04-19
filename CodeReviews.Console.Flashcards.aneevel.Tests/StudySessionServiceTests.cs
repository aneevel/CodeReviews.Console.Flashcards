using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Services;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using Moq;

namespace CodeReviews.Console.Flashcards.aneevel.Tests;

public class StudySessionServiceTests
{
    private readonly Mock<IStudySessionRepository> _mockStudySessionRepository;
    private readonly IStudySessionService _studySessionService;

    public StudySessionServiceTests()
    {
        _mockStudySessionRepository = new Mock<IStudySessionRepository>();
        _studySessionService = new StudySessionService(_mockStudySessionRepository.Object);
    }

    [Fact]
    public async Task CreateStudySession_ShouldReturnZero_WhenSuccessful()
    {
        const int expected = 0;
        _mockStudySessionRepository
            .Setup(repo => repo.InsertStudySessionAsync(It.IsAny<StudySession>()))
            .ReturnsAsync(expected);

        int result = await _studySessionService.CreateStudySessionAsync(new CreateStudySessionDto());
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CreateStudySession_ShouldReturnNegativeOne_WhenUnsuccessful()
    {
        const int expected = -1;
        _mockStudySessionRepository
            .Setup(repo => repo.InsertStudySessionAsync(It.IsAny<StudySession>()))
            .ReturnsAsync(expected);

        int result = await _studySessionService.CreateStudySessionAsync(new CreateStudySessionDto());
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetStudySessionsAsync_ShouldReturnListOfStudySessions_WhenStudySessionsExist()
    {
        List<StudySession> expected =
        [
            new StudySession(),
            new StudySession(),
            new StudySession()
        ];

        _mockStudySessionRepository
            .Setup(repo => repo.GetStudySessionsAsync())
            .ReturnsAsync(expected);

        List<ReadStudySessionDto> result = await _studySessionService.GetStudySessionsAsync();

        Assert.NotNull(result);
        Assert.Equal(expected.Count, result.Count);
        Assert.All(result, (item, index) => Assert.Equal(item.Id, expected[index].Id));
        Assert.All(result, (item, index) => Assert.Equal(item.StudyStackId, expected[index].StudyStackId));
        Assert.All(result, (item, index) => Assert.Equal(item.Score, expected[index].Score));
        Assert.All(result, (item, index) => Assert.Equal(item.Date, expected[index].Date));
    }

    [Fact]
    public async Task GetStudySessionsAsync_ShouldReturnEmptyList_WhenStudySessionsDoNotExist()
    {
        List<StudySession> expected = [];

        _mockStudySessionRepository
            .Setup(repo => repo.GetStudySessionsAsync())
            .ReturnsAsync(expected);

        List<ReadStudySessionDto> result = await _studySessionService.GetStudySessionsAsync();

        Assert.Empty(result);
        Assert.Equal(expected.Count, result.Count);
    }

}