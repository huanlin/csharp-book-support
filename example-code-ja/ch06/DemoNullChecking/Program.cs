// デモ: Null チェックパターン

Console.WriteLine("=== Null チェックパターンの例 ===\n");

// --------------------------------------------------------------
// 1. null 取り扱い挙動
// --------------------------------------------------------------
Console.WriteLine("1. null 取り扱い挙動");
Console.WriteLine(new string('-', 40));

object? obj = null;

// type pattern は null に一致しない
if (obj is string s)
{
    Console.WriteLine($"string です: {s}");
}
else if (obj is null)
{
    Console.WriteLine("obj は null（is null で一致）");
}

// --------------------------------------------------------------
// 2. Nullable 参照型と併用
// --------------------------------------------------------------
Console.WriteLine("\n2. Nullable 参照型と併用");
Console.WriteLine(new string('-', 40));

ProcessName("Alice");
ProcessName(null);
ProcessName("   ");

// --------------------------------------------------------------
// 3. is not パターン
// --------------------------------------------------------------
Console.WriteLine("\n3. is not パターン");
Console.WriteLine(new string('-', 40));

object value = "Hello";

if (value is not null)
{
    Console.WriteLine($"value は null ではない: {value}");
}

if (value is not int)
{
    Console.WriteLine($"value は int ではない。実際の型: {value?.GetType().Name ?? "null"}");
}

Console.WriteLine("\n=== 例の終了 ===");

// --------------------------------------------------------------
// ヘルパーメソッド
// --------------------------------------------------------------

static void ProcessName(string? name)
{
    if (name is string validName && !string.IsNullOrWhiteSpace(validName))
    {
        // validName は非 null と判定される
        Console.WriteLine($"名前: {validName.Trim()}, 長さ: {validName.Trim().Length}");
    }
    else
    {
        Console.WriteLine("有効な名前が指定されていません");
    }
}
