// Demo: Multiple Enumeration Trap

Console.WriteLine("=== 5. Multiple Enumeration Trap ===");
Console.WriteLine(new string('-', 40));

var items = new List<int> { 1, 2, 3 };
var transformed = items.Select(n =>
{
    Console.WriteLine($"  [Calculating] {n} * 10");
    return n * 10;
});

Console.WriteLine("First call to Count():");
Console.WriteLine($"  Result: {transformed.Count()}");

Console.WriteLine("\nSecond call to Count():");
Console.WriteLine($"  Result: {transformed.Count()}");

Console.WriteLine("\n(Note: The calculation is re-executed every time!)");

// Solution
Console.WriteLine("\nUsing ToList() to materialize the result:");
var materialized = items.Select(n =>
{
    Console.WriteLine($"  [Calculating] {n} * 10");
    return n * 10;
}).ToList();

Console.WriteLine($"First Count: {materialized.Count}");
Console.WriteLine($"Second Count: {materialized.Count}");
Console.WriteLine("(Calculated only once)");

Console.WriteLine("\n=== Example End ===");
