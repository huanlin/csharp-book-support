// 示範 var 模式

Console.WriteLine("=== var 模式範例 ===\n");

Console.WriteLine($"IsJaneOrJohn(\"Jane\"): {IsJaneOrJohn("Jane")}");
Console.WriteLine($"IsJaneOrJohn(\"JOHN\"): {IsJaneOrJohn("JOHN")}");
Console.WriteLine($"IsJaneOrJohn(\"Alice\"): {IsJaneOrJohn("Alice")}");

Console.WriteLine("\n=== 範例結束 ===");

// --------------------------------------------------------------
// 使用 var 模式
// --------------------------------------------------------------

static bool IsJaneOrJohn(string name) =>
    name.ToUpper() is var upper && (upper == "JANE" || upper == "JOHN");
