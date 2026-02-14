// Demo: Basic yield return

Console.WriteLine("=== 1. Basic yield return ===");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Calling GetNumbers()...");
var numbers = GetNumbers(); // Nothing will be printed here!
Console.WriteLine("Starting foreach...\n");

foreach (var n in numbers)
{
    Console.WriteLine($"Received {n}");
}

Console.WriteLine("\n=== Example End ===");

// --------------------------------------------------------------
// Iterator Method
// --------------------------------------------------------------

static IEnumerable<int> GetNumbers()
{
    Console.WriteLine("  Yielding 1");
    yield return 1;

    Console.WriteLine("  Yielding 2");
    yield return 2;

    Console.WriteLine("  Yielding 3");
    yield return 3;
}
