namespace CodeReviews.Console.Flashcards.aneevel.Entities;

public class StudyStack(string name)
{
    public int? Id { get; set; }
    public string? Name { get; set; } = name;
}