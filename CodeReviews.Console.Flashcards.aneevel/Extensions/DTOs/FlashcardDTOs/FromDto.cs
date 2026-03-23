using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.FlashcardDTOs;

internal static class FromDto
{
    extension(CreateFlashcardDto dto)
    {
        public Flashcard FromCreateFlashcardDto()
        {
            return new Flashcard
            {
                FrontText = dto.FrontText,
                BackText = dto.BackText,
                StudyStackId = dto.StudyStackId,
            };
        }
    }

    extension(UpdateFlashcardDto dto)
    {
        public Flashcard FromUpdateFlashcardDto()
        {
            return new Flashcard()
            {
                Id = dto.Id,
                FrontText = dto.FrontText,
                BackText = dto.BackText,
                StudyStackId = dto.StudyStackId
            };
        }
    }

    extension(DeleteFlashcardDto dto)
    {
        public Flashcard FromDeleteFlashcardDto()
        {
            return new Flashcard()
            {
                Id = dto.Id,
            };
        }
    }
}