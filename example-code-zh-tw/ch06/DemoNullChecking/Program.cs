// 示範 Null 檢查相關模式

Console.WriteLine("=== Null 檢查模式範例 ===\n");

// --------------------------------------------------------------
// 1. 處理 null 的行為
// --------------------------------------------------------------
Console.WriteLine("1. 處理 null 的行為");
Console.WriteLine(new string('-', 40));

object? obj = null;

// 型別模式不會匹配 null
if (obj is string s)
{
    Console.WriteLine($"是字串：{s}");
}
else if (obj is null)
{
    Console.WriteLine("obj 是 null（使用 is null 匹配）");
}

// --------------------------------------------------------------
// 2. 搭配 nullable reference types
// --------------------------------------------------------------
Console.WriteLine("\n2. 搭配 nullable reference types");
Console.WriteLine(new string('-', 40));

ProcessName("Alice");
ProcessName(null);
ProcessName("   ");

// --------------------------------------------------------------
// 3. is not 模式
// --------------------------------------------------------------
Console.WriteLine("\n3. is not 模式");
Console.WriteLine(new string('-', 40));

object value = "Hello";

if (value is not null)
{
    Console.WriteLine($"value 不是 null：{value}");
}

if (value is not int)
{
    Console.WriteLine($"value 不是 int，實際型別：{value?.GetType().Name ?? "null"}");
}

Console.WriteLine("\n=== 範例結束 ===");

// --------------------------------------------------------------
// 輔助方法
// --------------------------------------------------------------

static void ProcessName(string? name)
{
    if (name is string validName && !string.IsNullOrWhiteSpace(validName))
    {
        // validName 是 string（非 null），編譯器知道這點
        Console.WriteLine($"名字：{validName.Trim()}，長度：{validName.Trim().Length}");
    }
    else
    {
        Console.WriteLine("沒有提供有效的名字");
    }
}
