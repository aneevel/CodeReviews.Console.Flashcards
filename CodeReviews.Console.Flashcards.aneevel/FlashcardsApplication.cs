using CodeReviews.Console.Flashcards.aneevel.Database;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
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
                MainMenuOptions option = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenuOptions>()
                        .Title("Select a [green]module[/]:")
                        .AddChoices(Enum.GetValues<MainMenuOptions>()));

                switch (option)
                {
                    case MainMenuOptions.EnterFlashcardModule:
                        DisplayFlashcardOperations();
                        break;
                    case MainMenuOptions.EnterStacksModule:
                        DisplayStackOperations();
                        break;
                    case MainMenuOptions.EnterStudySessionsModule:
                        DisplayStudySessionOperations();
                        break;
                    case MainMenuOptions.ExitApplication:
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
            case FlashcardMenuOptions.ExitFlashcardModule:
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

        StackMenuOptions option = AnsiConsole.Prompt(
            new SelectionPrompt<StackMenuOptions>()
                .Title("Select an [blue]operation[/]:")
                .AddChoices(Enum.GetValues<StackMenuOptions>())
        );

        switch (option)
        {
            case StackMenuOptions.ViewAllStacks:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Viewing[/] all Stacks");
                break;
            case StackMenuOptions.AddAStack:
                CreateStack();
                AnsiConsole.MarkupLine("[green]Creating[/] a Stack");
                break;
            case StackMenuOptions.EditAStack:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Editing[/] a Stack");
                break;
            case StackMenuOptions.RemoveAStack:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Removing[/] a Stack");
                break;
            case StackMenuOptions.ExitStackModule:
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

        StudySessionMenuOptions option = AnsiConsole.Prompt(
            new SelectionPrompt<StudySessionMenuOptions>()
                .Title("Select an [blue]operation[/]:")
                .AddChoices(Enum.GetValues<StudySessionMenuOptions>())
        );

        switch (option)
        {
            case StudySessionMenuOptions.ViewAllStudySessions:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Viewing[/] all Sessions");
                break;
            case StudySessionMenuOptions.StartAStudySession:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Starting[/] a new Session");
                break;
            case StudySessionMenuOptions.ExitStudySessionModule:
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