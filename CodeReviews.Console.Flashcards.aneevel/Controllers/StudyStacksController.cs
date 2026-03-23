using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Controllers;

internal class StudyStacksController(IStudyStackService service) : IStudyStacksController
{
    public async Task HandleMainMenuSelectionAsync()
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
                await HandleReadOperationAsync();
                break;
            case StackMenuOptions.CreateAStack:
                await HandleCreateOperationAsync();
                break;
            case StackMenuOptions.EditAStack:
                await HandleUpdateOperationAsync();
                break;
            case StackMenuOptions.DeleteAStack:
                await HandleDeleteOperationAsync();
                break;
            case StackMenuOptions.ExitStackModule:
                AnsiConsole.Clear();
                return;
            default:
                throw new InvalidOperationException("Unknown Menu Option provided!");
        }
    }

    public async Task HandleCreateOperationAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("[green]Creating[/] a new Stack...");

        string stackName = AnsiConsole.Ask<string>("What should the [green]name[/] of this stack be?");

        CreateStudyStackDto stack = new CreateStudyStackDto(stackName);

        await service.CreateStudyStackAsync(stack);
    }

    public async Task HandleReadOperationAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("[green]Viewing[/] all Stacks...");

        try
        {
            List<ReadStudyStackDto> studyStacks =
                await service.GetStudyStacksAsync();

            // TODO: Will refactor to UI

            // TODO: Handle zero edge case
            Table table = new Table()
                .HideHeaders()
                .Border(TableBorder.None);

            table.AddColumn(new TableColumn("Name"));

            foreach (ReadStudyStackDto studyStack in studyStacks)
            {
                table.AddRow(studyStack.Name ?? string.Empty);
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
                                   Class: {nameof(StudyStacksController)}
                                   Method:  {nameof(HandleReadOperationAsync)}
                                   There was an error accessing the HandleReadOperationAsync module: {ex.Message}
                                   """;
            AnsiConsole.MarkupLine(errorMessage);
        }
    }

    public async Task HandleUpdateOperationAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Please select a Stack to [green]Edit[/]...");

        try
        {
            List<ReadStudyStackDto> studyStacks = await service.GetStudyStacksAsync();

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
                    await service.UpdateStudyStackAsync(
                        new UpdateStudyStackDto(newStackName, selectedStack.Id));
                    break;
                }
                case 'n':
                    AnsiConsole.MarkupLine("Aborting edit; returning to main menu.");
                    break;
            }
        }
        // TODO: What to catch?
        catch
        {
        }
    }

    public async Task HandleDeleteOperationAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Please select a Stack to [red]Delete[/]...");

        try
        {
            List<ReadStudyStackDto> studyStacks = await service.GetStudyStacksAsync();

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
                    await service.DeleteStudyStackAsync(new DeleteStudyStackDto(selectedStack.Id));
                    break;
                }
            }
        }
        // TODO: Catch what?
        catch
        {
        }
    }
}