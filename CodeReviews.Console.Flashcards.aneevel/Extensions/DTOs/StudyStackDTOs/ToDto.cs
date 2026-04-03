using CodeReviews.Console.Flashcards.aneevel.DTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudyStackDTOs;

internal static class ToDto
{
    extension(StudyStack studyStack)
    {
        public ReadStudyStackDto FromStudyStack()
        {
            List<ReadFlashcardDto> readFlashcardDtos = [];
            if (studyStack.Flashcards != null)
            {
                readFlashcardDtos.AddRange(studyStack.Flashcards.Select(flashcard => new ReadFlashcardDto(flashcard.Id, flashcard.FrontText, flashcard.BackText)));
            }
            
            return new ReadStudyStackDto(studyStack.Name, studyStack.Id, readFlashcardDtos);
        }
    }
}