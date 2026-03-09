// デモ: C# 14 の静的拡張メンバー
// static 拡張メソッド、static 拡張プロパティ、拡張演算子を定義

// 拡張 static メンバー: オブジェクトではなく型名から呼ぶ
public static class EnumerableExtensions
{
    // 拡張インスタンスメンバー（パラメーター名 source あり）
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool IsEmpty => !source.Any();
    }
    
    // 参照型制約付き拡張
    extension<TSource>(IEnumerable<TSource> source) where TSource : class?
    {
        public IEnumerable<TSource> WhereNotNull()
        {
            return source.Where(x => x is not null);
        }
    }
    
    // 拡張 static メンバー（注: パラメーター名なし、型のみ）
    extension<TSource>(IEnumerable<TSource>)
    {
        // static 拡張メソッド
        public static IEnumerable<TSource> Combine(
            IEnumerable<TSource> first, 
            IEnumerable<TSource> second) 
            => first.Concat(second);
        
        // static 拡張プロパティ
        public static IEnumerable<TSource> Empty 
            => Enumerable.Empty<TSource>();
        
        // 拡張演算子
        public static IEnumerable<TSource> operator +(
            IEnumerable<TSource> left, 
            IEnumerable<TSource> right) 
            => left.Concat(right);
    }
}

// デモ用メインプログラム
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== C# 14 静的拡張メンバーデモ ===\n");

        // シナリオ 1: 拡張インスタンスメンバー（オブジェクト経由で呼ぶ）
        Console.WriteLine("シナリオ 1: 拡張インスタンスメンバー");
        var numbers = new[] { 1, 2, 3 };
        Console.WriteLine($"  numbers.IsEmpty = {numbers.IsEmpty}");
        
        var emptyList = Array.Empty<int>();
        Console.WriteLine($"  emptyList.IsEmpty = {emptyList.IsEmpty}\n");

        // シナリオ 2: 拡張 static メンバー（型名で呼ぶ）
        Console.WriteLine("シナリオ 2: 拡張 static メンバー");
        var first = new[] { 1, 2 };
        var second = new[] { 3, 4 };
        var combined = IEnumerable<int>.Combine(first, second);
        Console.WriteLine($"  IEnumerable<int>.Combine([1,2], [3,4]) = [{string.Join(", ", combined)}]");
        
        var empty = IEnumerable<string>.Empty;
        Console.WriteLine($"  IEnumerable<string>.Empty.Count() = {empty.Count()}\n");

        // シナリオ 3: 拡張演算子（+ 構文）
        Console.WriteLine("シナリオ 3: 拡張演算子");
        var list1 = new[] { "a", "b" };
        var list2 = new[] { "c", "d" };
        var merged = list1 + list2;
        Console.WriteLine($"  [\"a\",\"b\"] + [\"c\",\"d\"] = [{string.Join(", ", merged)}]\n");

        // シナリオ 4: WhereNotNull（型制約付き拡張）
        Console.WriteLine("シナリオ 4: 型制約付き拡張メソッド");
        var names = new[] { "Alice", null, "Bob", null };
        var validNames = names.WhereNotNull();
        Console.WriteLine($"  null 除外後: [{string.Join(", ", validNames)}]");

        // 解説
        Console.WriteLine("\n=== 解説 ===");
        Console.WriteLine("- インスタンスメンバー: extension<T>(IEnumerable<T> source) はパラメーター名あり");
        Console.WriteLine("- static メンバー: extension<T>(IEnumerable<T>) はパラメーター名なし");
        Console.WriteLine("- 拡張演算子: 既存型に演算子を追加できる");
    }
}
