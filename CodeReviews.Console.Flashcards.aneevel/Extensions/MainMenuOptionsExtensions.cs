using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CodeReviews.Console.Flashcards.aneevel.Enums;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions;

internal static class MainMenuOptionsExtensions
{
    extension(MainMenuOptions option)
    {
        public string GetDisplayName()
        {
            FieldInfo? fieldInfo = option.GetType().GetField(option.ToString());

            return (
                fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false)
                    is DisplayAttribute[] { Length: > 0 } attributes
                    ? attributes[0].Name
                    : option.ToString()
            ) ?? string.Empty;
        }
    }
}