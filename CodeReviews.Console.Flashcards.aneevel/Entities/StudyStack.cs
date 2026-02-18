namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class StudyStack
{
    public int StudyStackID { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Flashcard>? Flashcards { get; set; }
    // TODO: Add Study Sessions
}