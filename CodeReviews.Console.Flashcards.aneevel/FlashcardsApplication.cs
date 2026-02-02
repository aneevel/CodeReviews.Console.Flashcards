using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel;

public class FlashcardsApplication
{
    public FlashcardsApplication()
    {
        Run();
    }

    private void Run()
    {
        while (true)
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a [green]module[/]:")
                    .AddChoices("Flashcard Operations", "Stack Operations", "Study Session Operations", "Quit"));

            switch (option)
            {
                case "Flashcard Operations":
                    AnsiConsole.MarkupLine("Please choose an operation...");
                    break;
                case "Stack Operations":
                    AnsiConsole.MarkupLine("Please choose an operation...");
                    break;
                case "Study Session Operations":
                    AnsiConsole.MarkupLine("Please choose an operation...");
                    break;
                case "Quit":
                    return;
                default:
                    throw new InvalidOperationException("Unknown Menu Option provided!");
            }
        }
    }
}