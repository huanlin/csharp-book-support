// デモ: 遅延実行と無限シーケンス

Console.WriteLine("=== 2. 遅延実行デモ ===");
Console.WriteLine(new string('-', 40));

Console.WriteLine("LINQ クエリを作成...");
var list = new List<int> { 1, 2, 3, 4, 5 };
var query = list.Where(n => n > 2);
Console.WriteLine("クエリ作成完了（まだ未実行）");

Console.WriteLine("\n元リストを変更（6 を追加）...");
list.Add(6);

Console.WriteLine("ここでクエリ実行:");
foreach (var n in query)
{
    Console.Write($"{n} ");
}
Console.WriteLine("\n（遅延実行のため結果に 6 も含まれる）");

Console.WriteLine("\n=== 3. 無限シーケンス: Fibonacci ===");
Console.WriteLine(new string('-', 40));

Console.WriteLine("Fibonacci の先頭 15 項:");
foreach (var n in Fibonacci().Take(15))
{
    Console.Write($"{n} ");
}
Console.WriteLine();

Console.WriteLine("\n=== 例の終了 ===");

// --------------------------------------------------------------
// イテレーターメソッド
// --------------------------------------------------------------

static IEnumerable<long> Fibonacci()
{
    long current = 1, next = 1;

    while (true)
    {
        yield return current;
        (current, next) = (next, current + next);
    }
}
