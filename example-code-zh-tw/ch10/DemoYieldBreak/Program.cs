// 示範 yield break

Console.WriteLine("=== 4. yield break ===");
Console.WriteLine(new string('-', 40));

var source = new List<int> { 1, 2, 3, -1, 4, 5 };
Console.WriteLine($"來源：{string.Join(", ", source)}");
Console.WriteLine($"GetValidNumbers（遇到負數停止）：{string.Join(", ", GetValidNumbers(source))}");

Console.WriteLine("\n=== 範例結束 ===");

// --------------------------------------------------------------
// 迭代器方法
// --------------------------------------------------------------

static IEnumerable<int> GetValidNumbers(List<int> source)
{
    foreach (var n in source)
    {
        if (n < 0) yield break; // 遇到負數就停止
        yield return n;
    }
}
