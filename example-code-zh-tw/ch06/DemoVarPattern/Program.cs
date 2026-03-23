// 示範 var 模式

Console.WriteLine("=== var 模式範例 ===\n");

Console.WriteLine($"IsJaneOrJohn(\"Jane\"): {IsJaneOrJohn("Jane")}");
Console.WriteLine($"IsJaneOrJohn(\"JOHN\"): {IsJaneOrJohn("JOHN")}");
Console.WriteLine($"IsJaneOrJohn(\"Alice\"): {IsJaneOrJohn("Alice")}");
Console.WriteLine($"IsJaneOrJohn(null): {IsJaneOrJohn(null)}");

Console.WriteLine("\n=== 範例結束 ===");

// --------------------------------------------------------------
// 使用 var 模式擷取中間結果。
// var 模式本身可以匹配 null，但前面的運算若不接受 null，仍會先拋出例外。
// --------------------------------------------------------------

static bool IsJaneOrJohn(string? name) =>
    name?.ToUpperInvariant() is var upper && (upper == "JANE" || upper == "JOHN");
