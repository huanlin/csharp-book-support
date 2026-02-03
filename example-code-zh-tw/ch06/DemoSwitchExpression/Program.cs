// 示範 Switch 表達式

Console.WriteLine("=== Switch 表達式範例 ===\n");

// --------------------------------------------------------------
// 1. 基本 Switch 表達式
// --------------------------------------------------------------
Console.WriteLine("1. 基本 Switch 表達式");
Console.WriteLine(new string('-', 40));

foreach (DayOfWeek day in Enum.GetValues<DayOfWeek>())
{
    string dayType = GetDayType(day);
    Console.WriteLine($"{day}: {dayType}");
}

// --------------------------------------------------------------
// 2. 型別模式在 Switch 表達式中
// --------------------------------------------------------------
Console.WriteLine("\n2. 型別模式在 Switch 表達式中");
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
// 3. 使用 when 子句添加條件
// --------------------------------------------------------------
Console.WriteLine("\n3. 使用 when 子句添加條件");
Console.WriteLine(new string('-', 40));

int[] numbers = [-5, 0, 7, 12, 100];

foreach (int n in numbers)
{
    string description = DescribeNumber(n);
    Console.WriteLine($"{n}: {description}");
}

// --------------------------------------------------------------
// 4. 窮舉性檢查與預設分支
// --------------------------------------------------------------
Console.WriteLine("\n4. 窮舉性檢查");
Console.WriteLine(new string('-', 40));

Shape[] allShapes = [
    new Circle(3),
    new Rectangle(2, 4),
    new Triangle(5, 6)
];

foreach (Shape s in allShapes)
{
    // 如果 Shape 是密封型別階層，編譯器可以檢查窮舉性
    string name = DescribeShapeExhaustive(s);
    Console.WriteLine(name);
}

Console.WriteLine("\n=== 範例結束 ===");

// ============================================================
// Switch 表達式方法
// ============================================================

static string GetDayType(DayOfWeek day) => day switch
{
    DayOfWeek.Saturday or DayOfWeek.Sunday => "週末",
    _ => "工作日"
};

static string DescribeShape(object? shape) => shape switch
{
    Circle c => $"圓形，半徑 {c.Radius}，面積 {c.Area:F2}",
    Rectangle r => $"矩形，{r.Width} x {r.Height}，面積 {r.Area:F2}",
    Triangle t => $"三角形，底 {t.Base}，高 {t.Height}，面積 {t.Area:F2}",
    null => "沒有形狀",
    _ => "未知的形狀"
};

static string DescribeNumber(int n) => n switch
{
    int x when x < 0 => "負數",
    0 => "零",
    int x when x % 2 == 0 => "正偶數",
    _ => "正奇數"
};

static string DescribeShapeExhaustive(Shape shape) => shape switch
{
    Circle => "圓形",
    Rectangle => "矩形",
    Triangle => "三角形",
    _ => throw new ArgumentException("未知的形狀類型")
};

// ============================================================
// 形狀類別
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
