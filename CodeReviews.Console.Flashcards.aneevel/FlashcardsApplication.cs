using CodeReviews.Console.Flashcards.aneevel.Database;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using CodeReviews.Console.Flashcards.aneevel.Services;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel;

public sealed class FlashcardsApplication
{
    private ServiceProvider _serviceProvider;
    public async Task Run()
    {
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                UserID = "sa", Password = "password1@",
                InitialCatalog = "master",
                TrustServerCertificate = true
            };

            ConnectionString connectionString = new ConnectionString(builder.ConnectionString);

            // TODO: this should all be top level
            IServiceCollection services = new ServiceCollection()
                .AddSingleton(connectionString)
                .AddScoped<IDatabaseInitializer, SqlServerDatabaseInitializer>()
                .AddScoped<IStudyStackRepository, StudyStackRepository>()
                .AddScoped<IStudyStackService, StudyStackService>()
                .AddScoped<IFlashcardRepository, FlashcardRepository>()
                .AddScoped<IFlashcardService, FlashcardService>();

            _serviceProvider = services.BuildServiceProvider();

            await _serviceProvider.GetRequiredService<IDatabaseInitializer>().InitializeDatabaseAsync();

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
                        await DisplayFlashcardOperations();
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

    private async Task DisplayFlashcardOperations()
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
            case FlashcardMenuOptions.CreateAFlashcard:
                await CreateFlashcard();
                break;
            case FlashcardMenuOptions.EditAFlashcard:
                await EditFlashcard();
                break;
            case FlashcardMenuOptions.RemoveAFlashcard:
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[green]Deleting[/] a Flashcard");
                break;
            case FlashcardMenuOptions.ExitFlashcardModule:
                AnsiConsole.Clear();
                break;
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
            case StackMenuOptions.CreateAStack:
                await CreateStack();
                break;
            case StackMenuOptions.EditAStack:
                await EditStack();
                break;
            case StackMenuOptions.DeleteAStack:
                await DeleteStack();
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

        await _serviceProvider.GetRequiredService<IStudyStackService>().CreateStudyStackAsync(stack);
    }

    private async Task ViewStacks()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("[green]Viewing[/] all Stacks...");
        
        try
        {
            List<ReadStudyStackDto> studyStacks =
                await _serviceProvider.GetRequiredService<IStudyStackService>().GetStudyStacksAsync();
            
            // TODO: Will refactor to UI

            // TODO: Handle zero edge case
            Table table = new Table()
                .HideHeaders()
                .Border(TableBorder.None);

            table.AddColumn(new TableColumn("Name"));

            foreach (ReadStudyStackDto studyStack in studyStacks)
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
        catch (InvalidOperationException ex)
        {
            string errorMessage = $"""
                                   Class: {nameof(FlashcardsApplication)}
                                   Method:  {nameof(ViewStacks)}
                                   There was an error accessing the ViewStudyStacks module: {ex.Message}
                                   """;
            AnsiConsole.MarkupLine(errorMessage);
        }
    }

    private async Task EditStack()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Please select a Stack to [green]Edit[/]...");

        // TODO: Should be in a try/catch
        List<ReadStudyStackDto> studyStacks = await _serviceProvider.GetRequiredService<IStudyStackService>().GetStudyStacksAsync();

        if (studyStacks.Count == 0)
        {
            // TODO: Provide wait for key press input
            // WaitForContinue...
            return;
        }

        ReadStudyStackDto selectedStack = AnsiConsole.Prompt(new SelectionPrompt<ReadStudyStackDto>()
            .Title("Which Stack do you want to edit?")
            .AddChoices(studyStacks)
        );

        char answer = AnsiConsole.Ask<char>($"You would like to [blue]edit {selectedStack}[/]? (Y/N)");

        switch (char.ToLower(answer))
        {
            case 'y':
            {
                // TODO: Move to edit
                string newStackName = AnsiConsole.Ask<string>("What should the new name of the stack be?");
                await _serviceProvider.GetRequiredService<IStudyStackService>().UpdateStudyStackAsync(
                    new UpdateStudyStackDto(newStackName, selectedStack.Id));
                break;
            }
            case 'n':
                AnsiConsole.MarkupLine("Aborting edit; returning to main menu.");
                break;
        }
    }

    private async Task DeleteStack()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Please select a Stack to [red]Delete[/]...");

        List<ReadStudyStackDto> studyStacks = await _serviceProvider.GetRequiredService<IStudyStackService>().GetStudyStacksAsync();

        if (studyStacks.Count == 0)
        {
            // TODO: Provide wait for key press input
            // WaitForContinue...
            return;
        }

        ReadStudyStackDto selectedStack = AnsiConsole.Prompt(new SelectionPrompt<ReadStudyStackDto>()
            .Title("Which Stack do you want to delete?")
            .AddChoices(studyStacks)
        );

        char answer = AnsiConsole.Ask<char>($"You would like to [red]delete[/] [blue]{selectedStack}[/]? (Y/N)");

        switch (char.ToLower(answer))
        {
            case 'y':
            {
                // TODO: Move to UI
                await _serviceProvider.GetRequiredService<IStudyStackService>().DeleteStudyStackAsync(new DeleteStudyStackDto(selectedStack.Id));
                break;
            }
        }

    }

    private async Task CreateFlashcard()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Entering [green]Create Flashcard[/] Module...");

        try
        {
            List<ReadStudyStackDto> studyStacks =
                await _serviceProvider.GetRequiredService<IStudyStackService>().GetStudyStacksAsync();

            if (studyStacks.Count == 0)
            {
                AnsiConsole.MarkupLine("There are [red]No Study Stacks found.[/] Flashcards must have an" +
                                       " associated Study Stack. Returning to main menu.");
                return;
            }

            // TODO: Move to input validation helper
            ReadStudyStackDto selectedStack = AnsiConsole.Prompt(new SelectionPrompt<ReadStudyStackDto>()
                .Title("Which Stack will this Flashcard belong to?")
                .AddChoices(studyStacks)
            );

            // TODO: Move to input validation helper
            string newFlashcardFrontText = AnsiConsole.Ask<string>("What should the [blue]Front[/] of the Flashcard be?");
            string newFlashcardBackText = AnsiConsole.Ask<string>("What should the [blue]Back[/] of the Flashcard be?");

            await _serviceProvider.GetRequiredService<IFlashcardService>()
                .CreateFlashcardAsync(
                    new CreateFlashcardDto(
                        selectedStack.Id,
                        newFlashcardFrontText,
                        newFlashcardBackText
                    )
                );
        }
        catch (InvalidCastException ex)
        {
            string errorMessage = $"""
                                   Class: {nameof(FlashcardsApplication)}
                                   Method: {nameof(CreateFlashcard)}
                                   There was an error reading StudyStacks in the CreateFlashcard module: {ex.Message}
                                   """;
            AnsiConsole.MarkupLine(errorMessage);
        }
    }

    private async Task EditFlashcard()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Please select a Flashcard to [green]Edit[/] ...");

        // TODO: Should be in a try/catch
        List<ReadFlashcardDto> flashcardDtos =
            await _serviceProvider.GetRequiredService<IFlashcardService>().GetFlashcardsAsync();

        if (flashcardDtos.Count == 0)
        {
            // TODO: Provide wait for key press input
            return;
        }

        ReadFlashcardDto selectedFlashcard = AnsiConsole.Prompt(new SelectionPrompt<ReadFlashcardDto>()
            .Title("Which Flashcard do you want to edit?")
            .AddChoices(flashcardDtos)
        );

        char answer = AnsiConsole.Ask<char>($"You would like to [blue]edit {selectedFlashcard}[/]? (Y/N)");

        switch (char.ToLower(answer))
        {
            case 'y':
            {
                List<ReadStudyStackDto> studyStacks =
                    await _serviceProvider
                        .GetRequiredService<IStudyStackService>()
                        .GetStudyStacksAsync();

                // TODO: will refactor ot UI
                if (studyStacks.Count == 0)
                {
                    // TODO: Move to UI/validation
                    return;
                }

                ReadStudyStackDto selectedStack = AnsiConsole.Prompt(new SelectionPrompt<ReadStudyStackDto>()
                    .Title("Which Stack should this Flashcard belong to?")
                    .AddChoices(studyStacks)
                );

                string newFrontText = AnsiConsole.Ask<string>("What should the Flashcard front text be?");
                string newBackText = AnsiConsole.Ask<string>("What should the Flashcard back text be?");
                await _serviceProvider.GetRequiredService<IFlashcardService>().UpdateFlashcardAsync(
                    new UpdateFlashcardDto(selectedStack.Id, newFrontText, newBackText, selectedFlashcard.Id));
                break;
            }
            case 'n':
                AnsiConsole.MarkupLine("Aborting edit; returning to main module.");
                break;
        }

    }
<<<<<<< HEAD
    }
=======
>>>>>>> 16cc63c ([feat]: implement module for Edit Flashcard)
}