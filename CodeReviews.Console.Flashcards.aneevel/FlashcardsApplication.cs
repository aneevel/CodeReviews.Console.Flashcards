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
            string option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a [green]module[/]:")
                    .AddChoices("Flashcard Operations", "Stack Operations", "Study Session Operations", "Quit"));

            switch (option)
            {
                case "Flashcard Operations":
                    DisplayFlashcardOperations();
                    break;
                case "Stack Operations":
                    DisplayStackOperations();
                    break;
                case "Study Session Operations":
                    DisplayStudySessionOperations();
                    break;
                case "Quit":
                    return;
                default:
                    throw new InvalidOperationException("Unknown Menu Option provided!");
            }
        }
    }

    private void DisplayFlashcardOperations()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Welcome to the [green]Flashcards Module[/]. Please choose the [blue]operation[/] you would like to perform.");

        string option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an [blue]operation[/]:")
                    .AddChoices("View All Flashcards", "Create A Flashcard", "Edit A Flashcard", "Delete A Flashcard", "Exit to Main Menu"));

        switch  (option)
        {
            case "View All Flashcards":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Viewing[/] all Flashcards");
                break;
            case "Create A Flashcard":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Creating[/] a Flashcard");
                break;
            case "Edit A Flashcard":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Editing[/] a Flashcard");
                break;
            case "Delete A Flashcard":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Deleting[/] a Flashcard");
                break;
            case "Exit to Main Menu":
                AnsiConsole.Clear();
                return;
            default:
                throw new InvalidOperationException("Unknown Menu Option provided!");
        }
    }

    private void DisplayStackOperations()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Welcome to the [green]Stacks Module[/]! Please choose an [blue]operation[/] you would like to perform.");

        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an [blue]operation[/]:")
                .AddChoices("View All Stacks", "Create A Stack", "Edit A Stack", "Delete A Stack", "Exit to Main Menu")
        );

        switch (option)
        {
            case "View All Stacks":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Viewing[/] all Stacks");
                break;
            case "Create A Stack":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Creating[/] a Stack");
                break;
            case "Edit A Stack":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Editing[/] a Stack");
                break;
            case "Delete A Stack":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Deleting[/] a Stack");
                break;
            case "Exit to Main Menu":
                AnsiConsole.Clear();
                return;
            default:
                throw new InvalidOperationException("Unknown Menu Option provided!");
        }
    }

    private void DisplayStudySessionOperations()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Welcome to the [green]Study Sessions Module[/]! Please choose an [blue] operation[/] you would like to perform.");

        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an [blue]operation[/]:")
                .AddChoices("View All Sessions", "Start Session", "Exit to Main Menu")
        );

        switch (option)
        {
            case "View All Sessions":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Viewing[/] all Sessions");
                break;
            case "Start Session":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Starting[/] a new Session");
                break;
            case "Exit to Main Menu":
                AnsiConsole.Clear();
                return;
            default:
                throw new InvalidOperationException("Unknown Menu Option provided!");

        }
    }
}