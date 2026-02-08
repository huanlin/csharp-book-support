// 示範重複求值的陷阱

Console.WriteLine("=== 5. 重複求值的陷阱 ===");
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
