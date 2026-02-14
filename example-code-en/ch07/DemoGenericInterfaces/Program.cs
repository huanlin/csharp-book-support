// Demo: Generic Interfaces

Console.WriteLine("=== Generic Interfaces ===\n");

// --------------------------------------------------------------
// 1. Implementing a Generic Interface
// --------------------------------------------------------------
Console.WriteLine("1. Implementing a Generic Interface");
Console.WriteLine(new string('-', 40));

var list = new MyGenericList<int>();
list.Add(10);
list.Add(20);
list.Add(30);

Console.WriteLine($"list[0] = {list[0]}");
Console.WriteLine($"list[1] = {list[1]}");
Console.WriteLine($"Count = {list.Count}");

// --------------------------------------------------------------
// 2. Open vs. Closed Type Parameters
// --------------------------------------------------------------
Console.WriteLine("\n2. Two Implementation Approaches");
Console.WriteLine(new string('-', 40));

// Approach 1: Keep the type parameter open
var hello1 = new Hello1<string>();
Console.WriteLine(hello1.GetWords("World"));

var hello1Int = new Hello1<int>();
Console.WriteLine(hello1Int.GetWords(42));

// Approach 2: Explicitly specify a type for the interface
var hello2 = new Hello2<string>();  // T is string, but GetWords parameter is int
Console.WriteLine(hello2.GetWords(100));

// --------------------------------------------------------------
// 3. Generic Structs
// --------------------------------------------------------------
Console.WriteLine("\n3. Generic Structs (KeyValuePair)");
Console.WriteLine(new string('-', 40));

var pair = new KeyValuePair<string, int>("Age", 30);
Console.WriteLine($"{pair.Key}: {pair.Value}");

var pair2 = new MyPair<string, decimal>("Price", 99.99m);
Console.WriteLine($"{pair2.Key}: {pair2.Value}");

// --------------------------------------------------------------
// 4. typeof and Unbound Generic Types
// --------------------------------------------------------------
Console.WriteLine("\n4. typeof and Unbound Generic Types");
Console.WriteLine(new string('-', 40));

// Unbound generic types
Type a1 = typeof(MyGenericList<>);     // Single type parameter
Type a2 = typeof(MyPair<,>);           // Two type parameters

Console.WriteLine($"MyGenericList<> Name: {a1.Name}");
Console.WriteLine($"MyPair<,> Name: {a2.Name}");

// Closed types
Type a3 = typeof(MyGenericList<int>);
Type a4 = typeof(MyPair<string, int>);

Console.WriteLine($"\nMyGenericList<int> Name: {a3.Name}");
Console.WriteLine($"MyGenericList<int> Full Name: {a3.FullName}");

// --------------------------------------------------------------
// 5. Generic Extension Methods
// --------------------------------------------------------------
Console.WriteLine("\n5. Generic Extension Methods");
Console.WriteLine(new string('-', 40));

var numbers = new List<int> { 1, 2, 3 };
numbers.PrintAll();

var strings = numbers.ConvertAll(n => $"Item {n}");
strings.PrintAll();

Console.WriteLine("\n=== Example End ===");

// ============================================================
// Helper Interfaces and Classes
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

// Generic Interface
public interface ISayHello<T>
{
    string GetWords(T obj);
}

// Approach 1: Keep type parameter open
public class Hello1<T> : ISayHello<T>
{
    public string GetWords(T obj)
    {
        return $"Hello, {obj}!";
    }
}

// Approach 2: Explicitly specify type parameter for interface
public class Hello2<T> : ISayHello<int>
{
    public string GetWords(int i)
    {
        return $"Hello, {i}!";
    }
}

// Custom Generic Struct
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

// Generic Extension Methods
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
