// デモ: with 式（非破壊的変更）

Console.WriteLine("=== with 式の基本 ===");
var alice = new Person("Alice", 30);
var olderAlice = alice with { Age = 31 };

Console.WriteLine($"alice: {alice}");
Console.WriteLine($"olderAlice: {olderAlice}");
Console.WriteLine($"alice == olderAlice: {alice == olderAlice}");  // False

Console.WriteLine();
Console.WriteLine("=== 複数プロパティの変更 ===");
var bob = alice with { Name = "Bob", Age = 25 };
Console.WriteLine($"bob: {bob}");

Console.WriteLine();
Console.WriteLine("=== カスタムコピーコンストラクター ===");
var p1 = new PersonWithCache("Alice", 30);
Console.WriteLine($"p1.CachedValue: {p1.CachedValue}");  // 計算
Console.WriteLine($"p1.CachedValue: {p1.CachedValue}");  // キャッシュ利用

var p2 = p1 with { Age = 31 };
Console.WriteLine($"p2.CachedValue: {p2.CachedValue}");  // 再計算（キャッシュをコピーしないため）

public record Person(string Name, int Age);

// カスタムコピーコンストラクターを持つ record
public record PersonWithCache(string Name, int Age)
{
    private int? _cachedValue;

    public int CachedValue => _cachedValue ??= ComputeExpensiveValue();

    private int ComputeExpensiveValue()
    {
        Console.WriteLine("  （計算中...）");
        return Name.Length * Age;
    }

    // カスタムコピーコンストラクター: キャッシュはコピーしない
    protected PersonWithCache(PersonWithCache original)
    {
        Name = original.Name;
        Age = original.Age;
        // 意図的に _cachedValue を引き継がず、再計算させる
        _cachedValue = null;
    }
}
