// Demo: var implicit typed variables

// Basic usage: compiler infers the type
int i = 10;
var j = 10;  // Compiler infers it as int
Console.WriteLine($"Type of i: {i.GetType().Name}");
Console.WriteLine($"Type of j: {j.GetType().Name}");

// Implicitly typed array
var numbers = new[] { 10, 20, 30, 40 };  // Inferred as int[]
Console.WriteLine($"\nType of numbers: {numbers.GetType().Name}");

foreach (var x in numbers)  // x is inferred as int
{
    Console.WriteLine($"  Element: {x}");
}

// Type inference: finding the best common type
var mixed = new[] { 1, 10000000000L };  // Inferred as long[]
Console.WriteLine($"\nType of mixed: {mixed.GetType().Name}");

// Using var with complex generic types
var dict = new Dictionary<string, int>();  // Avoid repeating the type name
dict["apple"] = 3;
dict["banana"] = 5;
Console.WriteLine($"\nType of dict: {dict.GetType().Name}");

// Using var with LINQ query results
var grouped = dict
    .GroupBy(kvp => kvp.Value > 3)
    .Select(g => new { MoreThan3 = g.Key, Count = g.Count() });

Console.WriteLine("\nLINQ Result:");
foreach (var item in grouped)
{
    Console.WriteLine($"  MoreThan3={item.MoreThan3}, Count={item.Count}");
}
