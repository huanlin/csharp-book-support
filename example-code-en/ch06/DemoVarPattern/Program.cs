// Demo: var pattern

Console.WriteLine("=== var Pattern Example ===\n");

Console.WriteLine($"IsJaneOrJohn(\"Jane\"): {IsJaneOrJohn("Jane")}");
Console.WriteLine($"IsJaneOrJohn(\"JOHN\"): {IsJaneOrJohn("JOHN")}");
Console.WriteLine($"IsJaneOrJohn(\"Alice\"): {IsJaneOrJohn("Alice")}");
Console.WriteLine($"IsJaneOrJohn(null): {IsJaneOrJohn(null)}");

Console.WriteLine("\n=== Example End ===");

// --------------------------------------------------------------
// Use the var pattern to capture an intermediate result.
// The var pattern itself can match null, but earlier operations still need to be null-safe.
// --------------------------------------------------------------

static bool IsJaneOrJohn(string? name) =>
    name?.ToUpperInvariant() is var upper && (upper == "JANE" || upper == "JOHN");
