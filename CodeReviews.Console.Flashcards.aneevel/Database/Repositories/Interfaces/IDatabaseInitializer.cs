namespace CodeReviews.Console.Flashcards.aneevel.Database;

internal interface IDatabaseInitializer
{
    public Task InitializeDatabaseAsync();
}