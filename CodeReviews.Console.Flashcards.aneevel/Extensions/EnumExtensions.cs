using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions;

internal static class EnumExtensions
{
    extension(Enum enumValue)
    {
        public string GetDisplayName()
        {
            FieldInfo? fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            return (
                fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false)
                    is DisplayAttribute[] { Length: > 0 } attributes
                    ? attributes[0].Name
                    : enumValue.ToString()
            ) ?? string.Empty;
        }
    }
}