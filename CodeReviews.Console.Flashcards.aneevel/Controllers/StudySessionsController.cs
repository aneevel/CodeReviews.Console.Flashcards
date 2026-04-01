using System.ComponentModel;
using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Controllers;

internal class StudySessionsController : IStudySessionsController
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
        throw new NotImplementedException();
    }
}