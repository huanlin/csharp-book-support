// 示範泛型結構

Console.WriteLine("=== 泛型結構 ===\n");

Console.WriteLine("1. .NET 內建的 KeyValuePair<TKey, TValue>");
Console.WriteLine(new string('-', 40));

var pair = new KeyValuePair<string, int>("Age", 30);
Console.WriteLine($"{pair.Key}: {pair.Value}");

Console.WriteLine("\n2. 自訂泛型結構 MyPair<TKey, TValue>");
Console.WriteLine(new string('-', 40));

var pair2 = new MyPair<string, decimal>("Price", 99.99m);
Console.WriteLine($"{pair2.Key}: {pair2.Value}");

Console.WriteLine("\n=== 範例結束 ===");

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
