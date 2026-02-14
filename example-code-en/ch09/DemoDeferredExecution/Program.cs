// Demo: Deferred Execution and Infinite Sequences

Console.WriteLine("=== 2. Deferred Execution Demo ===");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Creating LINQ query...");
var list = new List<int> { 1, 2, 3, 4, 5 };
var query = list.Where(n => n > 2);
Console.WriteLine("Query created, but not yet executed");

Console.WriteLine("\nModifying original list (adding 6)...");
list.Add(6);

Console.WriteLine("Executing query now:");
foreach (var n in query)
{
    Console.Write($"{n} ");
}
Console.WriteLine("\n(6 is also included in the result because the query is deferred)");

Console.WriteLine("\n=== 3. Infinite Sequence: Fibonacci ===");
Console.WriteLine(new string('-', 40));

Console.WriteLine("First 15 Fibonacci numbers:");
foreach (var n in Fibonacci().Take(15))
{
    Console.Write($"{n} ");
}
Console.WriteLine();

Console.WriteLine("\n=== Example End ===");

// --------------------------------------------------------------
// Iterator Method
// --------------------------------------------------------------

static IEnumerable<long> Fibonacci()
{
    long current = 1, next = 1;

    while (true) // Infinite loop! But safe within an iterator
    {
        yield return current;
        (current, next) = (next, current + next);
    }
}
