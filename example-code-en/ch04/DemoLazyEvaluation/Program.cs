// Demo: Computed Properties and Lazy Evaluation

Console.WriteLine("=== Basic Computed Property ===");
var sp = new SimplePoint(3, 4);
Console.WriteLine($"SimplePoint: ({sp.X}, {sp.Y})");
Console.WriteLine($"Distance: {sp.DistanceFromOrigin}");  // Calculated every time

Console.WriteLine();
Console.WriteLine("=== Lazy Evaluation and Caching ===");
var p1 = new CachedPoint(3, 4);
Console.WriteLine($"p1 DistanceFromOrigin: {p1.DistanceFromOrigin}");  // Calculates and caches
Console.WriteLine($"p1 DistanceFromOrigin: {p1.DistanceFromOrigin}");  // Uses cache

Console.WriteLine();
Console.WriteLine("=== with Expression and Cache Invalidation ===");
var p2 = p1 with { Y = 5 };
Console.WriteLine($"p2: ({p2.X}, {p2.Y})");
Console.WriteLine($"p2 DistanceFromOrigin: {p2.DistanceFromOrigin}");  // Re-calculates

Console.WriteLine();
Console.WriteLine("=== Original Object Remains Unchanged ===");
Console.WriteLine($"p1: ({p1.X}, {p1.Y})");
Console.WriteLine($"p1 DistanceFromOrigin: {p1.DistanceFromOrigin}");  // Still uses cache

// Basic computed property (re-calculates on every access)
public record SimplePoint(double X, double Y)
{
    public double DistanceFromOrigin => Math.Sqrt(X * X + Y * Y);
}

// Lazy evaluation and caching
public record CachedPoint(double X, double Y)
{
    private double? _distanceCache;

    public double DistanceFromOrigin => _distanceCache ??= Math.Sqrt(X * X + Y * Y);

    // Custom copy constructor, does not copy the cache
    protected CachedPoint(CachedPoint other) => (X, Y) = other;
}
