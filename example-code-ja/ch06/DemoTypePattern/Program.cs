// デモ: Type Pattern

Console.WriteLine("=== Type Pattern の例 ===\n");

// テストデータ
object[] shapes = [
    new Circle(5),
    new Rectangle(4, 6),
    new Triangle(3, 8),
    "This is a string",
    42,
    null!
];

// --------------------------------------------------------------
// 従来方式 vs Type Pattern
// --------------------------------------------------------------
Console.WriteLine("従来方式 vs Type Pattern");
Console.WriteLine(new string('-', 40));

foreach (object? shape in shapes)
{
    // Type pattern: 判定 + キャストを1ステップ
    if (shape is Circle c)
    {
        Console.WriteLine($"円, 半径: {c.Radius}, 面積: {c.Area:F2}");
    }
    else if (shape is Rectangle r)
    {
        Console.WriteLine($"長方形, 幅: {r.Width}, 高さ: {r.Height}, 面積: {r.Area:F2}");
    }
    else if (shape is Triangle t)
    {
        Console.WriteLine($"三角形, 底辺: {t.Base}, 高さ: {t.Height}, 面積: {t.Area:F2}");
    }
    else if (shape is null)
    {
        Console.WriteLine("オブジェクトは null");
    }
    else
    {
        Console.WriteLine($"その他の型: {shape.GetType().Name} = {shape}");
    }
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// 図形クラス
// ============================================================

public abstract class Shape
{
    public abstract double Area { get; }
}

public class Circle : Shape
{
    public double Radius { get; }
    public Circle(double radius) => Radius = radius;
    public override double Area => Math.PI * Radius * Radius;
}

public class Rectangle : Shape
{
    public double Width { get; }
    public double Height { get; }
    public Rectangle(double width, double height) => (Width, Height) = (width, height);
    public override double Area => Width * Height;
}

public class Triangle : Shape
{
    public double Base { get; }
    public double Height { get; }
    public Triangle(double @base, double height) => (Base, Height) = (@base, height);
    public override double Area => Base * Height / 2;
}
