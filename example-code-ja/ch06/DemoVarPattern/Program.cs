// デモ: var パターン

Console.WriteLine("=== var パターンの例 ===\n");

Console.WriteLine($"IsJaneOrJohn(\"Jane\"): {IsJaneOrJohn("Jane")}");
Console.WriteLine($"IsJaneOrJohn(\"JOHN\"): {IsJaneOrJohn("JOHN")}");
Console.WriteLine($"IsJaneOrJohn(\"Alice\"): {IsJaneOrJohn("Alice")}");
Console.WriteLine($"IsJaneOrJohn(null): {IsJaneOrJohn(null)}");

Console.WriteLine("\n=== 例の終了 ===");

// --------------------------------------------------------------
// var パターンで中間結果を受け取る。
// var パターン自体は null にマッチするが、その前の操作は null 安全である必要がある。
// --------------------------------------------------------------

static bool IsJaneOrJohn(string? name) =>
    name?.ToUpperInvariant() is var upper && (upper == "JANE" || upper == "JOHN");
