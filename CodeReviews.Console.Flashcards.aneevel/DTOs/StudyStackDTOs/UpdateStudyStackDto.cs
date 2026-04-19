using System.ComponentModel.DataAnnotations;

namespace CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;

public class UpdateStudyStackDto()
{
    public UpdateStudyStackDto(string name, int id) : this()
    {
        Name = name;
        Id = id;
    }

    public string Name { get; } = string.Empty;
    public int Id { get; }
}