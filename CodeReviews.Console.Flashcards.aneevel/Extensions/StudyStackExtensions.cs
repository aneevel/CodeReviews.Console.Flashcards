using CodeReviews.Console.Flashcards.aneevel.Entities;

namespace CodeReviews.Console.Flashcards.aneevel.Extensions;

internal static class StudyStackExtensions
{
    extension(List<StudyStack> studyStacks)
    {
        internal List<StudyStack> TakeElementsAtIndex(int skipSize, int takeSize)
        {
            return studyStacks.Skip(skipSize).Take(takeSize).ToList();
        }
    }
}