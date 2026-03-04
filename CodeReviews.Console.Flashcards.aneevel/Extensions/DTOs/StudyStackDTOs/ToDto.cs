using CodeReviews.Console.Flashcards.aneevel.DTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudyStackDTOs;

internal static class ToDto
{
    extension(StudyStack studyStack)
    {
        public ReadStudyStackDto FromStudyStack()
        {
            return new ReadStudyStackDto(studyStack.Name);
        }
    }
}