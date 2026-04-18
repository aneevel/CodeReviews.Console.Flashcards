using CodeReviews.Console.Flashcards.aneevel.Controllers;
using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Database;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Enums;
using CodeReviews.Console.Flashcards.aneevel.Extensions;
using CodeReviews.Console.Flashcards.aneevel.Services;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Serilog;
// ReSharper disable All

namespace CodeReviews.Console.Flashcards.aneevel;

public sealed class FlashcardsApplication(IFlashcardController flashcardController, IStudyStacksController studyStacksController, IStudySessionsController studySessionsController, UserInput userInput)
{
    public async Task Run()
    {
            while (true)
            {
                MainMenuOptions option = userInput.GetUserChoice<MainMenuOptions>("Select a [green]module[/]:",
                    Enum.GetValues<MainMenuOptions>(), option => option.GetDisplayName());

                switch (option)
                {
                    case MainMenuOptions.EnterFlashcardModule:
                        await flashcardController
                            .HandleMainMenuSelectionAsync();
                        break;
                    case MainMenuOptions.EnterStacksModule:
                        await studyStacksController
                            .HandleMainMenuSelectionAsync();
                        break;
                    case MainMenuOptions.EnterStudySessionsModule:
                        await studySessionsController
                            .HandleMainMenuSelectionAsync();
                        break;
                    case MainMenuOptions.ExitApplication:
                        return;
                }
            }
    }
}