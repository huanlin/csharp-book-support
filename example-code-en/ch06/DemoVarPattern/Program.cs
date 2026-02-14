// Demo: var pattern

Console.WriteLine("=== var Pattern Example ===\n");

Console.WriteLine($"IsJaneOrJohn(\"Jane\"): {IsJaneOrJohn("Jane")}");
Console.WriteLine($"IsJaneOrJohn(\"JOHN\"): {IsJaneOrJohn("JOHN")}");
Console.WriteLine($"IsJaneOrJohn(\"Alice\"): {IsJaneOrJohn("Alice")}");

Console.WriteLine("\n=== Example End ===");

// --------------------------------------------------------------
// Using var pattern
// --------------------------------------------------------------

static bool IsJaneOrJohn(string name) =>
    name.ToUpper() is var upper && (upper == "JANE" || upper == "JOHN");
