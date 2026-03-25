// デモ: ジェネリック構造体

Console.WriteLine("=== ジェネリック構造体 ===\n");

Console.WriteLine("1. 組み込みの KeyValuePair<TKey, TValue>");
Console.WriteLine(new string('-', 40));

var pair = new KeyValuePair<string, int>("Age", 30);
Console.WriteLine($"{pair.Key}: {pair.Value}");

Console.WriteLine("\n2. カスタム ジェネリック構造体 MyPair<TKey, TValue>");
Console.WriteLine(new string('-', 40));

var pair2 = new MyPair<string, decimal>("Price", 99.99m);
Console.WriteLine($"{pair2.Key}: {pair2.Value}");

Console.WriteLine("\n=== 例の終了 ===");

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
