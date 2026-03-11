using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
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

    extension(UpdateStudyStackDto dto)
    {
        public StudyStack FromUpdateStudyStackDto()
        {
            return new StudyStack()
            {
                Name = dto.Name,
                Id = dto.Id
            };
        }
    }

    extension(DeleteStudyStackDto dto)
    {
        public StudyStack FromDeleteStudyStackDto()
        {
            return new StudyStack()
            {
                Id = dto.Id,
            };
        }
    }
}