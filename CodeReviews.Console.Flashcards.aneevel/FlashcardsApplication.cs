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
                .AddScoped<IFlashcardRepository, FlashcardRepository>()
                .AddScoped<IStudySessionRepository, StudySessionRepository>()
                .AddScoped<IStudyStackService, StudyStackService>()
                .AddScoped<IFlashcardService, FlashcardService>()
                .AddScoped<IStudySessionService, StudySessionService>()
                .AddScoped<IFlashcardController, FlashcardController>()
                .AddScoped<IStudyStacksController, StudyStacksController>()
                .AddScoped<IStudySessionsController, StudySessionsController>();

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
                        await _serviceProvider.GetRequiredService<IFlashcardController>()
                            .HandleMainMenuSelectionAsync();
                        break;
                    case MainMenuOptions.EnterStacksModule:
                        await _serviceProvider.GetRequiredService<IStudyStacksController>()
                            .HandleMainMenuSelectionAsync();
                        break;
                    case MainMenuOptions.EnterStudySessionsModule:
                        await _serviceProvider.GetRequiredService<IStudySessionsController>()
                            .HandleMainMenuSelectionAsync();
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
}