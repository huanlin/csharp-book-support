using System.Collections;

Console.WriteLine("=== 讓 int 支援 foreach（duck typing）===\n");

foreach (var i in 3)
{
    Console.WriteLine($"Hello {i}");
}

Console.WriteLine("\n說明：");
Console.WriteLine("foreach 會尋找名為 GetEnumerator 的成員。");
Console.WriteLine("只要列舉器符合規範，就不一定要實作 IEnumerable。");

public static class IntExtensions
{
    // 透過擴充方法讓 int 可以被 foreach 迭代。
    public static IEnumerator<int> GetEnumerator(this int count)
    {
        return Enumerable.Range(0, count).GetEnumerator();
    }
}
