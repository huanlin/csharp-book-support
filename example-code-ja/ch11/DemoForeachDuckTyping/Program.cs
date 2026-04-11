using System.Collections;

Console.WriteLine("=== int で foreach を使う（duck typing）===\n");

foreach (var i in 3)
{
    Console.WriteLine($"Hello {i}");
}

Console.WriteLine("\n説明:");
Console.WriteLine("foreach は GetEnumerator という名前のメンバーを探します。");
Console.WriteLine("列挙子がパターンを満たしていれば、IEnumerable の実装は必須ではありません。");

public static class IntExtensions
{
    // 拡張メソッドで int を foreach 可能にする。
    public static IEnumerator<int> GetEnumerator(this int count)
    {
        return Enumerable.Range(0, count).GetEnumerator();
    }
}
