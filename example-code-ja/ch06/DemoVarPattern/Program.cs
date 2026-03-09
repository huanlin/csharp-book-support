// デモ: var パターン

Console.WriteLine("=== var パターンの例 ===\n");

Console.WriteLine($"IsJaneOrJohn(\"Jane\"): {IsJaneOrJohn("Jane")}");
Console.WriteLine($"IsJaneOrJohn(\"JOHN\"): {IsJaneOrJohn("JOHN")}");
Console.WriteLine($"IsJaneOrJohn(\"Alice\"): {IsJaneOrJohn("Alice")}");

Console.WriteLine("\n=== 例の終了 ===");

// --------------------------------------------------------------
// var パターン利用
// --------------------------------------------------------------

static bool IsJaneOrJohn(string name) =>
    name.ToUpper() is var upper && (upper == "JANE" || upper == "JOHN");
