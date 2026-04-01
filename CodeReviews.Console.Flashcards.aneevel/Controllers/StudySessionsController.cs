using System.ComponentModel;
using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
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
        throw new NotImplementedException();
    }

    public async Task HandleStartSessionOperationAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Entering [green]Start Study Session[/] Module...");

        try
        {
            List<ReadStudyStackDto> studyStackDtos =
                await studyStackService.GetStudyStacksAsync();

            // TODO: Move to input validation helper
            if (studyStackDtos.Count == 0)
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

            AnsiConsole.MarkupLine("Beginning [green]Study Session[/]...");

            int questionNumber = 1;
            foreach (ReadFlashcardDto flashcardDto in selectedStack.Flashcards)
            {
                AnsiConsole.MarkupLine($"[green]Question {questionNumber}[/]");
                AnsiConsole.MarkupLine(flashcardDto.FrontText);
                questionNumber++;
            }
        }
        // TODO: Catch what???
        catch (InvalidCastException e)
        {

        }
    }
}