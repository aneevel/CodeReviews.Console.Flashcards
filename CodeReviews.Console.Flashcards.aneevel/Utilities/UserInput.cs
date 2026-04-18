using Spectre.Console;

namespace CodeReviews.Console.Flashcards.aneevel.Utilities;

public class UserInput
{
    public void WelcomeToModule(string welcomeMessage)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine(welcomeMessage);
    }

    public T GetUserChoice<T>(string message, ICollection<T> options) where T : notnull
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<T>()
                .Title(message)
                .AddChoices(options));
    }

    public T GetUserChoice<T>(string message, ICollection<T> options, Func<T, string> displayTransformer) where T : notnull
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<T>()
                .Title(message)
                .AddChoices(options)
                .UseConverter(displayTransformer));
    }

    public void WaitForContinue(string message)
    {
        AnsiConsole.MarkupLine(message);

        AnsiConsole.Console.Input.ReadKey(false);
    }

    public T GetUserInput<T>(string message)
    {
        return AnsiConsole.Ask<T>(message);
    }

    public void DisplayQuestion(string question, int questionNumber)
    {
        AnsiConsole.MarkupLine($"[green]Question: {questionNumber}[/]\n{question}");
    }

    public void DisplayAnswer(string answer)
    {
        AnsiConsole.MarkupLine($"[green]Answer: {answer}[/]");
    }

    public void DisplayScore(int score, int questions)
    {
        AnsiConsole.MarkupLine($"Study Session [green]complete[/]. Your score was [green]{score}/{questions}[/]");
    }
}