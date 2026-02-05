namespace CodeReviews.Console.Flashcards.aneevel.Entities;

internal class Flashcard
{
    public int FlashcardID { get; set; }
    public string FrontText  { get; set; }
    public string BackText { get; set; }
    public int? StudyStackID { get; set; }
}