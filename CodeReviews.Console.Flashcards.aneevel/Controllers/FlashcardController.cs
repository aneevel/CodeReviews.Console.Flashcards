using System.ComponentModel;
using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Controllers;

internal class FlashcardController(IFlashcardService flashcardService, IStudyStackService studyStackService) : IFlashcardController
{
    public async Task HandleMainMenuSelectionAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Welcome to the [green]Flashcards Module[/]. Please choose the [blue]operation[/] you would like to perform.");

        FlashcardMenuOptions option = AnsiConsole.Prompt(
            new SelectionPrompt<FlashcardMenuOptions>()
                .Title("Select an [blue]operation[/]:")
                .AddChoices(Enum.GetValues<FlashcardMenuOptions>())
                .UseConverter(option => option.GetDisplayName())
        );

        switch (option)
        {
            case FlashcardMenuOptions.CreateAFlashcard:
                await HandleCreateOperationAsync();
                break;
            case FlashcardMenuOptions.EditAFlashcard:
                await HandleUpdateOperationAsync();
                break;
            case FlashcardMenuOptions.RemoveAFlashcard:
                await HandleDeleteOperationAsync();
                break;
            case FlashcardMenuOptions.ExitFlashcardModule:
                AnsiConsole.Clear();
                // TODO: Wait on user input and provide message
                break;
            default:
                throw new InvalidAsynchronousStateException("System failure; Unknown Menu Option provided.");
        }
    }

    public async Task HandleCreateOperationAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Entering [green]Create Flashcard[/] Module...");

        try
        {
            List<ReadStudyStackDto> studyStacks =
                await studyStackService.GetStudyStacksAsync();

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

            await flashcardService
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
                                   Class: {nameof(FlashcardController)}
                                   Method: {nameof(HandleCreateOperationAsync)}
                                   There was an error reading StudyStacks in the HandleCreateOperationAsync module: {ex.Message}
                                   """;
            AnsiConsole.MarkupLine(errorMessage);
        }
    }

    public Task HandleDeleteOperationAsync()
    {
        throw new NotImplementedException();
    }

    public async Task HandleUpdateOperationAsync()
    {
        AnsiConsole.Clear();

        AnsiConsole.MarkupLine("Please select a Flashcard to [green]Edit[/] ...");

        // TODO: Should be in a try/catch
        try
        {
            List<ReadFlashcardDto> flashcardDtos =
                await flashcardService.GetFlashcardsAsync();

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
                        await studyStackService
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
                    await flashcardService.UpdateFlashcardAsync(
                        new UpdateFlashcardDto(selectedStack.Id, newFrontText, newBackText, selectedFlashcard.Id));
                    break;
                }
                case 'n':
                    AnsiConsole.MarkupLine("Aborting edit; returning to main module.");
                    break;
            }
        }
        // TODO: What should this catch?
        catch
        {
        }
    }
}