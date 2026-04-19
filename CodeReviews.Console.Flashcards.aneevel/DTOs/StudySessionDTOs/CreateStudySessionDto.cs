using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;

public class CreateStudySessionDto()
{
    public CreateStudySessionDto(int studyStackId, int score, DateTime date) : this()
    {
       StudyStackId = studyStackId;
       Score = score;
       Date = date;
    }

    public int StudyStackId { get; }
    public int Score { get; }
    public DateTime Date { get; }
}