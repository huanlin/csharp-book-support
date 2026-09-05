// デモ: ジェネリック メソッド

Console.WriteLine("=== ジェネリック メソッド ===\n");

Console.WriteLine("1. 基本的なジェネリック メソッド");
Console.WriteLine(new string('-', 40));

var util = new Utility();
util.Print<int>(100);
util.Print<string>("こんにちは");

DateTime result = util.Create<string, DateTime>("now");
Console.WriteLine($"Create<string, DateTime>(\"now\") = {result:O}");

Console.WriteLine("\n2. 型推論");
Console.WriteLine(new string('-', 40));

util.Print(3.14);
util.Print("型推論が機能します");

Console.WriteLine("\n3. メソッドレベルとクラスレベルの型パラメーター");
Console.WriteLine(new string('-', 40));

var demo = new Demo<int>();
demo.Method1(42);
demo.Method2("こんにちは");
demo.Method3("隠された T");

Console.WriteLine("\n4. ジェネリック拡張メソッド");
Console.WriteLine(new string('-', 40));

var numbers = new List<int> { 1, 2, 3 };
numbers.PrintAll();

var strings = numbers.MapAll(n => $"項目 {n}");
strings.PrintAll();

Console.WriteLine("\n=== 例の終了 ===");

public class Utility
{
    public void Print<T>(T obj)
    {
        Console.WriteLine($"型: {typeof(T).Name}, 値: {obj}");
    }

    public TResult Create<T, TResult>(T obj) where TResult : new()
    {
        Console.WriteLine($"{obj} から作成中");
        return new TResult();
    }
}

public class Demo<T>
{
    public void Method1(T obj)
    {
        Console.WriteLine($"Method1: {obj}");
    }

    public void Method2<U>(U obj)
    {
        Console.WriteLine($"Method2: {obj}");
    }

#pragma warning disable CS0693
    public void Method3<T>(T obj)
    {
        Console.WriteLine($"Method3: {obj}");
    }
#pragma warning restore CS0693
}

public static class ListExtensions
{
    public static void PrintAll<T>(this List<T> list)
    {
        Console.WriteLine($"[{string.Join(", ", list)}]");
    }

    public static List<TResult> MapAll<T, TResult>(
        this List<T> source,
        Func<T, TResult> converter)
    {
        var result = new List<TResult>();
        foreach (var item in source)
        {
            result.Add(converter(item));
        }
        return result;
    }
}
