// デモ: ジェネリックインターフェイス

Console.WriteLine("=== ジェネリックインターフェイス ===\n");

// --------------------------------------------------------------
// 1. ジェネリックインターフェイス実装
// --------------------------------------------------------------
Console.WriteLine("1. ジェネリックインターフェイス実装");
Console.WriteLine(new string('-', 40));

var list = new MyGenericList<int>();
list.Add(10);
list.Add(20);
list.Add(30);

Console.WriteLine($"list[0] = {list[0]}");
Console.WriteLine($"list[1] = {list[1]}");
Console.WriteLine($"Count = {list.Count}");

// --------------------------------------------------------------
// 2. Open/Closed 型パラメーター
// --------------------------------------------------------------
Console.WriteLine("\n2. 実装の2方式");
Console.WriteLine(new string('-', 40));

var hello1 = new Hello1<string>();
Console.WriteLine(hello1.GetWords("World"));

var hello1Int = new Hello1<int>();
Console.WriteLine(hello1Int.GetWords(42));

var hello2 = new Hello2<string>();
Console.WriteLine(hello2.GetWords(100));

// --------------------------------------------------------------
// 3. ジェネリック構造体
// --------------------------------------------------------------
Console.WriteLine("\n3. ジェネリック構造体（KeyValuePair）");
Console.WriteLine(new string('-', 40));

var pair = new KeyValuePair<string, int>("Age", 30);
Console.WriteLine($"{pair.Key}: {pair.Value}");

var pair2 = new MyPair<string, decimal>("Price", 99.99m);
Console.WriteLine($"{pair2.Key}: {pair2.Value}");

// --------------------------------------------------------------
// 4. typeof と unbound generic type
// --------------------------------------------------------------
Console.WriteLine("\n4. typeof と unbound generic type");
Console.WriteLine(new string('-', 40));

Type a1 = typeof(MyGenericList<>);
Type a2 = typeof(MyPair<,>);

Console.WriteLine($"MyGenericList<> 名称: {a1.Name}");
Console.WriteLine($"MyPair<,> 名称: {a2.Name}");

Type a3 = typeof(MyGenericList<int>);
Type a4 = typeof(MyPair<string, int>);

Console.WriteLine($"\nMyGenericList<int> 名称: {a3.Name}");
Console.WriteLine($"MyGenericList<int> 完全名: {a3.FullName}");

// --------------------------------------------------------------
// 5. ジェネリック拡張メソッド
// --------------------------------------------------------------
Console.WriteLine("\n5. ジェネリック拡張メソッド");
Console.WriteLine(new string('-', 40));

var numbers = new List<int> { 1, 2, 3 };
numbers.PrintAll();

var strings = numbers.ConvertAll(n => $"Item {n}");
strings.PrintAll();

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// ヘルパー
// ============================================================

public interface IMyList<T>
{
    int Count { get; }
    T this[int index] { get; set; }
    void Add(T item);
    void Clear();
}

public class MyGenericList<T> : IMyList<T>
{
    private readonly List<T> _elements = new();

    public int Count => _elements.Count;

    public T this[int index]
    {
        get => _elements[index];
        set => _elements[index] = value;
    }

    public void Add(T item) => _elements.Add(item);
    public void Clear() => _elements.Clear();
}

public interface ISayHello<T>
{
    string GetWords(T obj);
}

public class Hello1<T> : ISayHello<T>
{
    public string GetWords(T obj)
    {
        return $"Hello, {obj}!";
    }
}

public class Hello2<T> : ISayHello<int>
{
    public string GetWords(int i)
    {
        return $"Hello, {i}!";
    }
}

public readonly struct MyPair<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; }

    public MyPair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}

public static class ListExtensions
{
    public static void PrintAll<T>(this List<T> list)
    {
        Console.WriteLine($"[{string.Join(", ", list)}]");
    }

    public static List<TResult> ConvertAll<T, TResult>(
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
