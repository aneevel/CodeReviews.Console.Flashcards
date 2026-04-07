namespace CodeReviews.Console.Flashcards.aneevel.Utilities;

internal sealed class DatabaseSettings
{
   public required string DataSource { get; init; }
   public required string UserId { get; init; }
   public required string Password { get; init; }
   public required string InitialCatalog { get; init; }
   public required bool TrustServerCertificate { get; init; }
}