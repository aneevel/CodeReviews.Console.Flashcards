using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudySessionDTOs;

internal static class ToDto
{
    extension(StudySession studySession)
    {
        public ReadStudySessionDto FromStudySession()
        {
            return new ReadStudySessionDto(studySession.Id, studySession.StudyStackId, studySession.Score, studySession.Date);
        }
    }
}