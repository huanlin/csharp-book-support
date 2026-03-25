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
var hello2 = new Hello2();
Console.WriteLine(hello2.GetWords(100));

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
public class Hello2 : ISayHello<int>
{
    public string GetWords(int i)
    {
        return $"Hello, {i}!";
    }
}

