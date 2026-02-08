// 示範：擴充 LINQ
// LINQ 本身就是透過擴充方法實作的，你可以自己添加新的 LINQ 操作

// 自訂的 LINQ 擴充方法
public static class LinqExtensions
{
    /// <summary>
    /// 過濾掉 null 元素，只回傳非 null 的項目。
    /// </summary>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
        where T : class
    {
        return source.Where(x => x != null)!;
    }

    /// <summary>
    /// 將序列分割成指定大小的批次。
    /// </summary>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        if (batchSize <= 0)
            throw new ArgumentException("批次大小必須大於 0", nameof(batchSize));

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
    /// 每隔 N 個元素取一個。
    /// </summary>
    public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> source, int step)
    {
        if (step <= 0)
            throw new ArgumentException("間隔必須大於 0", nameof(step));

        int index = 0;
        foreach (var item in source)
        {
            if (index % step == 0)
                yield return item;
            index++;
        }
    }
}

// 示範主程式
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== 擴充 LINQ 示範 ===\n");

        // 情境 1：WhereNotNull - 過濾 null
        Console.WriteLine("情境 1：WhereNotNull - 過濾 null 元素");
        var names = new[] { "Alice", null, "Bob", null, "Charlie" };
        Console.WriteLine($"  原始資料：{string.Join(", ", names.Select(n => n ?? "null"))}");
        var validNames = names.WhereNotNull().ToList();
        Console.WriteLine($"  過濾後：{string.Join(", ", validNames)}\n");

        // 情境 2：Batch - 分批處理
        Console.WriteLine("情境 2：Batch - 將序列分割成批次");
        var numbers = Enumerable.Range(1, 10);
        Console.WriteLine($"  原始資料：{string.Join(", ", numbers)}");
        Console.WriteLine($"  分成每批 3 個：");
        foreach (var batch in numbers.Batch(3))
        {
            Console.WriteLine($"    [{string.Join(", ", batch)}]");
        }

        // 情境 3：TakeEvery - 間隔取樣
        Console.WriteLine("\n情境 3：TakeEvery - 每隔 N 個取一個");
        var items = Enumerable.Range(1, 12);
        Console.WriteLine($"  原始資料：{string.Join(", ", items)}");
        var sampled = items.TakeEvery(3).ToList();
        Console.WriteLine($"  每隔 3 個取一個：{string.Join(", ", sampled)}");

        // 說明
        Console.WriteLine("\n=== 說明 ===");
        Console.WriteLine("這些擴充方法展示了如何為 LINQ 添加新功能。");
        Console.WriteLine("注意：.NET 6+ 已內建 DistinctBy 和 Chunk (類似 Batch)。");
        Console.WriteLine("自訂擴充方法適合處理專案特有的查詢邏輯。");
    }
}
