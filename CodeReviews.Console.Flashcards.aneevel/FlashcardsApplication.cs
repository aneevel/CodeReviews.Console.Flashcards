using CodeReviews.Console.Flashcards.aneevel.Database;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel;

public class FlashcardsApplication
{
    public async Task Run()
    {
        try
        {
            SqlServerDatabaseInitializer sqlServerDatabaseInitializer = new SqlServerDatabaseInitializer();
            await sqlServerDatabaseInitializer.InitializeDatabaseAsync();
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
        catch (SqlException ex)
        {
            AnsiConsole.MarkupLine("[red]An error occured:[/]" + ex.Message);
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine("[red]An error occured:[/]" + e.Message);
        }
    }

    private void DisplayFlashcardOperations()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Welcome to the [green]Flashcards Module[/]. Please choose the [blue]operation[/] you would like to perform.");

        FlashcardMenuOptions option = AnsiConsole.Prompt(
            new SelectionPrompt<FlashcardMenuOptions>()
                .Title("Select an [blue]operation[/]:")
                .AddChoices(Enum.GetValues<FlashcardMenuOptions>())
                .UseConverter(option => option.GetDisplayName())
        );

        switch  (option)
        {
            case FlashcardMenuOptions.ViewAllFlashcards:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Viewing[/] all Flashcards");
                break;
            case FlashcardMenuOptions.AddAFlashcard:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Creating[/] a Flashcard");
                break;
            case FlashcardMenuOptions.EditAFlashcard:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Editing[/] a Flashcard");
                break;
            case FlashcardMenuOptions.RemoveAFlashcard:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Deleting[/] a Flashcard");
                break;
            case FlashcardMenuOptions.ExitFlashcard:
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
                CreateStack();
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

    private void CreateStack()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("[green]Creating[/] a new Stack...");

        string stackName = AnsiConsole.Ask<string>("What should the [green]name[/] of this stack be?");

        StudyStack studyStack = new StudyStack(stackName);
    }
}