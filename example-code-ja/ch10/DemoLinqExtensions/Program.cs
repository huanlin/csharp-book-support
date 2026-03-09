// デモ: LINQ の拡張
// LINQ 自体が拡張メソッドで実装されており、独自演算子も追加できる。

// 独自 LINQ 拡張メソッド
public static class LinqExtensions
{
    /// <summary>
    /// null 要素を除外し、非 null の要素のみ返す。
    /// </summary>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
        where T : class
    {
        return source.Where(x => x != null)!;
    }

    /// <summary>
    /// シーケンスを指定サイズごとのバッチに分割する。
    /// </summary>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        if (batchSize <= 0)
            throw new ArgumentException("バッチサイズは 0 より大きくする必要があります。", nameof(batchSize));

        var batch = new List<T>(batchSize);
        foreach (var item in source)
        {
            batch.Add(item);
            if (batch.Count == batchSize)
            {
                yield return batch;
                batch = new List<T>(batchSize);
            }
        }

        if (batch.Count > 0)
            yield return batch;
    }

    /// <summary>
    /// シーケンスから N 個おきの要素を取得する。
    /// </summary>
    public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> source, int step)
    {
        if (step <= 0)
            throw new ArgumentException("step は 0 より大きくする必要があります。", nameof(step));

        int index = 0;
        foreach (var item in source)
        {
            if (index % step == 0)
                yield return item;
            index++;
        }
    }
}

// デモ用メインプログラム
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== LINQ 拡張デモ ===\n");

        // シナリオ 1: WhereNotNull - null の除外
        Console.WriteLine("シナリオ 1: WhereNotNull - null 要素の除外");
        var names = new[] { "Alice", null, "Bob", null, "Charlie" };
        Console.WriteLine($"  元データ: {string.Join(", ", names.Select(n => n ?? "null"))}");
        var validNames = names.WhereNotNull().ToList();
        Console.WriteLine($"  フィルタ結果: {string.Join(", ", validNames)}\n");

        // シナリオ 2: Batch - バッチ分割
        Console.WriteLine("シナリオ 2: Batch - シーケンスをバッチ分割");
        var numbers = Enumerable.Range(1, 10);
        Console.WriteLine($"  元データ: {string.Join(", ", numbers)}");
        Console.WriteLine("  3 件ずつに分割:");
        foreach (var batch in numbers.Batch(3))
        {
            Console.WriteLine($"    [{string.Join(", ", batch)}]");
        }

        // シナリオ 3: TakeEvery - 間引き取得
        Console.WriteLine("\nシナリオ 3: TakeEvery - N 個おきに取得");
        var items = Enumerable.Range(1, 12);
        Console.WriteLine($"  元データ: {string.Join(", ", items)}");
        var sampled = items.TakeEvery(3).ToList();
        Console.WriteLine($"  3 個おきの要素: {string.Join(", ", sampled)}");

        // 解説
        Console.WriteLine("\n=== 解説 ===");
        Console.WriteLine("これらの拡張メソッドは LINQ に機能を追加する方法を示している。");
        Console.WriteLine("注: .NET 6+ には DistinctBy や Chunk（Batch に近い）が標準搭載。");
        Console.WriteLine("独自拡張はプロジェクト固有のクエリロジックに適している。");
    }
}
