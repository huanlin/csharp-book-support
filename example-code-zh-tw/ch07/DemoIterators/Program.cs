// 示範迭代器（Iterators）：yield return、延遲執行

Console.WriteLine("=== 迭代器範例 ===\n");

// --------------------------------------------------------------
// 1. 基本 yield return
// --------------------------------------------------------------
Console.WriteLine("1. 基本 yield return");
Console.WriteLine(new string('-', 40));

Console.WriteLine("呼叫 GetNumbers()...");
var numbers = GetNumbers(); // 這裡什麼都不會印出！
Console.WriteLine("開始 foreach...\n");

foreach (var n in numbers)
{
    Console.WriteLine($"收到 {n}");
}

// --------------------------------------------------------------
// 2. 延遲執行（Deferred Execution）
// --------------------------------------------------------------
Console.WriteLine("\n2. 延遲執行展示");
Console.WriteLine(new string('-', 40));

Console.WriteLine("建立 LINQ 查詢...");
var list = new List<int> { 1, 2, 3, 4, 5 };
var query = list.Where(n => n > 2);
Console.WriteLine("查詢已建立，但尚未執行");

Console.WriteLine("\n修改原始清單（加入 6）...");
list.Add(6);

Console.WriteLine("現在執行查詢：");
foreach (var n in query)
{
    Console.Write($"{n} ");
}
Console.WriteLine("\n（6 也包含在結果中，因為查詢是延遲執行的）");

// --------------------------------------------------------------
// 3. 無限序列：Fibonacci
// --------------------------------------------------------------
Console.WriteLine("\n3. 無限序列：Fibonacci");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Fibonacci 前 15 個數字：");
foreach (var n in Fibonacci().Take(15))
{
    Console.Write($"{n} ");
}
Console.WriteLine();

// --------------------------------------------------------------
// 4. yield break
// --------------------------------------------------------------
Console.WriteLine("\n4. yield break");
Console.WriteLine(new string('-', 40));

var source = new List<int> { 1, 2, 3, -1, 4, 5 };
Console.WriteLine($"來源：{string.Join(", ", source)}");
Console.WriteLine($"GetValidNumbers（遇到負數停止）：{string.Join(", ", GetValidNumbers(source))}");

// --------------------------------------------------------------
// 5. 重複求值的陷阱
// --------------------------------------------------------------
Console.WriteLine("\n5. 重複求值的陷阱");
Console.WriteLine(new string('-', 40));

var items = new List<int> { 1, 2, 3 };
var transformed = items.Select(n =>
{
    Console.WriteLine($"  [計算] {n} * 10");
    return n * 10;
});

Console.WriteLine("第一次呼叫 Count()：");
Console.WriteLine($"  結果：{transformed.Count()}");

Console.WriteLine("\n第二次呼叫 Count()：");
Console.WriteLine($"  結果：{transformed.Count()}");

Console.WriteLine("\n（注意：每次都重新執行計算！）");

// 解決方法
Console.WriteLine("\n使用 ToList() 固定結果：");
var materialized = items.Select(n =>
{
    Console.WriteLine($"  [計算] {n} * 10");
    return n * 10;
}).ToList();

Console.WriteLine($"第一次 Count：{materialized.Count}");
Console.WriteLine($"第二次 Count：{materialized.Count}");
Console.WriteLine("（只計算一次）");

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 迭代器方法
// ============================================================

static IEnumerable<int> GetNumbers()
{
    Console.WriteLine("  產生 1");
    yield return 1;

    Console.WriteLine("  產生 2");
    yield return 2;

    Console.WriteLine("  產生 3");
    yield return 3;
}

static IEnumerable<long> Fibonacci()
{
    long current = 1, next = 1;

    while (true) // 無窮迴圈！但在迭代器中是安全的
    {
        yield return current;
        (current, next) = (next, current + next);
    }
}

static IEnumerable<int> GetValidNumbers(List<int> source)
{
    foreach (var n in source)
    {
        if (n < 0) yield break; // 遇到負數就停止
        yield return n;
    }
}
