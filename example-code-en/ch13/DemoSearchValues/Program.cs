// Demo: SearchValues<T> (.NET 8+) optimizes repeated searches

using System.Buffers;

Console.WriteLine("=== SearchValues<T> (.NET 8+) ===\n");

string text = "Hello World! This is a test.";
int vowels = VowelCounter.CountVowels(text);
Console.WriteLine($"String: \"{text}\"");
Console.WriteLine($"Vowel count: {vowels}");

// Helper Class

public static class VowelCounter
{
    // Create reusable search values
    private static readonly SearchValues<char> _vowels = SearchValues.Create("aeiouAEIOU");

    public static int CountVowels(ReadOnlySpan<char> text)
    {
        int count = 0;
        int pos = 0;
        int idx;

        // Use SearchValues to optimize search
        while ((idx = text.Slice(pos).IndexOfAny(_vowels)) >= 0)
        {
            count++;
            pos += idx + 1; // Move to the next character
        }

        return count;
    }
}
