// Demo: Extending LINQ
// LINQ itself is implemented via extension methods. You can add your own LINQ operators.

// Custom LINQ extension methods
public static class LinqExtensions
{
    /// <summary>
    /// Filters out null elements, returning only non-null items.
    /// </summary>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
        where T : class
    {
        return source.Where(x => x != null)!;
    }

    /// <summary>
    /// Splits a sequence into batches of a specified size.
    /// </summary>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        if (batchSize <= 0)
            throw new ArgumentException("Batch size must be greater than 0", nameof(batchSize));

        var batch = new List<T>(batchSize);
        foreach (var item in source)
        {
            batch.Add(item);
            if (batch.Count == batchSize)
            {
                yield return batch;
                batch = new List<T>(batchSize);
            }
        }

        if (batch.Count > 0)
            yield return batch;
    }

    /// <summary>
    /// Takes every Nth element from the sequence.
    /// </summary>
    public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> source, int step)
    {
        if (step <= 0)
            throw new ArgumentException("Step must be greater than 0", nameof(step));

        int index = 0;
        foreach (var item in source)
        {
            if (index % step == 0)
                yield return item;
            index++;
        }
    }
}

// Demo Main Program
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Extending LINQ Demo ===\n");

        // Scenario 1: WhereNotNull - Filtering nulls
        Console.WriteLine("Scenario 1: WhereNotNull - Filtering null elements");
        var names = new[] { "Alice", null, "Bob", null, "Charlie" };
        Console.WriteLine($"  Original data: {string.Join(", ", names.Select(n => n ?? "null"))}");
        var validNames = names.WhereNotNull().ToList();
        Console.WriteLine($"  Filtered results: {string.Join(", ", validNames)}\n");

        // Scenario 2: Batch - Splitting sequence into batches
        Console.WriteLine("Scenario 2: Batch - Splitting sequence into batches");
        var numbers = Enumerable.Range(1, 10);
        Console.WriteLine($"  Original data: {string.Join(", ", numbers)}");
        Console.WriteLine($"  Split into batches of 3:");
        foreach (var batch in numbers.Batch(3))
        {
            Console.WriteLine($"    [{string.Join(", ", batch)}]");
        }

        // Scenario 3: TakeEvery - Periodic sampling
        Console.WriteLine("\nScenario 3: TakeEvery - Taking every Nth element");
        var items = Enumerable.Range(1, 12);
        Console.WriteLine($"  Original data: {string.Join(", ", items)}");
        var sampled = items.TakeEvery(3).ToList();
        Console.WriteLine($"  Taking every 3rd element: {string.Join(", ", sampled)}");

        // Explanation
        Console.WriteLine("\n=== Explanation ===");
        Console.WriteLine("These extension methods demonstrate how to add new features to LINQ.");
        Console.WriteLine("Note: .NET 6+ has built-in DistinctBy and Chunk (similar to Batch).");
        Console.WriteLine("Custom extension methods are suitable for project-specific query logic.");
    }
}
