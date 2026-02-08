// 示範 with 表達式：非破壞性修改

Console.WriteLine("=== with 表達式基本用法 ===");
var alice = new Person("Alice", 30);
var olderAlice = alice with { Age = 31 };

Console.WriteLine($"alice: {alice}");
Console.WriteLine($"olderAlice: {olderAlice}");
Console.WriteLine($"alice == olderAlice: {alice == olderAlice}");  // False

Console.WriteLine();
Console.WriteLine("=== 修改多個屬性 ===");
var bob = alice with { Name = "Bob", Age = 25 };
Console.WriteLine($"bob: {bob}");

Console.WriteLine();
Console.WriteLine("=== 自訂複製建構式 ===");
var p1 = new PersonWithCache("Alice", 30);
Console.WriteLine($"p1.CachedValue: {p1.CachedValue}");  // 計算
Console.WriteLine($"p1.CachedValue: {p1.CachedValue}");  // 使用快取

var p2 = p1 with { Age = 31 };
Console.WriteLine($"p2.CachedValue: {p2.CachedValue}");  // 重新計算（因為快取未複製）

public record Person(string Name, int Age);

// 自訂複製建構式的 record
public record PersonWithCache(string Name, int Age)
{
    private int? _cachedValue;

    public int CachedValue => _cachedValue ??= ComputeExpensiveValue();

    private int ComputeExpensiveValue()
    {
        Console.WriteLine("  (計算中...)");
        return Name.Length * Age;
    }

    // 自訂複製建構式：不複製快取
    protected PersonWithCache(PersonWithCache original)
    {
        Name = original.Name;
        Age = original.Age;
        // 故意不複製 _cachedValue，讓它重新計算
        _cachedValue = null;
    }
}
