// デモ: 計算プロパティと遅延評価

Console.WriteLine("=== 基本的な計算プロパティ ===");
var sp = new SimplePoint(3, 4);
Console.WriteLine($"SimplePoint: ({sp.X}, {sp.Y})");
Console.WriteLine($"距離: {sp.DistanceFromOrigin}");  // 毎回計算

Console.WriteLine();
Console.WriteLine("=== 遅延評価とキャッシュ ===");
var p1 = new CachedPoint(3, 4);
Console.WriteLine($"p1 DistanceFromOrigin: {p1.DistanceFromOrigin}");  // 計算してキャッシュ
Console.WriteLine($"p1 DistanceFromOrigin: {p1.DistanceFromOrigin}");  // キャッシュ利用

Console.WriteLine();
Console.WriteLine("=== with 式とキャッシュ無効化 ===");
var p2 = p1 with { Y = 5 };
Console.WriteLine($"p2: ({p2.X}, {p2.Y})");
Console.WriteLine($"p2 DistanceFromOrigin: {p2.DistanceFromOrigin}");  // 再計算

Console.WriteLine();
Console.WriteLine("=== 元オブジェクトは不変 ===");
Console.WriteLine($"p1: ({p1.X}, {p1.Y})");
Console.WriteLine($"p1 DistanceFromOrigin: {p1.DistanceFromOrigin}");  // 既存キャッシュ利用

// 基本的な計算プロパティ（アクセスごとに再計算）
public record SimplePoint(double X, double Y)
{
    public double DistanceFromOrigin => Math.Sqrt(X * X + Y * Y);
}

// 遅延評価とキャッシュ
public record CachedPoint(double X, double Y)
{
    private double? _distanceCache;

    public double DistanceFromOrigin => _distanceCache ??= Math.Sqrt(X * X + Y * Y);

    // カスタムコピーコンストラクター: キャッシュはコピーしない
    protected CachedPoint(CachedPoint other) => (X, Y) = other;
}
