// 示範基本 yield return

Console.WriteLine("=== 1. 基本 yield return ===");
Console.WriteLine(new string('-', 40));

Console.WriteLine("呼叫 GetNumbers()...");
var numbers = GetNumbers(); // 這裡什麼都不會印出！
Console.WriteLine("開始 foreach...\n");

foreach (var n in numbers)
{
    Console.WriteLine($"收到 {n}");
}

Console.WriteLine("\n=== 範例結束 ===");

// --------------------------------------------------------------
// 迭代器方法
// --------------------------------------------------------------

static IEnumerable<int> GetNumbers()
{
    Console.WriteLine("  產生 1");
    yield return 1;

    Console.WriteLine("  產生 2");
    yield return 2;

    Console.WriteLine("  產生 3");
    yield return 3;
}
