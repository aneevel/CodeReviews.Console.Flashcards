using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.DTOs.StudyStackDTOs;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Controllers;

internal class StudyStacksController(IStudyStackService service, UserInput userInput) : IStudyStacksController
{
    public async Task HandleMainMenuSelectionAsync()
    {
        userInput.WelcomeToModule(
           "Welcome to the [green]Stacks Module[/]! Please choose an [blue]operation[/] you would like to perform."
           );

        StackMenuOptions option =
            userInput.GetUserChoice<StackMenuOptions>("Select an [blue]operation[/]:", Enum.GetValues<StackMenuOptions>(), option => option.GetDisplayName());

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
                userInput.WaitForContinue("Exiting to main menu. Press any key to continue...");
                return;
            default:
                throw new InvalidOperationException("Unknown Menu Option provided!");
        }
    }

    public async Task HandleCreateOperationAsync()
    {
        userInput.WelcomeToModule("[green]Creating[/] a new Study Stack...");

        string stackName = userInput.GetUserInput<string>("What should the [green]name[/] of this stack be?");

        CreateStudyStackDto stack = new CreateStudyStackDto(stackName);

        if (await service.CreateStudyStackAsync(stack) == -1)
            userInput.WaitForContinue("[red]Unable to create Study Stack![/] Returning to main menu. Press any key to continue...");
    }

    public async Task HandleReadOperationAsync()
    {
        userInput.WelcomeToModule("[green]Viewing[/] all Study Stacks...");

            List<ReadStudyStackDto> studyStacks =
                await service.GetStudyStacksAsync();

            if (!studyStacks.Any())
            {
                userInput.WaitForContinue(
                    "[red]No Study Stacks to read![/] Make sure to create some before viewing. Returning to main menu. Press any key to continue...");
                return;
            }

            // TODO: Handle table building
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

    public async Task HandleUpdateOperationAsync()
    {
        userInput.WelcomeToModule("Please select a Study Stack to [green]Edit[/]...");

        List<ReadStudyStackDto> studyStacks = await service.GetStudyStacksAsync();

        if (!studyStacks.Any())
        {
            userInput.WaitForContinue(
                "[red]There were no Study Stacks found to edit![/] Please add some in order to edit. Returning to main menu. Press any key to continue...");
                return;
        }

        ReadStudyStackDto selectedStack =
            userInput.GetUserChoice<ReadStudyStackDto>("Which Study Stack do you want to Edit?", studyStacks);

            char answer = userInput.GetUserChoice<char>($"You would like to [blue]edit {selectedStack}[/]? (y/n)", ['y', 'n']);

            switch (answer)
            {
                case 'y':
                {
                    // TODO: Move to edit
                    string newStackName = userInput.GetUserInput<string>("What should the new name of the stack be?");
                    if (await service.UpdateStudyStackAsync(
                            new UpdateStudyStackDto(newStackName, selectedStack.Id)) == -1)
                    {
                        userInput.WaitForContinue("[red]Unable to edit Study Stack![/] Study Stack will remain as it was. Returning to main menu. Press any key to continue...");
                    }

                    break;
                }
                case 'n':
                    userInput.WaitForContinue("[red]Aborting edit;[/] returning to main menu. Press any key to continue...");
                    break;
            }
    }

    public async Task HandleDeleteOperationAsync()
    {
        userInput.WelcomeToModule("Please select a Study Stack to [red]Delete[/]...");

            List<ReadStudyStackDto> studyStacks = await service.GetStudyStacksAsync();

            if (!studyStacks.Any())
            {
                userInput.WaitForContinue(
                    "[red]No Study Stacks found![/] You must have at least one to delete. Returning to main menu...");
                return;
            }

            ReadStudyStackDto selectedStack =
                userInput.GetUserChoice("Which Study Stack do you want to delete?", studyStacks);

            char answer = userInput.GetUserChoice<char>($"You would like to [red]delete[/] [blue]{selectedStack}[/]? (y/n)", ['y', 'n']);

            switch (answer)
            {
                case 'y':
                {
                    if (await service.DeleteStudyStackAsync(new DeleteStudyStackDto(selectedStack.Id)) == -1)
                        userInput.WaitForContinue("[red]Unable to delete study stack![/] Study stack will remain. Returning to main menu...");
                    break;
                }
                case 'n':
                {
                    userInput.WaitForContinue("[red]Aborting delete[/]; returning to main menu...");
                    break;
                }
            }
    }
}