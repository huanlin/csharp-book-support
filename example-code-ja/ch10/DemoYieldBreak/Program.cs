// デモ: yield break

Console.WriteLine("=== 4. yield break ===");
Console.WriteLine(new string('-', 40));

var source = new List<int> { 1, 2, 3, -1, 4, 5 };
Console.WriteLine($"元データ: {string.Join(", ", source)}");
Console.WriteLine($"GetValidNumbers（負数で停止）: {string.Join(", ", GetValidNumbers(source))}");

Console.WriteLine("\n=== 例の終了 ===");

// --------------------------------------------------------------
// イテレーターメソッド
// --------------------------------------------------------------

static IEnumerable<int> GetValidNumbers(List<int> source)
{
    foreach (var n in source)
    {
        if (n < 0) yield break;
        yield return n;
    }
}
