using System.Globalization;
using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Controllers;

internal class StudySessionsController(IStudyStackService studyStackService, IStudySessionService studySessionService) : IStudySessionsController
{
    public async Task HandleMainMenuSelectionAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Welcome to the [green]Study Sessions Module[/]. Please choose the [blue]operation[/] you would like to perform.");

        StudySessionMenuOptions option = AnsiConsole.Prompt(
            new SelectionPrompt<StudySessionMenuOptions>()
                .Title("Select an [blue]operation[/]:")
                .AddChoices(Enum.GetValues<StudySessionMenuOptions>())
                .UseConverter(option => option.GetDisplayName())
        );

        switch (option)
        {
            case StudySessionMenuOptions.StartAStudySession:
                await HandleStartSessionOperationAsync();
                break;
            case StudySessionMenuOptions.ViewAllStudySessions:
                await HandleViewSessionsOperationAsync();
                break;
            case StudySessionMenuOptions.ExitStudySessionModule:
                AnsiConsole.Clear();
                // TODO: Wait on user input and provide message
                break;
            default:
                throw new InvalidOperationException("System failure; Unknown Menu Option provided.");
        }
    }

    public async Task HandleViewSessionsOperationAsync()
    {
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[green]Viewing[/] all Study Sessions...");

            List<ReadStudySessionDto> studySessionDtos =
                await studySessionService.GetStudySessionsAsync();

            if (!studySessionDtos.Any())
            {
                AnsiConsole.MarkupLine("There are no Study Sessions found.");
            }

            // TODO: refactor to UI

            // TODO: Handle zero edge case
            Table table = new Table()
                .DoubleBorder()
                .BorderColor(Color.Aqua)
                .Expand();

            table.AddColumn(new TableColumn("[yellow]Study Stack[/]"));
            table.AddColumn(new TableColumn("[yellow]Date[/]"));
            table.AddColumn(new TableColumn("[yellow]Score[/]"));

            foreach (ReadStudySessionDto studySessionDto in studySessionDtos)
            {
                table.AddRow($"[green]{studySessionDto.ReadStudyStackDto.Name}[/]",
                    $"[green]{studySessionDto.Date.ToString(CultureInfo.InvariantCulture)}[/]",
                    $"[green]{studySessionDto.Score.ToString()}[/]");
            }

            AnsiConsole.Write(table);
    }

    public async Task HandleStartSessionOperationAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Entering [green]Start Study Session[/] Module...");

            List<ReadStudyStackDto> studyStackDtos =
                await studyStackService.GetStudyStacksAsync();

            // TODO: Move to input validation helper
            if (!studyStackDtos.Any() )
            {
                AnsiConsole.MarkupLine(
                    "There are [red]No Study Stacks found.[/] There must be at least [blue]one[/] Study Stack" +
                    " in order to have a Study Session.");
                // TODO: Prompt user to return
                return;
            }

            // TODO: Move to input validation helper
            ReadStudyStackDto selectedStack = AnsiConsole.Prompt(new SelectionPrompt<ReadStudyStackDto>()
                .Title("Which Study Stack will you use for your session?")
                .AddChoices(studyStackDtos)
            );

            // TODO: Move to input invalidation helper
            if (!selectedStack.Flashcards.Any())
            {
                AnsiConsole.MarkupLine(
                    $"There are [red]No Flashcards associated with {selectedStack}. There must be at least [blue]one[/] Flashcard" +
                    " in order to have a Study Session.");
                // TODO: Prompt user to return
                return;
            }

            AnsiConsole.MarkupLine("Beginning [green]Study Session[/]...");

            int questionNumber = 0;
            int score = 0;
            foreach (ReadFlashcardDto flashcardDto in selectedStack.Flashcards)
            {
                questionNumber++;
                AnsiConsole.MarkupLine($"[green]Question {questionNumber}[/]");
                AnsiConsole.MarkupLine(flashcardDto.FrontText);

                AnsiConsole.Ask<char>("Press enter to see the answer.");
                
                AnsiConsole.MarkupLine($"[green]Answer[/]");
                AnsiConsole.MarkupLine(flashcardDto.BackText);

                char response = AnsiConsole.Prompt<char>(new SelectionPrompt<char>()
                    .Title("Did you get the answer correct?")
                    .AddChoices(['Y', 'N'])
                );

                if (response == 'Y')
                    score++;
            }

            if (await studySessionService.CreateStudySessionAsync(new CreateStudySessionDto(selectedStack.Id, score,
                    DateTime.Now)) == -1)
            {
                AnsiConsole.MarkupLine("[red]Error; there was a problem creating the study session![/]");
                return;
            }

            AnsiConsole.MarkupLine($"Study Session [green]complete[/]. Your score was; [green]{score}/{questionNumber}[/]");
    }
}