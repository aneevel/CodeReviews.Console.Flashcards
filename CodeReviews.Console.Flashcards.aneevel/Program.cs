using CodeReviews.Console.Flashcards.aneevel;

try
{
    Init();
    Shutdown();
}
catch (Exception ex)
{
    System.Console.WriteLine($"There was an error during application execution: {ex.Message}");
}

void Init()
{
   // TODO: Implement configuration file load
   
   // TODO: Implement Logger load
   
   // TODO: Setup DI 
   
   // TODO: Set up connection

   FlashcardsApplication flashcardsApplication = new();
}

void Shutdown()
{
    // TODO: Gracefully shutdown any services/logs/connections
}