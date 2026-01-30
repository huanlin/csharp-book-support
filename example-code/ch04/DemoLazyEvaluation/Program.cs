// 示範計算屬性與延遲求值

Console.WriteLine("=== 基本計算屬性 ===");
var sp = new SimplePoint(3, 4);
Console.WriteLine($"SimplePoint: ({sp.X}, {sp.Y})");
Console.WriteLine($"Distance: {sp.DistanceFromOrigin}");  // 每次都計算

Console.WriteLine();
Console.WriteLine("=== 延遲求值與快取 ===");
var p1 = new CachedPoint(3, 4);
Console.WriteLine($"p1 DistanceFromOrigin: {p1.DistanceFromOrigin}");  // 計算並快取
Console.WriteLine($"p1 DistanceFromOrigin: {p1.DistanceFromOrigin}");  // 使用快取

Console.WriteLine();
Console.WriteLine("=== with 表達式與快取失效 ===");
var p2 = p1 with { Y = 5 };
Console.WriteLine($"p2: ({p2.X}, {p2.Y})");
Console.WriteLine($"p2 DistanceFromOrigin: {p2.DistanceFromOrigin}");  // 重新計算

Console.WriteLine();
Console.WriteLine("=== 原始物件不變 ===");
Console.WriteLine($"p1: ({p1.X}, {p1.Y})");
Console.WriteLine($"p1 DistanceFromOrigin: {p1.DistanceFromOrigin}");  // 仍使用快取

// 基本的計算屬性（每次存取都重新計算）
public record SimplePoint(double X, double Y)
{
    public double DistanceFromOrigin => Math.Sqrt(X * X + Y * Y);
}

// 延遲求值與快取
public record CachedPoint(double X, double Y)
{
    private double? _distanceCache;

    public double DistanceFromOrigin => _distanceCache ??= Math.Sqrt(X * X + Y * Y);

    // 自訂複製建構子，不複製快取
    protected CachedPoint(CachedPoint other) => (X, Y) = other;
}
