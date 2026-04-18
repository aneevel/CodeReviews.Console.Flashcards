namespace CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;

public interface IStudySessionsController
{
    Task HandleMainMenuSelectionAsync();
    Task HandleStartSessionOperationAsync();
    Task HandleViewSessionsOperationAsync();
}