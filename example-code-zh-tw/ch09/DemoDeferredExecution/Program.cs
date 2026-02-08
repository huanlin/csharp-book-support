// 示範延遲執行（Deferred Execution）與無限序列

Console.WriteLine("=== 2. 延遲執行展示 ===");
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

Console.WriteLine("\n=== 3. 無限序列：Fibonacci ===");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Fibonacci 前 15 個數字：");
foreach (var n in Fibonacci().Take(15))
{
    Console.Write($"{n} ");
}
Console.WriteLine();

Console.WriteLine("\n=== 範例結束 ===");

// --------------------------------------------------------------
// 迭代器方法
// --------------------------------------------------------------

static IEnumerable<long> Fibonacci()
{
    long current = 1, next = 1;

    while (true) // 無窮迴圈！但在迭代器中是安全的
    {
        yield return current;
        (current, next) = (next, current + next);
    }
}
