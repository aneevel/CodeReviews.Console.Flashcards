using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;

public class CreateStudyStackDto()
{
    public CreateStudyStackDto(string name) : this()
    {
        Name = name;
    }

    public string Name { get; } = string.Empty;

}