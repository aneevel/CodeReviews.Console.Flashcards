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

public sealed class FlashcardsApplication
{
    private ServiceProvider _serviceProvider;
    public async Task Run()
    {
        try
        {
            IConfigurationBuilder configBuilder =
                new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot configuration = configBuilder.Build();

            DatabaseSettings? databaseSettings =
                configuration.GetRequiredSection("DatabaseSettings").Get<DatabaseSettings>();

            // TODO: Catch when application does not have settings file
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = databaseSettings?.DataSource,
                UserID = databaseSettings?.UserId,
                Password = databaseSettings?.Password,
                InitialCatalog = databaseSettings?.InitialCatalog,
                TrustServerCertificate = databaseSettings!.TrustServerCertificate || false
            };

            ConnectionString connectionString = new ConnectionString(builder.ConnectionString);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Logger(logger => logger
                    .MinimumLevel.Error()
                    .WriteTo.File("logs/error.txt", rollingInterval: RollingInterval.Day)
                    )
                .CreateLogger();

            // TODO: this should all be top level
            IServiceCollection services = new ServiceCollection()
                .AddSingleton(connectionString)
                .AddSingleton<IConfigurationRoot>(configuration)
                .AddSingleton<UserInput>()
                .AddScoped<IDatabaseInitializer, SqlServerDatabaseInitializer>()
                .AddScoped<IStudyStackRepository, StudyStackRepository>()
                .AddScoped<IFlashcardRepository, FlashcardRepository>()
                .AddScoped<IStudySessionRepository, StudySessionRepository>()
                .AddScoped<IStudyStackService, StudyStackService>()
                .AddScoped<IFlashcardService, FlashcardService>()
                .AddScoped<IStudySessionService, StudySessionService>()
                .AddScoped<IFlashcardController, FlashcardController>()
                .AddScoped<IStudyStacksController, StudyStacksController>()
                .AddScoped<IStudySessionsController, StudySessionsController>()
                .AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

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
        catch (FileNotFoundException ex)
        {
            Log.Logger.Fatal(ex, "appsettings.json was not found; FlashcardsApplication requires a valid appsettings.json file.");
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Log.Logger.Fatal(ex, "An unknown error occurred!");
            Environment.Exit(1);
        }
    }
}