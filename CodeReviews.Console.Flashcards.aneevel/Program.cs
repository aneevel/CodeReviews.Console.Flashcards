using CodeReviews.Console.Flashcards.aneevel;

try
{
    await Init();
    await Shutdown();
}
catch (Exception ex)
{
    Console.WriteLine($"There was an error during application execution: {ex.Message}");
}

async Task Init()
{
   // TODO: Implement configuration file load
   
   // TODO: Implement Logger load
   
   // TODO: Setup DI
   
   // TODO: Set up connection

   FlashcardsApplication flashcardsApplication = new FlashcardsApplication();
   await flashcardsApplication.Run();
}

async Task Shutdown()
{
    // TODO: Gracefully shutdown any services/logs/connections
}