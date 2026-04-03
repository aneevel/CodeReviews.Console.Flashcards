using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.FlashcardDTOs;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudySessionDTOs;

internal static class ToDto
{
    extension(StudySession studySession)
    {
        public ReadStudySessionDto FromStudySession()
        {
            return new ReadStudySessionDto(studySession.Id, studySession.StudyStack.Id, studySession.Score, studySession.Date, 
                new ReadStudyStackDto(studySession.StudyStack.Name, 
                    studySession.StudyStack.Id, 
                    studySession.StudyStack.Flashcards
                        .Select(flashcard => flashcard.FromFlashcard())
                        .ToList()
                    )
                );
        }
    }
}