using CodeReviews.Console.Flashcards.aneevel.Database;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories;
using CodeReviews.Console.Flashcards.aneevel.DTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Entities;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using CodeReviews.Console.Flashcards.aneevel.Extensions.DTOs.StudyStackDTOs;
using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel;

public class FlashcardsApplication
{

    private StudyStackRepository studyStackRepository;
    public async Task Run()
    {
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                UserID = "sa",
                Password = "password1@",
                InitialCatalog = "master",
                TrustServerCertificate = true
            };
            studyStackRepository = new StudyStackRepository(builder.ConnectionString);

            SqlServerDatabaseInitializer sqlServerDatabaseInitializer = new SqlServerDatabaseInitializer();
            await sqlServerDatabaseInitializer.InitializeDatabaseAsync();
            while (true)
            {
                MainMenuOptions option = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenuOptions>()
                        .Title("Select a [green]module[/]:")
                        .AddChoices(Enum.GetValues<MainMenuOptions>())
                        .UseConverter(option => option.GetDisplayName()));

                switch (option)
                {
                    case MainMenuOptions.EnterFlashcardModule:
                        DisplayFlashcardOperations();
                        break;
                    case MainMenuOptions.EnterStacksModule:
                        await DisplayStackOperations();
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

    private async Task DisplayStackOperations()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Welcome to the [green]Stacks Module[/]! Please choose an [blue]operation[/] you would like to perform.");

        StackMenuOptions option = AnsiConsole.Prompt(
            new SelectionPrompt<StackMenuOptions>()
                .Title("Select an [blue]operation[/]:")
                .AddChoices(Enum.GetValues<StackMenuOptions>())
                .UseConverter(option => option.GetDisplayName())

        );

        switch (option)
        {
            case StackMenuOptions.ViewAllStacks:
                await ViewStacks();
                break;
            case StackMenuOptions.AddAStack:
                await CreateStack();
                break;
            case StackMenuOptions.EditAStack:
                await EditStacks();
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

    private async Task CreateStack()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("[green]Creating[/] a new Stack...");

        string stackName = AnsiConsole.Ask<string>("What should the [green]name[/] of this stack be?");

        CreateStudyStackDto stack = new CreateStudyStackDto(stackName);

        await studyStackRepository.InsertStudyStackAsync(stack.FromCreateStudyStackDto());
    }

    private async Task ViewStacks()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("[green]Viewing[/] all Stacks...");

        List<StudyStack> studyStacks = await studyStackRepository.GetStudyStacksAsync();

        // TODO: Will refactor to UI

        // TODO: Handle zero edge case
        Table table = new Table()
            .HideHeaders()
            .Border(TableBorder.None);

        table.AddColumn(new TableColumn("Name"));

        foreach (StudyStack studyStack in studyStacks)
        {
            table.AddRow(studyStack.Name);
        }

        Panel panel = new Panel(table)
            .Header(new PanelHeader("Study Stacks"))
            .DoubleBorder()
            .BorderColor(Color.Purple)
            .Expand();

        AnsiConsole.Write(panel);
    }

    private async Task EditStacks()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Please select a Stack to [green]Edit[/]...");

        List<ReadStudyStackDto> studyStacks = await studyStackRepository.GetStudyStacksAsync();

        StudyStack selectedStack = AnsiConsole.Prompt(new SelectionPrompt<StudyStack>()
            .Title("Which Stack do you want to edit?")
            .AddChoices(studyStacks)
        );

        char answer = AnsiConsole.Ask<char>($"You would like to edit {selectedStack}?");

        if (answer == 'y')
        {
            // TODO: Move to edit

        }
        else if (answer == 'n')
        {
            AnsiConsole.MarkupLine("Aborting edit; returning to main menu.");
            return;
        }
    }
}