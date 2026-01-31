// 示範泛型介面

Console.WriteLine("=== 泛型介面 ===\n");

// --------------------------------------------------------------
// 1. 實作泛型介面
// --------------------------------------------------------------
Console.WriteLine("1. 實作泛型介面");
Console.WriteLine(new string('-', 40));

var list = new MyGenericList<int>();
list.Add(10);
list.Add(20);
list.Add(30);

Console.WriteLine($"list[0] = {list[0]}");
Console.WriteLine($"list[1] = {list[1]}");
Console.WriteLine($"Count = {list.Count}");

// --------------------------------------------------------------
// 2. 保持型別參數開放 vs 明確指定
// --------------------------------------------------------------
Console.WriteLine("\n2. 兩種實作方式");
Console.WriteLine(new string('-', 40));

// 方式 1：保持型別參數開放
var hello1 = new Hello1<string>();
Console.WriteLine(hello1.GetWords("World"));

var hello1Int = new Hello1<int>();
Console.WriteLine(hello1Int.GetWords(42));

// 方式 2：明確指定型別參數
var hello2 = new Hello2<string>();  // T 是 string，但 GetWords 參數是 int
Console.WriteLine(hello2.GetWords(100));

// --------------------------------------------------------------
// 3. 泛型結構
// --------------------------------------------------------------
Console.WriteLine("\n3. 泛型結構（KeyValuePair）");
Console.WriteLine(new string('-', 40));

var pair = new KeyValuePair<string, int>("Age", 30);
Console.WriteLine($"{pair.Key}: {pair.Value}");

var pair2 = new MyPair<string, decimal>("Price", 99.99m);
Console.WriteLine($"{pair2.Key}: {pair2.Value}");

// --------------------------------------------------------------
// 4. typeof 與未綁定泛型型別
// --------------------------------------------------------------
Console.WriteLine("\n4. typeof 與未綁定泛型型別");
Console.WriteLine(new string('-', 40));

// 未綁定泛型型別
Type a1 = typeof(MyGenericList<>);     // 單一型別參數
Type a2 = typeof(MyPair<,>);           // 兩個型別參數

Console.WriteLine($"MyGenericList<> 名稱：{a1.Name}");
Console.WriteLine($"MyPair<,> 名稱：{a2.Name}");

// 已關閉的型別
Type a3 = typeof(MyGenericList<int>);
Type a4 = typeof(MyPair<string, int>);

Console.WriteLine($"\nMyGenericList<int> 名稱：{a3.Name}");
Console.WriteLine($"MyGenericList<int> 完整名稱：{a3.FullName}");

// --------------------------------------------------------------
// 5. 泛型擴充方法
// --------------------------------------------------------------
Console.WriteLine("\n5. 泛型擴充方法");
Console.WriteLine(new string('-', 40));

var numbers = new List<int> { 1, 2, 3 };
numbers.PrintAll();

var strings = numbers.ConvertAll(n => $"項目 {n}");
strings.PrintAll();

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// 輔助介面與類別
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

// 泛型介面
public interface ISayHello<T>
{
    string GetWords(T obj);
}

// 方式 1：保持型別參數開放
public class Hello1<T> : ISayHello<T>
{
    public string GetWords(T obj)
    {
        return $"Hello, {obj}!";
    }
}

// 方式 2：明確指定型別參數
public class Hello2<T> : ISayHello<int>
{
    public string GetWords(int i)
    {
        return $"Hello, {i}!";
    }
}

// 自訂泛型結構
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

// 泛型擴充方法
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
