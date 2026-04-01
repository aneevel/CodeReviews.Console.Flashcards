namespace CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;

internal interface IStudySessionsController
{
    Task HandleMainMenuSelectionAsync();
    Task HandleStartSessionOperationAsync();
    Task HandleViewSessionsOperationAsync();
}