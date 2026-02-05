using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions;

internal static class EnumExtensions
{
    public static string GetDisplayName(this Enum option)
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

