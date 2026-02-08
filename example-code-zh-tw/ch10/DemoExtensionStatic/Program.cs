// 示範：C# 14 擴充靜態成員
// 定義靜態擴充方法、靜態擴充屬性、擴充運算子

// 擴充靜態成員：透過型別名稱呼叫，而非物件實例
public static class EnumerableExtensions
{
    // 擴充實例成員（有參數名稱 source）
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool IsEmpty => !source.Any();
    }
    
    // 需要參考型別約束的擴充
    extension<TSource>(IEnumerable<TSource> source) where TSource : class?
    {
        public IEnumerable<TSource> WhereNotNull()
        {
            return source.Where(x => x is not null);
        }
    }
    
    // 擴充靜態成員（注意：沒有參數名稱，只有型別）
    extension<TSource>(IEnumerable<TSource>)
    {
        // 靜態擴充方法
        public static IEnumerable<TSource> Combine(
            IEnumerable<TSource> first, 
            IEnumerable<TSource> second) 
            => first.Concat(second);
        
        // 靜態擴充屬性
        public static IEnumerable<TSource> Empty 
            => Enumerable.Empty<TSource>();
        
        // 擴充運算子
        public static IEnumerable<TSource> operator +(
            IEnumerable<TSource> left, 
            IEnumerable<TSource> right) 
            => left.Concat(right);
    }
}

// 示範主程式
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== C# 14 擴充靜態成員示範 ===\n");

        // 情境 1：實例擴充成員（透過物件呼叫）
        Console.WriteLine("情境 1：實例擴充成員");
        var numbers = new[] { 1, 2, 3 };
        Console.WriteLine($"  numbers.IsEmpty = {numbers.IsEmpty}");
        
        var emptyList = Array.Empty<int>();
        Console.WriteLine($"  emptyList.IsEmpty = {emptyList.IsEmpty}\n");

        // 情境 2：靜態擴充成員（透過型別名稱呼叫）
        Console.WriteLine("情境 2：靜態擴充成員");
        var first = new[] { 1, 2 };
        var second = new[] { 3, 4 };
        var combined = IEnumerable<int>.Combine(first, second);
        Console.WriteLine($"  IEnumerable<int>.Combine([1,2], [3,4]) = [{string.Join(", ", combined)}]");
        
        var empty = IEnumerable<string>.Empty;
        Console.WriteLine($"  IEnumerable<string>.Empty.Count() = {empty.Count()}\n");

        // 情境 3：擴充運算子（使用 + 語法）
        Console.WriteLine("情境 3：擴充運算子");
        var list1 = new[] { "a", "b" };
        var list2 = new[] { "c", "d" };
        var merged = list1 + list2;
        Console.WriteLine($"  [\"a\",\"b\"] + [\"c\",\"d\"] = [{string.Join(", ", merged)}]\n");

        // 情境 4：WhereNotNull（有型別約束的擴充）
        Console.WriteLine("情境 4：帶型別約束的擴充方法");
        var names = new[] { "Alice", null, "Bob", null };
        var validNames = names.WhereNotNull();
        Console.WriteLine($"  過濾 null 後：[{string.Join(", ", validNames)}]");

        // 說明
        Console.WriteLine("\n=== 說明 ===");
        Console.WriteLine("- 實例成員：extension<T>(IEnumerable<T> source) 有參數名稱");
        Console.WriteLine("- 靜態成員：extension<T>(IEnumerable<T>) 沒有參數名稱");
        Console.WriteLine("- 擴充運算子：可為現有型別「添加」運算子");
    }
}
