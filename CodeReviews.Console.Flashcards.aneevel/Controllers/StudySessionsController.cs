using System.Globalization;
using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudySessionDTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Controllers;

internal class StudySessionsController(IStudyStackService studyStackService, IStudySessionService studySessionService, UserInput userInput) : IStudySessionsController
{
    public async Task HandleMainMenuSelectionAsync()
    {
        userInput.WelcomeToModule("Welcome to the [green]Study Sessions Module[/]. Please choose the [blue]operation[/] you would like to perform.");

        StudySessionMenuOptions option =
            userInput.GetUserChoice<StudySessionMenuOptions>("Select an [blue]operation[/]:", Enum.GetValues<StudySessionMenuOptions>(), option => option.GetDisplayName());

        switch (option)
        {
            case StudySessionMenuOptions.StartAStudySession:
                await HandleStartSessionOperationAsync();
                break;
            case StudySessionMenuOptions.ViewAllStudySessions:
                await HandleViewSessionsOperationAsync();
                break;
            case StudySessionMenuOptions.ExitStudySessionModule:
                userInput.WaitForContinue("Exiting to main menu. Press any key to continue...");
                break;
            default:
                throw new InvalidOperationException("System failure; Unknown Menu Option provided.");
        }
    }

    public async Task HandleViewSessionsOperationAsync()
    {
        userInput.WelcomeToModule("[green]Viewing[/] all Study Sessions...");

            List<ReadStudySessionDto> studySessionDtos =
                await studySessionService.GetStudySessionsAsync();

            if (!studySessionDtos.Any())
            {
                userInput.WaitForContinue(
                    "There are [red]No Study Sessions found.[/] Please create some Study Sessions " +
                    " in order to view them. Press any key to continue...");
                return;
            }

            // TODO: Refactor to dedicated builder class
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
        userInput.WelcomeToModule("Entering [green]Start Study Session[/] Module...");

            List<ReadStudyStackDto> studyStackDtos =
                await studyStackService.GetStudyStacksAsync();

            // TODO: Move to input validation helper
            if (!studyStackDtos.Any() )
            {
                userInput.WaitForContinue("There are [red]No Study Stacks found.[/] There must be at least [blue]one[/] Study Stack" +
                    " in order to have a Study Session. Press any key to continue...");
                return;
            }

            ReadStudyStackDto selectedStack = userInput.GetUserChoice<ReadStudyStackDto>("Which Study Stack will you use for your session?", studyStackDtos);

            if (!selectedStack.Flashcards.Any())
            {
                userInput.WaitForContinue($"There are [red]No Flashcards associated with {selectedStack}. There must be at least [blue]one[/] Flashcard" +
                    " in order to have a Study Session. Press any key to continue...");
                return;
            }

            int questionNumber = 0;
            int score = 0;
            foreach (ReadFlashcardDto flashcardDto in selectedStack.Flashcards)
            {
                questionNumber++;
                userInput.DisplayQuestion(flashcardDto.FrontText, questionNumber);
                userInput.WaitForContinue("Press any key to see the answer...");

                userInput.DisplayAnswer(flashcardDto.BackText);

                char response = userInput.GetUserChoice<char>("Did you get the answer correct?", ['y', 'n']);

                if (response == 'y')
                    score++;
            }

            if (await studySessionService.CreateStudySessionAsync(new CreateStudySessionDto(selectedStack.Id, score,
                    DateTime.Now)) == -1)
            {
                userInput.WaitForContinue(
                    "[red}Error; there was a problem creating the Study Session![/] Returning to main menu...");
                return;
            }

            userInput.DisplayScore(score, questionNumber);
            //AnsiConsole.MarkupLine($"Study Session [green]complete[/]. Your score was; [green]{score}/{questionNumber}[/]");
    }
}