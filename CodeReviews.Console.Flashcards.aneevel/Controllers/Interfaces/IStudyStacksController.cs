namespace CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;

internal interface IStudyStacksController
{
    Task HandleMainMenuSelectionAsync();
    Task HandleCreateOperationAsync();
    Task HandleReadOperationAsync();
    Task HandleUpdateOperationAsync();
    Task HandleDeleteOperationAsync();
}