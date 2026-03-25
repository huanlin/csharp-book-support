// Demo: Generic Structs

Console.WriteLine("=== Generic Structs ===\n");

Console.WriteLine("1. Built-in KeyValuePair<TKey, TValue>");
Console.WriteLine(new string('-', 40));

var pair = new KeyValuePair<string, int>("Age", 30);
Console.WriteLine($"{pair.Key}: {pair.Value}");

Console.WriteLine("\n2. Custom generic struct MyPair<TKey, TValue>");
Console.WriteLine(new string('-', 40));

var pair2 = new MyPair<string, decimal>("Price", 99.99m);
Console.WriteLine($"{pair2.Key}: {pair2.Value}");

Console.WriteLine("\n=== Example End ===");

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
