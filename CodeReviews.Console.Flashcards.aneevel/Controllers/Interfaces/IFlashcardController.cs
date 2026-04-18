namespace CodeReviews.Console.Flashcards.aneevel.Controllers.Interfaces;

public interface IFlashcardController
{
    Task HandleMainMenuSelectionAsync();
    Task HandleCreateOperationAsync();
    Task HandleDeleteOperationAsync();
    Task HandleUpdateOperationAsync();
}