using CodeReviews.Console.Flashcards.aneevel.DTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudyStackDTOs;

internal static class FromDto
{
    extension(CreateStudyStackDto dto)
    {
        public StudyStack FromCreateStudyStackDto()
        {
            return new StudyStack
            {
                Name = dto.Name,
            };
        }
    }
}