namespace CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;

internal interface IFlashcardController
{
    Task HandleMainMenuSelectionAsync();
    Task HandleCreateOperationAsync();
    Task HandleDeleteOperationAsync();
    Task HandleUpdateOperationAsync();
}