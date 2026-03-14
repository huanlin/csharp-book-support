// デモ: switch 式

Console.WriteLine("=== switch 式の例 ===\n");

// --------------------------------------------------------------
// 1. 基本的な switch 式
// --------------------------------------------------------------
Console.WriteLine("1. 基本的な switch 式");
Console.WriteLine(new string('-', 40));

foreach (DayOfWeek day in Enum.GetValues<DayOfWeek>())
{
    string dayType = GetDayType(day);
    Console.WriteLine($"{day}: {dayType}");
}

// --------------------------------------------------------------
// 2. switch 式の type pattern
// --------------------------------------------------------------
Console.WriteLine("\n2. switch 式の type pattern");
Console.WriteLine(new string('-', 40));

object[] shapes = [
    new Circle(5),
    new Rectangle(4, 6),
    new Triangle(3, 8),
    null!
];

foreach (object? shape in shapes)
{
    string description = DescribeShape(shape);
    Console.WriteLine(description);
}

// --------------------------------------------------------------
// 3. when 句で条件追加
// --------------------------------------------------------------
Console.WriteLine("\n3. when 句で条件追加");
Console.WriteLine(new string('-', 40));

int[] numbers = [-5, 0, 7, 12, 100];

foreach (int n in numbers)
{
    string description = DescribeNumber(n);
    Console.WriteLine($"{n}: {description}");
}

// --------------------------------------------------------------
// 4. 網羅性チェックと既定分岐
// --------------------------------------------------------------
Console.WriteLine("\n4. 網羅性チェック");
Console.WriteLine(new string('-', 40));

Shape[] allShapes = [
    new Circle(3),
    new Rectangle(2, 4),
    new Triangle(5, 6)
];

foreach (Shape s in allShapes)
{
    // コンパイラは、明らかに不完全な switch 式を補助的に検出できる
    string name = DescribeShapeExhaustive(s);
    Console.WriteLine(name);
}

Console.WriteLine("\n=== 例の終了 ===");

// ============================================================
// switch 式メソッド
// ============================================================

static string GetDayType(DayOfWeek day) => day switch
{
    DayOfWeek.Saturday or DayOfWeek.Sunday => "週末",
    _ => "平日"
};

static string DescribeShape(object? shape) => shape switch
{
    Circle c => $"円, 半径 {c.Radius}, 面積 {c.Area:F2}",
    Rectangle r => $"長方形, {r.Width} x {r.Height}, 面積 {r.Area:F2}",
    Triangle t => $"三角形, 底辺 {t.Base}, 高さ {t.Height}, 面積 {t.Area:F2}",
    null => "図形なし",
    _ => "未知の図形"
};

static string DescribeNumber(int n) => n switch
{
    int x when x < 0 => "負の数",
    0 => "ゼロ",
    int x when x % 2 == 0 => "正の偶数",
    _ => "正の奇数"
};

static string DescribeShapeExhaustive(Shape shape) => shape switch
{
    Circle => "円",
    Rectangle => "長方形",
    Triangle => "三角形",
    _ => throw new ArgumentException("未知の図形型")
};

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
