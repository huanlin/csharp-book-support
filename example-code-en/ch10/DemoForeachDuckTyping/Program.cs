using System.Collections;

Console.WriteLine("=== Making int work with foreach (duck typing) ===\n");

foreach (var i in 3)
{
    Console.WriteLine($"Hello {i}");
}

Console.WriteLine("\nExplanation:");
Console.WriteLine("foreach looks for a member named GetEnumerator.");
Console.WriteLine("As long as the enumerator matches the pattern, implementing IEnumerable is not required.");

public static class IntExtensions
{
    // Use an extension method to make int iterable by foreach.
    public static IEnumerator<int> GetEnumerator(this int count)
    {
        return Enumerable.Range(0, count).GetEnumerator();
    }
}
