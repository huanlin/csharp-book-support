// Demo: yield break

Console.WriteLine("=== 4. yield break ===");
Console.WriteLine(new string('-', 40));

var source = new List<int> { 1, 2, 3, -1, 4, 5 };
Console.WriteLine($"Source: {string.Join(", ", source)}");
Console.WriteLine($"GetValidNumbers (stops at negative number): {string.Join(", ", GetValidNumbers(source))}");

Console.WriteLine("\n=== Example End ===");

// --------------------------------------------------------------
// Iterator Method
// --------------------------------------------------------------

static IEnumerable<int> GetValidNumbers(List<int> source)
{
    foreach (var n in source)
    {
        if (n < 0) yield break; // Stop when a negative number is encountered
        yield return n;
    }
}
