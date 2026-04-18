using CodeReviews.Console.Flashcards.aneevel;
using CodeReviews.Console.Flashcards.aneevel.Controllers;
using CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Database;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories;
using CodeReviews.Console.Flashcards.aneevel.Database.Repositories.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Services;
using CodeReviews.Console.Flashcards.aneevel.Services.Interfaces;
using CodeReviews.Console.Flashcards.aneevel.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

try
{
    await Init();
}
catch (Exception ex)
{
    Console.WriteLine($"There was an error during application execution: {ex.Message}");
}

return;

async Task Init()
{
    IConfigurationBuilder configBuilder =
        new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
    IConfigurationRoot configuration = configBuilder.Build();

    DatabaseSettings? databaseSettings =
        configuration.GetRequiredSection("DatabaseSettings").Get<DatabaseSettings>();

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
            .WriteTo.File(
                path: "logs/error.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:MM-dd-yyyy HH:mm:ss} [[{Level 3}]] {Message:lj}{NewLine}{Exception}")
            )
        .CreateLogger();

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

    ServiceProvider serviceProvider = services.BuildServiceProvider();

    await serviceProvider.GetRequiredService<IDatabaseInitializer>().InitializeDatabaseAsync();

    FlashcardsApplication flashcardsApplication = new FlashcardsApplication(serviceProvider.GetRequiredService<IFlashcardController>(), serviceProvider.GetRequiredService<IStudyStacksController>(), serviceProvider.GetRequiredService<IStudySessionsController>(), serviceProvider.GetRequiredService<UserInput>());
    await flashcardsApplication.Run();
}