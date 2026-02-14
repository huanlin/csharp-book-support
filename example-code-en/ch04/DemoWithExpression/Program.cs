// Demo: with expression: non-destructive mutation

Console.WriteLine("=== with Expression Basic Usage ===");
var alice = new Person("Alice", 30);
var olderAlice = alice with { Age = 31 };

Console.WriteLine($"alice: {alice}");
Console.WriteLine($"olderAlice: {olderAlice}");
Console.WriteLine($"alice == olderAlice: {alice == olderAlice}");  // False

Console.WriteLine();
Console.WriteLine("=== Modifying multiple properties ===");
var bob = alice with { Name = "Bob", Age = 25 };
Console.WriteLine($"bob: {bob}");

Console.WriteLine();
Console.WriteLine("=== Custom Copy Constructor ===");
var p1 = new PersonWithCache("Alice", 30);
Console.WriteLine($"p1.CachedValue: {p1.CachedValue}");  // Calculated
Console.WriteLine($"p1.CachedValue: {p1.CachedValue}");  // Uses cache

var p2 = p1 with { Age = 31 };
Console.WriteLine($"p2.CachedValue: {p2.CachedValue}");  // Re-calculated (because cache was not copied)

public record Person(string Name, int Age);

// record with a custom copy constructor
public record PersonWithCache(string Name, int Age)
{
    private int? _cachedValue;

    public int CachedValue => _cachedValue ??= ComputeExpensiveValue();

    private int ComputeExpensiveValue()
    {
        Console.WriteLine("  (Calculating...)");
        return Name.Length * Age;
    }

    // Custom copy constructor: do not copy the cache
    protected PersonWithCache(PersonWithCache original)
    {
        Name = original.Name;
        Age = original.Age;
        // Purposefully do not copy _cachedValue, making it re-calculate
        _cachedValue = null;
    }
}
