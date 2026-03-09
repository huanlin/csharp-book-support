// デモ: Multiple Enumeration の落とし穴

Console.WriteLine("=== 5. Multiple Enumeration の落とし穴 ===");
Console.WriteLine(new string('-', 40));

var items = new List<int> { 1, 2, 3 };
var transformed = items.Select(n =>
{
    Console.WriteLine($"  [計算] {n} * 10");
    return n * 10;
});

Console.WriteLine("Count() 1回目:");
Console.WriteLine($"  結果: {transformed.Count()}");

Console.WriteLine("\nCount() 2回目:");
Console.WriteLine($"  結果: {transformed.Count()}");

Console.WriteLine("\n（毎回再計算される）");

Console.WriteLine("\nToList() で実体化する解決策:");
var materialized = items.Select(n =>
{
    Console.WriteLine($"  [計算] {n} * 10");
    return n * 10;
}).ToList();

Console.WriteLine($"Count 1回目: {materialized.Count}");
Console.WriteLine($"Count 2回目: {materialized.Count}");
Console.WriteLine("（計算は1回だけ）");

Console.WriteLine("\n=== 例の終了 ===");
