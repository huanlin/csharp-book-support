// Demo: Null Check Patterns

Console.WriteLine("=== Null Check Pattern Example ===\n");

// --------------------------------------------------------------
// 1. Behavior when handling null
// --------------------------------------------------------------
Console.WriteLine("1. Behavior when handling null");
Console.WriteLine(new string('-', 40));

object? obj = null;

// Type pattern will not match null
if (obj is string s)
{
    Console.WriteLine($"Is a string: {s}");
}
else if (obj is null)
{
    Console.WriteLine("obj is null (matched using is null)");
}

// --------------------------------------------------------------
// 2. Together with nullable reference types
// --------------------------------------------------------------
Console.WriteLine("\n2. Together with nullable reference types");
Console.WriteLine(new string('-', 40));

ProcessName("Alice");
ProcessName(null);
ProcessName("   ");

// --------------------------------------------------------------
// 3. is not Pattern
// --------------------------------------------------------------
Console.WriteLine("\n3. is not Pattern");
Console.WriteLine(new string('-', 40));

object value = "Hello";

if (value is not null)
{
    Console.WriteLine($"value is not null: {value}");
}

if (value is not int)
{
    Console.WriteLine($"value is not an int, actual type: {value?.GetType().Name ?? "null"}");
}

Console.WriteLine("\n=== Example End ===");

// --------------------------------------------------------------
// Helper Methods
// --------------------------------------------------------------

static void ProcessName(string? name)
{
    if (name is string validName && !string.IsNullOrWhiteSpace(validName))
    {
        // validName is string (non-null), the compiler knows this
        Console.WriteLine($"Name: {validName.Trim()}, Length: {validName.Trim().Length}");
    }
    else
    {
        Console.WriteLine("No valid name provided");
    }
}
