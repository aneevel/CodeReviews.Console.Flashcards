using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudySessionDTOs;

internal static class FromDto
{
    extension(CreateStudySessionDto dto)
    {
        public StudySession FromCreateStudySessionDto()
        {
            return new StudySession
            {
                StudyStackId = dto.StudyStackId,
                Date = dto.Date,
                Score = dto.Score,
            };
        }
    }
}