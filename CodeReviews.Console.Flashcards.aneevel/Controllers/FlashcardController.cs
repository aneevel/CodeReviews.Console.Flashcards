using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.FlashcardDTOs;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Utilities;

namespace CodeReviews.Console.Flashcards.aneevel.Controllers;

internal class FlashcardController(IFlashcardService flashcardService, IStudyStackService studyStackService, UserInput userInput) : IFlashcardController
{
    public async Task HandleMainMenuSelectionAsync()
    {
        userInput.WelcomeToModule(
            "Welcome to the [green]Flashcards Module[/]. Please choose the [blue]operation[/] you would like to perform.");

        FlashcardMenuOptions option =
            userInput.GetUserChoice<FlashcardMenuOptions>("Select an [blue]operation[/]", Enum.GetValues<FlashcardMenuOptions>(), option => option.GetDisplayName());

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
                userInput.WaitForContinue("Exiting to main menu. Press any key to continue...");
                break;
            default:
                throw new InvalidOperationException("System failure; Unknown Menu Option provided.");
        }
    }

    public async Task HandleCreateOperationAsync()
    {
       userInput.WelcomeToModule(
           "Entering [green]Create Flashcard[/] Module...");

            List<ReadStudyStackDto> studyStacks =
                await studyStackService.GetStudyStacksAsync();

            if (!studyStacks.Any())
            {
                userInput.WaitForContinue("There are [red]No Study Stacks found.[/] Flashcards must have an" +
                                       " associated Study Stack. Press any key to continue...");
                return;
            }

            ReadStudyStackDto selectedStack =
                userInput.GetUserChoice<ReadStudyStackDto>("Which Study Stack will this Flashcard belong to?", studyStacks);

            // TODO: Move to input validation helper
            string newFlashcardFrontText =
                userInput.GetUserInput<string>("What should the [blue]Front[/] of the Flashcard be?");
            string newFlashcardBackText =
                userInput.GetUserInput<string>("What should the [blue]Back[/] of the Flashcard be?");


            if (await flashcardService
                    .CreateFlashcardAsync(
                        new CreateFlashcardDto(
                            selectedStack.Id,
                            newFlashcardFrontText,
                            newFlashcardBackText
                        )
                    ) == -1)
            {
                userInput.WaitForContinue("[red]System error;[/] unable to create new Flashcard! Press any key to continue...");
            }
    }

    public async Task HandleDeleteOperationAsync()
    {
        userInput.WelcomeToModule("Please select a Flashcard to [red]Delete[/]...");

        List<ReadFlashcardDto> flashcardDtos = await flashcardService.GetFlashcardsAsync();

        if (!flashcardDtos.Any())
        {
            userInput.WaitForContinue(
                "[red]There are no Flashcards to delete. There must be at least one to use this module.[/] Returning to main menu. Press any key to continue...");
            return;
        }

        ReadFlashcardDto selectedFlashcard = userInput.GetUserChoice<ReadFlashcardDto>("Which Flashcard do you want to delete?", flashcardDtos);

        char answer =
            userInput.GetUserChoice<char>($"You would like to [red]delete[/] [blue]{selectedFlashcard}[/]? (y/n)", ['y', 'n']);

        switch (answer)
        {
            case 'y':
                await flashcardService.DeleteFlashcardAsync(new DeleteFlashcardDto(selectedFlashcard.Id));
                break;
            case 'n':
                userInput.WaitForContinue("Aborting Delete; returning to main menu. Press any key to continue...");
                break;
        }
    }

    public async Task HandleUpdateOperationAsync()
    {
        userInput.WelcomeToModule("Please select a Flashcard to [green]Edit[/] ...");

        List<ReadFlashcardDto> flashcardDtos =
            await flashcardService.GetFlashcardsAsync();

            if (!flashcardDtos.Any())
            {
                userInput.WaitForContinue(
                    "No Flashcards found. There must be at least one Flashcard to edit. Returning to main menu. Press any key to continue...");
                return;
            }

            ReadFlashcardDto selectedFlashcard =
                userInput.GetUserChoice<ReadFlashcardDto>("Which Flashcard do you want to edit?", flashcardDtos);

            char answer = userInput.GetUserChoice<char>($"You would like to [blue]edit {selectedFlashcard}[/]? (y/n)", ['y', 'n']);

            switch (answer)
            {
                case 'y':
                {
                    List<ReadStudyStackDto> studyStacks =
                        await studyStackService
                            .GetStudyStacksAsync();

                    if (!studyStacks.Any())
                    {
                        userInput.WaitForContinue(
                            "No Study Stacks found. There must be at least one Study Stack to edit Flashcards. Returning to main menu. Press any key to continue...");
                        return;
                    }

                    ReadStudyStackDto selectedStack =
                        userInput.GetUserChoice<ReadStudyStackDto>("Which Stack should this Flashcard belong to?", studyStacks);

                    string newFrontText = userInput.GetUserInput<string>("What should the Flashcard front text be?");
                    string newBackText = userInput.GetUserInput<string>("What should the Flashcard back text be?");
                    await flashcardService.UpdateFlashcardAsync(
                        new UpdateFlashcardDto(selectedStack.Id, newFrontText, newBackText, selectedFlashcard.Id));
                    break;
                }
                case 'n':
                    userInput.WaitForContinue("Aborting edit; returning to main module. Press any key to continue...");
                    break;
            }
    }
}